using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerfectBuild.Models;
using System.Threading.Tasks;

namespace PerfectBuild.Components
{
    public class LoginPanel : ViewComponent
    {
        private readonly UserManager<User> userManager;
        public LoginPanel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            //var currentUser = HttpContext.User;
            string user = HttpContext.User.Identity.Name;
            return View("default",user??"None");
        }
    }
}
