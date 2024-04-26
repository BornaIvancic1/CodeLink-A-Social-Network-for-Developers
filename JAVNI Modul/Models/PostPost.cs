using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAVNI_Modul.Models
{
    public class PostPost
    {
   
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    
        [Required]
        public string PostImage { get; set; }


        [ForeignKey("User")]
        public int? UserId { get; set; }

    }
}
