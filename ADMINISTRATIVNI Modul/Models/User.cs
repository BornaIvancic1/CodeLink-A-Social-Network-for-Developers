using System.Collections.Generic;

namespace API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public List<Technology> KnownTechnologies { get; set; }
        public string UserProfilePictureBase64 { get; set; }
        public string PwdHash { get; set; }
        public string PwdSalt { get; set; }
     
    }
}
