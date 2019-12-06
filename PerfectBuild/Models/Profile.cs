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

        public string UserId { get; set; } // заменил с int на string, т.к. User:IdentityUser,   IdentityUser: IdentityUser<string>

        [ForeignKey("UserId")]
        public User User { get; set; }

        [MaxLength(15), Required]
        public string FName { get; set; }

        [MaxLength(15), Required]
        public string LName { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public DateTime DayBirth { get; set; }

        [Required]
        public byte Height { get; set; }

        [Required]
        public float Weight { get; set; }
    }
}
