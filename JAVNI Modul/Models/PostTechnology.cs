using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JAVNI_Modul.Models
{
    public class PostTechnology
    {
        [Required]
        public string Name { get; set; }
        

        [Required]

        public string SkillLevel
        { get; set; }
    }
}