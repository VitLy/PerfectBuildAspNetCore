using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class CreateAccountModel
    {
        [Required(ErrorMessage = "LoginRequired")]
        public string Login { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "PasswordsMismatch")]
        public string PasswordConfirm { get; set; }

        [EmailAddress(ErrorMessage = "EmailIncorrect")]
        [Required(ErrorMessage = "EmailRequired")]
        public string Email { get; set; }
    }
}
