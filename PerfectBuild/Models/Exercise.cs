using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models
{
    public partial class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(45, ErrorMessage = "Exercise's name length should be less then 40 chars")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Exercise's description length should be less then 250 chars")]
        public string Description { get; set; }
    }
}




