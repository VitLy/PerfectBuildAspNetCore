using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerfectBuild.Data;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles ="User,Admin")]
    public class ActivityController : Controller
    {
        private readonly ApplicationContext appContext;
        private UserManager<User> userManager;

        public ActivityController(UserManager<User> userManager, ApplicationContext appContext)
        {
            this.appContext = appContext;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult DailyParameters()
        {
            var user = HttpContext.User;
            string userId = userManager.GetUserId(user);
            Param userParam = appContext.Params.Where(x => x.UserId == userId).FirstOrDefault();
            FillParametersViewModel viewModel = new FillParametersViewModel
            {
                Date = DateTime.Today
            };
            if (userParam != null)
            {
                viewModel.Weight = userParam.Weight;
                viewModel.Breast = userParam.Breast;
                viewModel.Waist = userParam.Waist;
                viewModel.Buttock = userParam.Buttock;
                viewModel.Thigh = userParam.Thigh;
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DailyParameters(FillParametersViewModel viewModel)
        {
            var user = HttpContext.User;
            string userId = userManager.GetUserId(user);
            Param userParam = appContext.Params.Where(x => x.UserId == userId).FirstOrDefault();
            if (userParam == null)
            {
                await appContext.Params.AddAsync(
                    new Param
                    {
                        UserId = userId,
                        Date = viewModel.Date,
                        Weight = viewModel.Weight,
                        Breast = viewModel.Breast,
                        Waist = viewModel.Waist,
                        Buttock = viewModel.Buttock,
                        Thigh = viewModel.Thigh
                    });
            }
            else
            {
                userParam.Date = viewModel.Date;
                userParam.Weight = viewModel.Weight;
                userParam.Breast = viewModel.Breast;
                userParam.Waist = viewModel.Waist;
                userParam.Buttock = viewModel.Buttock;
                userParam.Thigh = viewModel.Thigh;
                appContext.Params.Update(userParam);
            }
            await appContext.SaveChangesAsync();
            return View(viewModel);
        }
    }
}
