using System;

namespace API.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishingDate { get; set; }
        public string PostImage { get; set; }
        public int? UserId { get; set; }
        public int? AdminId { get; set; }
       
        public bool IsUserPost
        {
            get { return UserId != null; }
        }
    }
}
