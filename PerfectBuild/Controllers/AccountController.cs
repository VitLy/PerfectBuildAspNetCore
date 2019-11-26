using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PerfectBuild.Model;
using PerfectBuild.Models.ViewModels;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{

    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IStringLocalizer<AccountController> localizer;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,IStringLocalizer<AccountController> localizer )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.localizer = localizer;
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string returnUrl, LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(loginModel.Login);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    var signInResult = await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        Redirect(returnUrl ?? "/");
                    }
                    else
                    {
                        ModelState.AddModelError(localizer["IncorrectLoginPasswordShort"], localizer["IncorrectLoginPasswordLong"]);
                    }
                }
                else
                {
                    ModelState.AddModelError(localizer["NullLoginPasswordShort"], localizer["NullLoginPasswordLong"]);
                }
            }
            return View(loginModel);
        }
    }
}
