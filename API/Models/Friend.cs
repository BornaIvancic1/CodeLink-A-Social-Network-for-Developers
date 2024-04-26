using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FriendId { get; set; }
    }
}
