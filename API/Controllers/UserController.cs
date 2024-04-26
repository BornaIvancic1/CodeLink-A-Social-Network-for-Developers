using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_API_.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _dbContext.User.ToListAsync();
            if (users == null || !users.Any())
            {
                return NotFound();
            }

            return users;
        }

   
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _dbContext.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

       
        [HttpPost]
        public async Task<ActionResult<PostUser>> PostUser(PostUser user)
        {
            string salt = Encryption.CreateSalt(8);
            string password = user.PwdHash;


            var existingUser = await _dbContext.User
                .FirstOrDefaultAsync(u => u.Username == user.Username || u.Email == user.Email);
            if (existingUser != null)
            {
                return Conflict("User with the same username or email already exists.");
            }

            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserProfilePictureBase64 = user.UserProfilePictureBase64,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                PwdSalt = salt,
                PwdHash = Encryption.GenerateHash(password, salt),
                KnownTechnologies = new List<Technology>()
            };


            if (user.KnownTechnologies != null)
            {
                foreach (var techName in user.KnownTechnologies)
                {
                    var existingTech = await _dbContext.Technology.FirstOrDefaultAsync(t => t.Name == techName.Name);
                    if (existingTech == null)
                    {
                        newUser.KnownTechnologies.Add(new Technology { Name = techName.Name, SkillLevel=techName.SkillLevel });
                    }
                    else
                    {
                        newUser.KnownTechnologies.Add(existingTech);
                    }
                }
            }


            _dbContext.User.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, PostUser user)
        {
            var existingUser = await _dbContext.User
                .Include(u => u.KnownTechnologies) 
                .FirstOrDefaultAsync(u => u.Id == id);

            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            string salt = Encryption.CreateSalt(8);
            string password = user.PwdHash;

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Username = user.Username;
            existingUser.UserProfilePictureBase64 = user.UserProfilePictureBase64;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.PwdSalt = salt;
            existingUser.PwdHash = Encryption.GenerateHash(password, salt);

          
            existingUser.KnownTechnologies.Clear();

       
            foreach (var techName in user.KnownTechnologies)
            {
                var existingTech = await _dbContext.Technology.FirstOrDefaultAsync(t => t.Name == techName.Name);
                if (existingTech == null)
                {
                   
                    existingUser.KnownTechnologies.Add(new Technology { Name = techName.Name });
                }
                else
                {
                   
                    existingUser.KnownTechnologies.Add(existingTech);
                }
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound("User not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _dbContext.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

      
            user.KnownTechnologies.Clear();

            _dbContext.User.Remove(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
        private bool UserExists(int id)
        {
            return _dbContext.User.Any(e => e.Id == id);
        }

        [HttpGet("login/{email}/{password}")]
        public async Task<ActionResult<User>> Login(string email,string password)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!Encryption.ValidatePassword(password, user.PwdSalt, user.PwdHash))
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(user);
        }
        [HttpPost("changePassword/{userIdf}/{oldPassword}/{newPassword}")]
        public async Task<IActionResult> ChangePassword(int userIdf, string oldPassword, string newPassword)
        {
            
            var userIdString = userIdf.ToString();
            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
            {

                var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null && Encryption.ValidatePassword(oldPassword, user.PwdSalt, user.PwdHash))
                {
                    string newSalt = Encryption.CreateSalt(8);
                    string newHash = Encryption.GenerateHash(newPassword, newSalt);

                    user.PwdSalt = newSalt;
                    user.PwdHash = newHash;

                    await _dbContext.SaveChangesAsync();

                    return Ok("Password changed successfully!");
                }
                else
                {
                    return BadRequest("Invalid current password");
                }
            }
            else
            {
                return Unauthorized();
            }
        }
      
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUser registerUser)
        {
            string salt = Encryption.CreateSalt(8);
            string password = registerUser.PwdHash;


            var existingUser = await _dbContext.User
                .FirstOrDefaultAsync(u => u.Username == registerUser.Username || u.Email == registerUser.Email);
            if (existingUser != null)
            {
                return Conflict("User with the same username or email already exists.");
            }

            var newUser = new User
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Email = registerUser.Email,
                UserProfilePictureBase64 = registerUser.UserProfilePictureBase64,
                Username = registerUser.Username,
                PhoneNumber = registerUser.PhoneNumber,
                PwdSalt = salt,
                PwdHash = Encryption.GenerateHash(password, salt),
           
            };


            _dbContext.User.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

    }
}
