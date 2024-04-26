using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class MessagePost
    {

      
        public string Content { get; set; }
     

        public int SenderId { get; set; }
    

        public int ReceiverId { get; set; }

    }
}
