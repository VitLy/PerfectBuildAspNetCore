using Microsoft.AspNetCore.Mvc;

namespace PerfectBuild.Components
{
    public class LoginPanel : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string user = HttpContext.User.Identity.Name;
            return View("default",user??"None");
        }
    }
}
