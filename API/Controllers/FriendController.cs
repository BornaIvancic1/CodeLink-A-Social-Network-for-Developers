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
    public class FriendController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public FriendController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("GetFriends/{loggedInUserId}")]
        public async Task<ActionResult<List<User>>> GetFriends(int loggedInUserId)
        {
      
            var friendIds = await _dbContext.Friend
                .Where(f => f.UserId == loggedInUserId)
                .Select(f => f.FriendId)
                .ToListAsync();

            if (friendIds == null || friendIds.Count == 0)
            {
                return NotFound("No friends found for the given user.");
            }

     
            var friends = await _dbContext.User
                .Where(u => friendIds.Contains(u.Id))
                .ToListAsync();

            if (friends == null || friends.Count == 0)
            {
                return NotFound("No users found for the given friend IDs.");
            }

            return friends;
        }

        [HttpPost("AddFriend")]
        public async Task<ActionResult> AddFriend([FromForm]int userId, [FromForm] int friendId)
        {
        
            var existingFriendship = await _dbContext.Friend.FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);

            if (existingFriendship != null)
            {
                return Conflict("Friendship already exists.");
            }

     
            var friendship = new Friend { UserId = userId, FriendId = friendId };

            _dbContext.Friend.Add(friendship);
            await _dbContext.SaveChangesAsync();

            return Ok("Friend added successfully.");
        }

        [HttpDelete("RemoveFriend")]
        public async Task<ActionResult> RemoveFriend([FromForm] int userId, [FromForm] int friendId)
        {
           
            var friendship = await _dbContext.Friend.FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);

            if (friendship == null)
            {
                return NotFound("Friendship not found.");
            }

            _dbContext.Friend.Remove(friendship);
            await _dbContext.SaveChangesAsync();

            return Ok("Friend removed successfully.");
        }
    }
}
