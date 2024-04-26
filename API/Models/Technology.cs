using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Technology
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
      
        [Required]
     
        public string SkillLevel
        { get; set; }
    }
}