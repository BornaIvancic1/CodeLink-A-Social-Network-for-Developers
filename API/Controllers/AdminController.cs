
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_API_.Models;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            var admins = await _dbContext.Admin.ToListAsync();
            if (admins == null || !admins.Any())
            {
                return NotFound();
            }

            return admins;
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _dbContext.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

   
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(PostAdmin admin)
        {
            string salt = Encryption.CreateSalt(8);
            string password = admin.PwdHash;


            var existingUser = await _dbContext.Admin
                .FirstOrDefaultAsync(u => u.Username == admin.Username || u.Email == admin.Email);
            if (existingUser != null)
            {
                return Conflict("User with the same username or email already exists.");
            }

            var newUser = new Admin
            {
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                UserProfilePictureBase64 = admin.UserProfilePictureBase64,
                Username = admin.Username,
                PhoneNumber = admin.PhoneNumber,
                PwdSalt = salt,
                PwdHash = Encryption.GenerateHash(password, salt),
            };



            _dbContext.Admin.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdmin), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, PutAdmin admin)
        {
            var existingAdmin = await _dbContext.Admin.FindAsync(id);

            if (existingAdmin == null)
            {
                return NotFound("Admin not found.");
            }

            existingAdmin.FirstName = admin.FirstName;
            existingAdmin.LastName = admin.LastName;
            existingAdmin.Email = admin.Email;
            existingAdmin.Username = admin.Username;
            existingAdmin.UserProfilePictureBase64 = admin.UserProfilePictureBase64;
            existingAdmin.PhoneNumber = admin.PhoneNumber;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
                {
                    return NotFound("Admin not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult<Admin>> DeleteAdmin(int id)
        {
            var admin = await _dbContext.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _dbContext.Admin.Remove(admin);
            await _dbContext.SaveChangesAsync();

            return admin;
        }

        private bool AdminExists(int id)
        {
            return _dbContext.Admin.Any(e => e.Id == id);
        }

        [HttpGet("login/{email}/{password}")]
        public async Task<ActionResult<Admin>> Login(string email, string password)
        {
            var admin = await _dbContext.Admin.FirstOrDefaultAsync(u => u.Email == email);
            if (admin == null)
            {
                return NotFound("Admin not found.");
            }

            if (!Encryption.ValidatePassword(password, admin.PwdSalt, admin.PwdHash))
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(admin);
        }
    }
}
