using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectBuild.Data;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class TrainingProgramController : Controller
    {
        readonly ApplicationContext appContext;
        readonly UserManager<User> userManager;
        string userId;

        public TrainingProgramController(ApplicationContext appContext, UserManager<User> userManager)
        {
            this.appContext = appContext;
            this.userManager = userManager;
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
            var specCollection = appContext.TrainingProgramSpecs.Include(x => x.Exercise).Where(x => x.ProgramHeadId == headId).Select(selector => new {
                id=selector.Id, exercise=selector.Exercise.Name,set=selector.Set, weight=selector.Weight, amount=selector.Amount }).ToList();

            var spec = Json(specCollection);
            return spec;
        }

        private List<TrainingProgramHead> GetTrainigPrograms()
        {
            userId = userManager.GetUserId(HttpContext.User);
            return appContext.TrainingProgramHeads.Where(x=>x.UserId==userId).Include(x => x.Category).Include(x=>x.TrainingProgramSpec).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTrainingProgram(int id)
        {
            if (id != 0)
            {
                var specList = appContext.TrainingProgramSpecs.Where(x => x.ProgramHeadId == id);
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
            TrainigProgramHeadViewModel trHeadViewModel = new TrainigProgramHeadViewModel
            {
                Categories = appContext.Categories.ToList(),
                Exercises = appContext.Exercises.ToList()
            };
            if (id != 0)
            {
                trHeadViewModel.Id = id;
                trHeadViewModel.Name = appContext.TrainingProgramHeads.Find(id).Name;
                trHeadViewModel.Date = appContext.TrainingProgramHeads.Find(id).Date;
                trHeadViewModel.Description = appContext.TrainingProgramHeads.Find(id).Description;
                trHeadViewModel.CategoryId = appContext.TrainingProgramHeads.Find(id).CategoryId;
            }
            return View(trHeadViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyTrainingProgram(TrainigProgramHeadViewModel viewModel)
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
                        Date = viewModel.Date,
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
                    var spec = appContext.TrainingProgramSpecs.Include(x => x.Exercise).Where(x => x.Id == specId).ToList().FirstOrDefault();
                    line.Id = spec.Id;
                    line.Name = spec.Exercise.Name;
                    line.Set = spec.Set;
                    line.Weight = spec.Weight;
                    line.Amount = spec.Amount;
                }
                return View(line);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyTrainingSpecLine(TrainigSpecLineChangeViewModel viewModel)
        {
            if (viewModel == null)
            {
                ModelState.AddModelError("Incorrect TrainigSpecLineChangeViewModel", "Incorrect TrainigSpecLineChangeViewModel");
                return RedirectToAction("List");
            }
            if (viewModel.Id == 0)
            {
                TrainingProgramSpec programSpec = new TrainingProgramSpec
                {
                    ProgramHeadId = viewModel.HeadId,
                    ExId = viewModel.ExerciseId,
                    Set = viewModel.Set,
                    Weight = viewModel.Weight,
                    Amount = viewModel.Amount
                };
                appContext.TrainingProgramSpecs.Add(programSpec);
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

        private AddModifyTrainingSpecViewModel<TrainingProgramSpec> Getmodel(int headId)
        {
            var model = new AddModifyTrainingSpecViewModel<TrainingProgramSpec>
            {
                TrainingHeadName = appContext.TrainingProgramHeads.Where(x => x.Id == headId).FirstOrDefault().Name,
                TrainingHeadCategory = appContext.TrainingProgramHeads.Where(x => x.Id == headId).Include(x => x.Category).FirstOrDefault().Category.Name,
                TrainingSpecs = appContext.TrainingProgramSpecs.Where(x => x.ProgramHeadId == headId).Include(x => x.Exercise).ToList(),
                TrainingHeadId = headId,
                TrainingHeadDate = appContext.TrainingProgramHeads.Where(x => x.Id == headId).FirstOrDefault().Date
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
