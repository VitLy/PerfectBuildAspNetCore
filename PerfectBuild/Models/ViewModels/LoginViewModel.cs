using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="LoginRequired")]
        public string Login { get; set; }
        [Required(ErrorMessage = "PasswordRequired")]
        public string Password { get; set; }
    }
}
