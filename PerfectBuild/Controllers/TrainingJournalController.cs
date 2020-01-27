using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerfectBuild.Data;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using PerfectBuild.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class TrainingJournalController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext appContext;

        public TrainingJournalController(UserManager<User> userManager, ApplicationContext appContext)
        {
            this.userManager = userManager;
            this.appContext = appContext;
        }

        [HttpGet]
        public IActionResult List()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            return View(appContext.TrainingHeads.Where(x => x.UserId.Equals(userId)).ToList());
        }

        [HttpGet]
        public IActionResult Details(int headId=0)
        {
            if (headId != 0)
            {
                return View(appContext.TrainingSpecs.Where(x => x.HeadId.Equals(headId)).OrderBy(x=>x.Set).ToList());
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult AddTrainingManually()
        {
            var exercises = appContext.Exercises.ToList();
            var model = new AddTrainingManuallyViewModel
            {
                HeadId = 0,
                Exercises = exercises,
                Date = DateTime.UtcNow,
                Spec = new List<TrainingSpec>()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddTrainingManually(AddTrainingManuallyViewModel model)
        {
           
            return View(model);
        }
    }
}
