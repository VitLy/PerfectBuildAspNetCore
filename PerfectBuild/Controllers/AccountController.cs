using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PerfectBuild.Infrastructure;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IStringLocalizer<SharedResource> localizer;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IStringLocalizer<SharedResource> localizer, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.localizer = localizer;
            this.roleManager = roleManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginFacebook(string returnUrl)
        {
            string redirectUrl = Url.Action(nameof(FacebookResponse), "Account", new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> FacebookResponse(string returnUrl)
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl??Url.Action("Index","Home"));
            }
            else
            {
                string userName = (info.Principal.FindFirst(ClaimTypes.GivenName).Value +
                    info.Principal.FindFirst(ClaimTypes.Surname).Value).Substring(0,10).Trim();
                User user = new User
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = userName
                };
                IdentityResult identityResult = await userManager.CreateAsync(user);
                if (identityResult.Succeeded)
                {
                    var res = await userManager.AddLoginAsync(user, info);
                    var roleResult = await userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        var signInExternalresult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
                        return RedirectToAction("Modify", "Profile");
                    }
                }
                else
                {
                    List<string> signInErrors = new List<string>();
                    foreach (var item in identityResult.Errors)
                    {
                        signInErrors.Add(item.Code);
                    }
                    return View("Errors", signInErrors);
                }
            }
            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
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
                        return RedirectToAction("Index", "Home");
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

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountModel accountCreateModel, string password)
        {
            if (accountCreateModel != null)
            {
                string userLogin = accountCreateModel.Login;
                if (!string.IsNullOrWhiteSpace(userLogin))
                {
                    User user = await userManager.FindByNameAsync(userLogin);
                    if (user == null)
                    {
                        User createduser = new User { UserName = accountCreateModel.Login, Email = accountCreateModel.Email };
                        IdentityResult addUserResult = await userManager.CreateAsync(createduser, accountCreateModel.Password);
                        if (addUserResult.Succeeded)
                        {
                            string role = "User";
                            if (!await roleManager.RoleExistsAsync(role))
                            {
                                await roleManager.CreateAsync(new IdentityRole { Name = role });
                            }
                            await userManager.AddToRoleAsync(createduser, role);

                            await signInManager.SignOutAsync();
                            await signInManager.SignInAsync(createduser, false);
                            return RedirectToAction("Index", "Home");

                        }
                        else AddErrors(addUserResult.Errors);
                    }
                    else
                    {
                        ModelState.AddModelError(localizer["UserIsInDatabase"], localizer["UserIsInDatabase"]);
                    }
                }
                else
                {
                    ModelState.AddModelError(localizer["NullLoginPasswordShort"], localizer["NullLoginPasswordLong"]);
                }
            }
            return RedirectToAction("Create", accountCreateModel);
        }

        #region PrivateMethods
        private void AddErrors(IEnumerable<IdentityError> errors)
        {
            foreach (var item in errors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
        }


        #endregion
    }
}
