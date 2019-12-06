using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PerfectBuild.Data;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;

namespace PerfectBuild.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext appContext;
        UserProfileModel userProfileModel;
        Profile profile;
        public ProfileController(UserManager<User> userManager, ApplicationContext appContext)
        {
            this.userManager = userManager;
            this.appContext = appContext;
        }

        [HttpGet]
        public IActionResult Modify()
        {
            string userId = userManager.GetUserId(HttpContext.User);
            profile= appContext.Profiles.Where(x => x.UserId == userId).FirstOrDefault();
            if (profile == null)
            {
                userProfileModel = new UserProfileModel { Profile = new Profile { UserId = userId } };
            }
            else
            {
                userProfileModel = new UserProfileModel { Profile = profile };
            }
            return View(userProfileModel);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(UserProfileModel model)
        {
            if (model != null) 
            {
                if (ModelState.IsValid) 
                {
                    profile = appContext.Profiles.Where(x => x.UserId == model.Profile.UserId).FirstOrDefault();
                    if (profile == null) 
                    {
                        var result = await appContext.Profiles.AddAsync(model.Profile);
                        await appContext.SaveChangesAsync();
                    }
                    else 
                    {
                        profile.FName = model.Profile.FName;
                        profile.LName = model.Profile.LName;
                        profile.DayBirth = model.Profile.DayBirth;
                        profile.Sex = model.Profile.Sex;
                        profile.Weight = model.Profile.Weight;
                        profile.Height = model.Profile.Height;
                        appContext.Update(profile);
                        await appContext.SaveChangesAsync();
                        model.Profile = profile;
                    }
                }
            }
            return View(model);
        }

    }
}