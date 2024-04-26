using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PostController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _dbContext.Post.ToListAsync();
            if (posts == null || !posts.Any())
            {
                return NotFound();
            }

            return posts;
        }

     
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _dbContext.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        
        [HttpPost("UserPost")]
        public async Task<ActionResult<Post>> PostPost([FromForm] PostPost postPost)
        {
            Post newPost;
            if (postPost.UserId != null)
            {
                newPost = new Post
                {
                    UserId = postPost.UserId,
                    PostImage = postPost.PostImage,
                    PublishingDate = DateTime.Now,
                    Title = postPost.Title,
                    Description = postPost.Description
                };
            }
          
            else
            {
            return BadRequest("UserId or AdminId must be provided.");
            }

            _dbContext.Post.Add(newPost);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = newPost.Id }, newPost);
        }

        [HttpPut("UserPostUpdate")]
        public async Task<ActionResult<Post>> PutPost([FromForm] PutPost postPost)
        {
            var existingPost = await _dbContext.Post.FindAsync(postPost.Id);

            if (existingPost == null)
            {
                return NotFound("Post not found.");
            }

            if (postPost.PostImage != "None")
            {
                existingPost.PostImage = postPost.PostImage;
            }
         
            existingPost.PublishingDate = DateTime.Now;
            existingPost.Title = postPost.Title;
            existingPost.Description = postPost.Description;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(postPost.Id))
                {
                    return NotFound("Post not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            var post = await _dbContext.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _dbContext.Post.Remove(post);
            await _dbContext.SaveChangesAsync();

            return post;
        }

        private bool PostExists(int id)
        {
            return _dbContext.Post.Any(e => e.Id == id);
        }
    }
}
