using Microsoft.EntityFrameworkCore.Query;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ?Title { get; set; }

        [Required]
        public string ?Description { get; set; }

        [Required]
        public DateTime PublishingDate { get; set; }

        [Required]
        public string ?PostImage { get; set; }

     
        [ForeignKey("User")]
        public int? UserId { get; set; }

        [NotMapped]
        public User ?User { get; set; }

        
        [ForeignKey("Admin")]
        public int? AdminId { get; set; }

        [NotMapped]
        public Admin ?Admin { get; set; }

       
        [NotMapped]
        public bool IsUserPost
        {
            get { return UserId != null; }
        }
    }
}
