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
    [Authorize(Roles = "User,Admin")]
    public class ActivityController : Controller
    {
        private readonly ApplicationContext appContext;
        private UserManager<User> userManager;

        public ActivityController(UserManager<User> userManager, ApplicationContext appContext)
        {
            this.appContext = appContext;
            this.userManager = userManager;
        }
        // TODO: "Подумать, стоит ли в этой же форме показывать пользователю предыдущие результаты или лучше выводить дату и нули во всех полях,
        //а данные выводить только в том случае, если они есть за этот день "

        [HttpGet]
        public IActionResult DailyParameters()
        {
            var user = HttpContext.User;
            ParametersViewModel viewModel = new ParametersViewModel {Date = DateTime.Now};
            string userId = userManager.GetUserId(user);
            var currentUserParam = appContext.Params.Where(x => x.UserId.Equals(userId) & x.Date.Date.Equals(DateTime.UtcNow.Date)).FirstOrDefault();

            if (currentUserParam != null) // на эту дату есть уже внесенные данные в базе данных
            {
                viewModel = new ParametersViewModel
                {
                    Date = currentUserParam.Date.ToLocalTime(),
                    Breast = currentUserParam.Breast,
                    Pelvis = currentUserParam.Buttock,
                    Thigh = currentUserParam.Thigh,
                    Waist = currentUserParam.Waist,
                    Weight = currentUserParam.Weight
                };
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DailyParameters(ParametersViewModel viewModel)
        {
            var user = HttpContext.User;
            string userId = userManager.GetUserId(user);

            var userParam = appContext.Params.Where(x => x.UserId.Equals(userId) & x.Date.Date.Equals(viewModel.Date.ToUniversalTime().Date)).FirstOrDefault();

            if (userParam == null)
            {
                await appContext.Params.AddAsync(
                    new Param
                    {
                        UserId = userId,
                        Date = viewModel.Date.ToUniversalTime(),
                        Weight = viewModel.Weight,
                        Breast = viewModel.Breast,
                        Waist = viewModel.Waist,
                        Buttock = viewModel.Pelvis,
                        Thigh = viewModel.Thigh
                    });
            }
            else
            {
                userParam.Date = viewModel.Date.ToUniversalTime();
                userParam.Weight = viewModel.Weight;
                userParam.Breast = viewModel.Breast;
                userParam.Waist = viewModel.Waist;
                userParam.Buttock = viewModel.Pelvis;
                userParam.Thigh = viewModel.Thigh;
                appContext.Params.Update(userParam);
            }
            await appContext.SaveChangesAsync();
            return View(viewModel);
        }
    }
}
