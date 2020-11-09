using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PerfectBuild.Data;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using PerfectBuild.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [AutoValidateAntiforgeryToken]
    public class TrainingProgramController : Controller
    {
        readonly ApplicationContext appContext;
        readonly UserManager<User> userManager;
        private readonly SpecLineValidator specValidator;
        private readonly IStringLocalizer<TrainingProgramController> localizer;
        string userId;

        public TrainingProgramController(ApplicationContext appContext, UserManager<User> userManager, SpecLineValidator specValidator, IStringLocalizer<TrainingProgramController> localizer)
        {
            this.appContext = appContext;
            this.userManager = userManager;
            this.specValidator = specValidator;
            this.localizer = localizer; ;
    }

        [HttpGet]
        public IActionResult List()
        {
            List<TrainingProgramHead> trainingProgramHeads = GetTrainigPrograms();
            return View(trainingProgramHeads);
        }

        [HttpGet]
        public IActionResult GetSpecLine(int headId)
        {
            var specCollection = appContext.TrainingProgramSpecs.Include(x => x.Exercise).Where(x => x.HeadId == headId).Select(selector => new {
                id=selector.Id, exercise=selector.Exercise.Name,set=selector.Set, weight=selector.Weight, amount=selector.Amount }).ToList();

            var spec = Json(specCollection);
            return spec;
        }

        private List<TrainingProgramHead> GetTrainigPrograms()
        {
            userId = userManager.GetUserId(HttpContext.User);
            return appContext.TrainingProgramHeads.Where(x=>x.UserId==userId).Include(x => x.Category).Include(x=>x.TrainingProgramSpecs).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTrainingProgram(int id)
        {
            if (id != 0)
            {
                var specList = appContext.TrainingProgramSpecs.Where(x => x.HeadId == id);
                appContext.RemoveRange(specList);
                await appContext.SaveChangesAsync();
                var head = appContext.TrainingProgramHeads.Where(x => x.Id == id);
                appContext.RemoveRange(head);
                await appContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult AddModifyTrainingProgram(int id = 0)
        {
            TrainingProgramHeadViewModel trHeadViewModel = new TrainingProgramHeadViewModel
            {
                Date=DateTime.Now.ToLocalTime(),
                Categories = appContext.Categories.ToList(),
                Exercises = appContext.Exercises.ToList()
            };
            if (id != 0)
            {
                trHeadViewModel.Id = id;
                trHeadViewModel.Name = appContext.TrainingProgramHeads.Find(id).Name;
                trHeadViewModel.Date = appContext.TrainingProgramHeads.Find(id).Date.ToLocalTime();
                trHeadViewModel.Description = appContext.TrainingProgramHeads.Find(id).Description;
                trHeadViewModel.CategoryId = appContext.TrainingProgramHeads.Find(id).CategoryId;
            }
            return View(trHeadViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyTrainingProgram(TrainingProgramHeadViewModel viewModel)
        {
            TrainingProgramHead trPrHead = new TrainingProgramHead();
            userId = userManager.GetUserId(HttpContext.User);
            int headId = viewModel.Id;
            if (viewModel != null)
            {
                if (viewModel.Id == 0)
                {
                    if (appContext.TrainingProgramHeads.Where(x => x.Name == viewModel.Name & x.Date == viewModel.Date & x.UserId == userId).FirstOrDefault() != null)
                    {
                        ModelState.AddModelError("Trainig program is existed", "Trainig program is existed");
                        return RedirectToAction("List");
                    };
                    trPrHead = new TrainingProgramHead
                    {
                        Name = viewModel.Name,
                        CategoryId = viewModel.CategoryId,
                        Date = viewModel.Date.ToUniversalTime(),
                        UserId = userId,
                        Description = viewModel.Description,
                        Category = new Category
                        {
                            Id = viewModel.CategoryId,
                            Name = appContext.Categories.ToList().Where(x => x.Id == viewModel.CategoryId).FirstOrDefault().Name
                        }
                    };
                    await appContext.TrainingProgramHeads.AddAsync(trPrHead);
                    await appContext.SaveChangesAsync();
                    headId = trPrHead.Id;
                }
                else
                {
                    trPrHead = new TrainingProgramHead
                    {
                        Id = headId,
                        CategoryId = viewModel.CategoryId,
                        Name = viewModel.Name,
                        Date = viewModel.Date,
                        Description = viewModel.Description,
                        UserId = userId
                    };
                    appContext.Update(trPrHead);
                    await appContext.SaveChangesAsync();
                }
                    return View("TrainingSpecList", Getmodel(headId));
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult AddModifyTrainingSpecLine(int headId, int specId = 0)
        {
            if (headId == 0)
            {
                ModelState.AddModelError("Incorrect Head Id", "Incorrect Head Id");
            }
            else
            {
                var line = new TrainigSpecLineChangeViewModel()
                {
                    HeadId = headId,
                    Exercises = appContext.Exercises.ToList(),
                };
                if (specId != 0)   //ветка модификации существующей позиции
                {
                    ViewBag.Tittle = localizer["TittleModify"];
                    var spec = appContext.TrainingProgramSpecs.Include(x => x.Exercise).Where(x => x.Id == specId).ToList().FirstOrDefault();
                    line.Id = spec.Id;
                    line.Name = spec.Exercise.Name;
                    line.Set = spec.Set;
                    line.Weight = spec.Weight;
                    line.Amount = spec.Amount;
                }
                else 
                {
                    ViewBag.Tittle = localizer["TittleAdd"];
                }
                return View(line);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyTrainingSpecLine(TrainigSpecLineChangeViewModel viewModel)
        {
            ViewBag.Tittle = viewModel.Id==0?localizer["TittleAdd"]:localizer["TittleModify"];

            if (viewModel == null)
            {
                ModelState.AddModelError("Incorrect TrainigSpecLineChangeViewModel", "Incorrect TrainigSpecLineChangeViewModel");
                return RedirectToAction("List");
            }

            var exercises = appContext.Exercises.ToList();
            if (!specValidator.IsSpecLineHasCorrectWeight(viewModel.ExerciseId, viewModel.Weight,exercises, out string shortMessage, out string longMessage)) 
            {
                ModelState.AddModelError(shortMessage, longMessage);
            }
            if (ModelState.IsValid)
            {
                if (viewModel.Id == 0)
                {
                    TrainingProgramSpec programSpec = new TrainingProgramSpec
                    {
                        HeadId = viewModel.HeadId,
                        ExId = viewModel.ExerciseId,
                        Set = viewModel.Set,
                        Weight = viewModel.Weight,
                        Amount = viewModel.Amount
                    };

                    await appContext.TrainingProgramSpecs.AddAsync(programSpec);
                    await appContext.SaveChangesAsync();
                }
                else
                {
                    TrainingProgramSpec programSpec = await appContext.TrainingProgramSpecs.FindAsync(viewModel.Id);
                    programSpec.ExId = viewModel.ExerciseId;
                    programSpec.Set = viewModel.Set;
                    programSpec.Weight = viewModel.Weight;
                    programSpec.Amount = viewModel.Amount;
                    appContext.Update(programSpec);
                    await appContext.SaveChangesAsync();
                }

                return View("TrainingSpecList", Getmodel(viewModel.HeadId));
            }
            viewModel.Exercises = exercises;
            return View(viewModel);
        }

        private AddModifyTrainingSpecViewModel Getmodel(int headId)
        {
            var model = new AddModifyTrainingSpecViewModel
            {
                TrainingHeadName = appContext.TrainingProgramHeads.Where(x => x.Id == headId).FirstOrDefault().Name,
                TrainingHeadCategory = appContext.TrainingProgramHeads.Where(x => x.Id == headId).Include(x => x.Category).FirstOrDefault().Category.Name,
                TrainingSpecs = appContext.TrainingProgramSpecs.Where(x => x.HeadId == headId).Include(x => x.Exercise).ToList(),
                TrainingHeadId = headId,
                TrainingHeadDate = appContext.TrainingProgramHeads.Where(x => x.Id == headId).FirstOrDefault().Date.ToLocalTime()
            };
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTrainingSpecLine(int headId, int specId = 0)
        {
            if (specId != 0)
            {
                var deleteSpec = appContext.TrainingProgramSpecs.Find(specId);
                appContext.TrainingProgramSpecs.Remove(deleteSpec);
                await appContext.SaveChangesAsync();
            }
            return View("TrainingSpecList", Getmodel(headId));
        }
    }
}
