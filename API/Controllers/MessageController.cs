using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public MessageController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

     
        [HttpGet("{senderId}/{receiverId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(int senderId, int receiverId)
        {
           
            var messages = await _dbContext.Message
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == senderId))
                .ToListAsync();

            return messages;
        }

     
        [HttpPost("{senderId}/{receiverId}/{Content}")]
        public async Task<ActionResult<Message>> SendMessage(int senderId, int receiverId, string Content)
        {
            try
            {
                var message = new Message
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Content = Content,
                 Timestamp = DateTime.Now,
                };



                _dbContext.Message.Add(message);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMessages), new { senderId = message.SenderId, receiverId = message.ReceiverId }, message);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to send message", error = ex.Message });
            }
        }
    }
}
