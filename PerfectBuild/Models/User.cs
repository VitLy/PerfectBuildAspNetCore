using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models
{
    public partial class User:IdentityUser
    {
        //[Key]
        //public int Id { get; set; }

        //[Required,MaxLength(10,ErrorMessage ="Max length of the Login is 10 chars")]
        //public string Login { get; set; }

        //[Required, MaxLength(10, ErrorMessage = "Max length of the Password is 10 chars")]
        //public string Password { get; set; }
    }
}




