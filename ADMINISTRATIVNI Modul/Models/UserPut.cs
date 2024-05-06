using System.Collections.Generic;

namespace API.Models
{
    public class UserPut
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string username { get; set; }
        public List<Technology> knownTechnologies { get; set; }
        public string userProfilePictureBase64 { get; set; }
        public string password { get; set; }
     
    }
}
