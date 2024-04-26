using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JAVNI_Modul.Models
{
    public class User
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
        public List<Technology> KnownTechnologies { get; set; }
        [Required]
        public string UserProfilePictureBase64 { get; set; }

        [Required]
        public string? PwdHash { get; set; }
        [Required]
        public string? PwdSalt { get; set; }
     
    }
}
