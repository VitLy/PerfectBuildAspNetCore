using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PerfectBuild.Data;
using PerfectBuild.Models;
using PerfectBuild.Models.Interfaces;
using PerfectBuild.Models.ViewModels;
using PerfectBuild.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class TrainingController : Controller
    {
        private const int stepTrainingHeadDocument = 1;

        private readonly ApplicationContext appContext;
        private readonly UserManager<User> userManager;
        private readonly ITrainigDayConverter dayConverter;

        public TrainingController(ApplicationContext appContext, UserManager<User> userManager, ITrainigDayConverter dayConverter,
            IStringLocalizer<TrainingController> localizer, DocumentSpecHandler<TrainingPlanSpec> specHandler)
        {
            this.appContext = appContext;
            this.userManager = userManager;
            this.dayConverter = dayConverter;
        }

        [HttpGet]
        public IActionResult Start()
        {
            DayOfWeek currentDay = DateTime.UtcNow.DayOfWeek;
            string userId = userManager.GetUserId(HttpContext.User);
            var trainingPlan = appContext.TrainingPlanHeads
                .Where(x => x.TrainingDays.Equals(dayConverter.DaysToByte(currentDay)))
                ?.ToList().OrderBy(x => x.Date)
                .LastOrDefault();
            TrainigPlanViewModel model = new TrainigPlanViewModel
            {
                CurrentTrainingDay = currentDay
            };
            if (appContext.TrainingHeads.Where(x => x.TrainingPlanHeadId.Equals(trainingPlan.Id)).ToList().FirstOrDefault() != null)
            {
                ModelState.AddModelError("Training already exists", "Training already exists. To change it, pass to the training journal");
                return View("Error");
            }

            if (trainingPlan == null)
            {
                ModelState.AddModelError("Program plan not found ", "");
                return View("Error");
            }
            else
            {
                List<TrainingPlanSpec> planSpec = appContext.TrainingPlanSpecs.Include(x => x.Exercise)
                    .Where(x => x.HeadId.Equals(trainingPlan.Id))
                    .OrderBy(x => x.Order)
                    .ToList();
                model = new TrainigPlanViewModel
                {
                    CurrentTrainingDay = currentDay,
                    Id = trainingPlan.Id,
                    Date = trainingPlan.Date,
                    Description = trainingPlan.Description,
                    Name = trainingPlan.Name,
                    Lines = planSpec,
                };
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Start(int currentHeadPlanId)
        {
            string userId = userManager.GetUserId(HttpContext.User);
            if (currentHeadPlanId != 0)
            {
                TrainingPlanHead trainingPlanHead = appContext.TrainingPlanHeads.Find(currentHeadPlanId);
                if (trainingPlanHead != null)
                {
                    TrainingHead trainingHead = new TrainingHead
                    {
                        Date = DateTime.UtcNow,
                        TrainingPlanHeadId = currentHeadPlanId,
                        Name = trainingPlanHead.Name,
                        Number = GetNumberDocument<TrainingHead>(stepTrainingHeadDocument, userId),
                    };
                    await appContext.TrainingHeads.AddAsync(trainingHead);
                    await appContext.SaveChangesAsync();
                    int trainingHeadId = trainingHead.Id;
                   
                    int? currentSpecPlanId = appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(currentHeadPlanId)).OrderBy(x => x.Order).FirstOrDefault()?.Id;
                    currentSpecPlanId = currentSpecPlanId ?? 0;
                    return RedirectToAction("NextStep", new { currentSpecPlanId, trainingPlanHeadId=currentHeadPlanId, trainingHeadId });
                }
                else
                {
                    ModelState.AddModelError("TrainingPlan not Found", "TrainingPlan not Found");
                }
            }
            else
            {
                ModelState.AddModelError(nameof(currentHeadPlanId) + "can not be null", nameof(currentHeadPlanId) + "can not be null");
            }
            return View();
        }

        [HttpGet]
        public IActionResult NextStep(int currentSpecPlanId = 0, int trainingPlanHeadId = 0, int trainingHeadId = 0)
        {
            if (currentSpecPlanId == 0)
            {
                return RedirectToAction("Finish", new { trainingHeadId });
            }

            var currentSpecPlanLine = appContext.TrainingPlanSpecs.Include(x=>x.Exercise).Where(x=>x.Id.Equals(currentSpecPlanId)).FirstOrDefault();

            if (currentSpecPlanLine != null)
            {
                string userId = userManager.GetUserId(HttpContext.User);

                TrainingSpec currentSpecLine = GetCurrentSpecLine(currentSpecPlanLine, trainingHeadId);

                TrainingStepViewModel model;
                if (currentSpecLine == null)
                {
                    model = new TrainingStepViewModel
                    {
                        HeadTrainingPlanId = trainingPlanHeadId,
                        HeadTrainingId = trainingHeadId,
                        CurrentSpecPlanId = currentSpecPlanId,
                        CurrentSpecId = 0,
                        Set = currentSpecPlanLine.Set,
                        Weight = currentSpecPlanLine.Weight,
                        ExerciseId = currentSpecPlanLine.ExId,
                        Exercise = currentSpecPlanLine.Exercise.Name,
                        ExerciseDescription = currentSpecPlanLine.Exercise.Description,
                        Amount = currentSpecPlanLine.Amount,
                        Order = currentSpecPlanLine.Order
                    };
                }
                else
                {
                    model = new TrainingStepViewModel
                    {
                        HeadTrainingPlanId = trainingPlanHeadId,
                        HeadTrainingId = trainingHeadId,
                        CurrentSpecPlanId = currentSpecPlanId,
                        CurrentSpecId = currentSpecLine.Id,
                        Set = currentSpecLine.Set,
                        Weight = currentSpecLine.Weight,
                        ExerciseId = currentSpecLine.ExId,
                        Exercise = currentSpecLine.Exercise.Name,
                        ExerciseDescription = currentSpecLine.Exercise.Description,
                        Amount = currentSpecLine.Amount,
                        Order = currentSpecLine.Order
                    };
                }
                return View(model);
            }
            ModelState.AddModelError("Trainig specification line not found", "Trainig specification line not found");
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> NextStep(TrainingStepViewModel model)
        {
            string userId = userManager.GetUserId(HttpContext.User);
            var currentSpecPlanId = model.CurrentSpecPlanId;

            if (currentSpecPlanId != 0)
            {
                if (model.CurrentSpecId != 0) // такая запись в TrainingSpec есть - модифицируем
                {
                    var currentSpecLine = appContext.TrainingSpecs.Find(model.CurrentSpecId);
                    if (currentSpecLine == null)
                    {
                        ModelState.AddModelError("IncorrectSpecId", $"Record {nameof(model.CurrentSpecId)}  with not found in DataBase");
                        RedirectToAction("NextStep", new { currentSpecPlanId = model.CurrentSpecPlanId, trainingPlanHeadId = model.HeadTrainingPlanId, trainingHeadId = model.HeadTrainingId });
                    }
                    else
                    {
                        currentSpecLine.Weight = model.Weight;
                        currentSpecLine.Amount = model.Amount;
                        appContext.TrainingSpecs.Update(currentSpecLine);
                        await appContext.SaveChangesAsync();
                    }
                }
                else // новая запись в TrainingSpec - добавляем
                {
                    var currentSpecLine = new TrainingSpec
                    {
                        HeadId = model.HeadTrainingId,
                        ExId = model.ExerciseId,
                        Set = model.Set,
                        Weight = model.Weight,
                        Amount = model.Amount,
                        Order = appContext.TrainingPlanSpecs.Find(currentSpecPlanId).Order
                    };
                    appContext.TrainingSpecs.Add(currentSpecLine);
                    await appContext.SaveChangesAsync();
                }

                //Найти следующую запись в тренировочной программе.
                var nextSpecPlanLine = GetNextSpecLine<TrainingPlanSpec>(model.CurrentSpecPlanId);
                if (nextSpecPlanLine == null)
                {
                    return RedirectToAction("Finish", new { headPlanId = model.HeadTrainingPlanId });
                }
                else
                {
                    return RedirectToAction("NextStep", new { curentSpecPlanId = nextSpecPlanLine.Id, trainingPlanHead = model.HeadTrainingPlanId, trainigHeadId = model.HeadTrainingId });
                }
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Skip(TrainingStepViewModel model)
        {
            var currentSpecPlanLine = appContext.TrainingPlanSpecs.Find(model.CurrentSpecPlanId);
            var currentSpecLine = GetCurrentSpecLine(currentSpecPlanLine, model.HeadTrainingId);
            var nextSpecPlanLine = GetNextSpecLine<TrainingPlanSpec>(currentSpecPlanLine.Id);

            if (nextSpecPlanLine == null)
            {
                return RedirectToAction("Finish", new { model.HeadTrainingPlanId });
            }
            else if (currentSpecLine == null)
            {
                currentSpecLine = new TrainingSpec
                {
                    HeadId = model.HeadTrainingId,
                    ExId = model.ExerciseId,
                    Set = model.Set,
                    Weight = model.Weight,
                    Amount = 0,
                    Order = model.Order
                };
                appContext.TrainingSpecs.Add(currentSpecLine);
                await appContext.SaveChangesAsync();
            }
            return RedirectToAction("NextStep", new { curentSpecPlanId = nextSpecPlanLine.Id, trainingPlanHead = model.HeadTrainingPlanId, trainigHeadId = model.HeadTrainingId });
        }

        [HttpGet]
        public IActionResult PreviousStep(TrainingStepViewModel model)
        {
            var previousSpecPlanLine = GetPreviousSpecLine<TrainingSpec>(model.CurrentSpecPlanId);
            if (previousSpecPlanLine != null)
            {
                return RedirectToAction("NextStep", new { curentSpecPlanId = previousSpecPlanLine.Id, trainingPlanHead = model.HeadTrainingPlanId, trainigHeadId = model.HeadTrainingId });
            }
            else
            {
                return RedirectToAction("NextStep", new { curentSpecPlanId = model.CurrentSpecPlanId, trainingPlanHead = model.HeadTrainingPlanId, trainigHeadId = model.HeadTrainingId });
            }
        }

        [HttpGet]
        public IActionResult Finish(int headPlanId)
        {
            return View();
        }

        #region private methods
        private T GetNextSpecLine<T>(int currentSpecId) where T : class, ISpec, IOrdered
        {
            var specs = appContext.Set<T>();
            int headId = specs.Select(x => x.HeadId).FirstOrDefault();
            var lines = specs.Where(x => x.HeadId.Equals(headId)).ToList();
            var line = lines.Where(x => x.Id.Equals(currentSpecId)).ToList().FirstOrDefault();
            var documentHandler = HttpContext.RequestServices.GetService<DocumentSpecHandler<T>>();
            documentHandler.FillDocument(lines);
            return documentHandler.GetNextLine(line);
        }

        private T GetPreviousSpecLine<T>(int currentSpecId) where T : class, ISpec, IOrdered
        {
            var specs = appContext.Set<T>();
            int headId = specs.Select(x => x.HeadId).FirstOrDefault();
            var lines = specs.Where(x => x.HeadId.Equals(headId));
            var line = lines.Where(x => x.Id.Equals(currentSpecId)).ToList().FirstOrDefault();
            var documentHandler = HttpContext.RequestServices.GetService<DocumentSpecHandler<T>>();
            documentHandler.FillDocument(lines);
            return documentHandler.GetPreviousLine(line);
        }

        private TrainingSpec GetCurrentSpecLine(TrainingPlanSpec currentSpecPlanLine, int trainingHeadId)
        {
            return appContext.TrainingSpecs.Where(x => x.HeadId.Equals(trainingHeadId))
                .Where(x => x.Set.Equals(currentSpecPlanLine.Set) & x.ExId.Equals(currentSpecPlanLine.ExId) & x.Order.Equals(currentSpecPlanLine.Order))
                .Include(x => x.Exercise).FirstOrDefault();
        }

        private int GetNumberDocument<T>(int stepTrainingHeadDocument, string userId) where T : class, IHead
        {

            var heads = appContext.Set<T>()
                .Where(x => x.UserId.Equals(userId));
            if (heads.Any())
            {
                return heads.Max(x => x.Number) + stepTrainingHeadDocument;
            }
            else 
            {
                return stepTrainingHeadDocument;
            }
        }
        #endregion
    }
}
