using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class PutAdminPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ?Title { get; set; }
        [Required]
        public string ?Description { get; set; }
    
        [Required]
        public string ?PostImage { get; set; }


        [ForeignKey("Admin")]
        public int? AdminId { get; set; }

     
    }
}
