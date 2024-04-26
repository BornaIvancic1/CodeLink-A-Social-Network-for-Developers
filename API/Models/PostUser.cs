using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class PostUser
    {
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
        public List<PostTechnology> KnownTechnologies { get; set; }
        [Required]
        public string UserProfilePictureBase64 { get; set; }

        [JsonPropertyName("password")]
        public string? PwdHash { get; set; }
    }
}
