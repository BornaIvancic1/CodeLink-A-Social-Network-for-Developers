using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string UserProfilePictureBase64 { get; set; }

        [JsonPropertyName("password")]
        public string? PwdHash { get; set; }
        [JsonIgnore]
        public string? PwdSalt { get; set; }
 
    }
}
