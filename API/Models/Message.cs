using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Message
    {

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public int SenderId { get; set; }
        [NotMapped]
        
        public User Sender { get; set; } 

        public int ReceiverId { get; set; }
        [NotMapped]
        public User Receiver { get; set; } 
    }
}
