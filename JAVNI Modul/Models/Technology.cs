using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAVNI_Modul.Models
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