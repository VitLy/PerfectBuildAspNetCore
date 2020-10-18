using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Models
{
    public enum Sex
    {
        Man,Woman
    }

    //[Table("UserProfile")]
    public partial class Profile
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } 

        [ForeignKey("UserId")]
        public User User { get; set; }

        [MaxLength(15,ErrorMessage ="FNameMaxLength"), Required(ErrorMessage ="FNameRequired")]
        public string FName { get; set; }

        [MaxLength(15,ErrorMessage ="LNameMaxLength"), Required(ErrorMessage ="LNameRequired")]
        public string LName { get; set; }

        [Required(ErrorMessage ="SexRequired")]
        public Sex Sex { get; set; }

        [Required(ErrorMessage ="DayBirthRequired")]
        public DateTime DayBirth { get; set; }

        [Required(ErrorMessage ="HeightRequired")]
        [Range(50,300,ErrorMessage ="HeightRange")]
        public byte Height { get; set; }

        [Required(ErrorMessage ="WeightRequired")]
        [Range(20,600,ErrorMessage ="WeightRange")]
        public float Weight { get; set; }
    }
}
