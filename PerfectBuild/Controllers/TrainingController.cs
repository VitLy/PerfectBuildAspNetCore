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
using Microsoft.Extensions.DependencyInjection;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class TrainingController : Controller
    {
        private readonly ApplicationContext appContext;
        private readonly UserManager<User> userManager;
        private readonly ITrainigDayConverter dayConverter;
        private readonly IStringLocalizer<TrainingController> localizer;
        private readonly DocumentSpecHandler<TrainingPlanSpec> specHandler;

        public TrainingController(ApplicationContext appContext, UserManager<User> userManager, ITrainigDayConverter dayConverter,
            IStringLocalizer<TrainingController> localizer, DocumentSpecHandler<TrainingPlanSpec> specHandler)
        {
            this.appContext = appContext;
            this.userManager = userManager;
            this.dayConverter = dayConverter;
            this.localizer = localizer;
            this.specHandler = specHandler;
        }

        [HttpGet]
        public IActionResult Start()
        {
            DayOfWeek currentDay = DateTime.UtcNow.DayOfWeek;
            string userId = userManager.GetUserId(HttpContext.User);
            var trainingPlan = appContext.TrainingPlanHeads.Where(x => x.TrainingDays.Equals(dayConverter.DaysToByte(currentDay)))?.ToList().FirstOrDefault();
            TrainigPlanViewModel model = new TrainigPlanViewModel
            {
                CurrentTrainingDay = currentDay
            };
            if (trainingPlan == null)
            {
                model.Name = localizer["ProgramPlanNotFoundName"];
                model.Description = localizer["ProgramPlanNotFoundDescription"];
            }
            else
            {
                List<TrainingPlanSpec> planSpec = appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(trainingPlan.Id)).OrderBy(x => x.Order).ToList();
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

        [HttpGet]
        public IActionResult StepTraining(int currentSpecPlanId, int currentSpecId = 0) //TODO: добавить в модель результат упражнения из прошлой тренировки, лучший результат по упражнению
        {
            string userId;
            if (currentSpecPlanId != 0)
            {
                userId = userManager.GetUserId(HttpContext.User);
                var trainingPlanSpec = appContext.TrainingPlanSpecs.Where(x => x.Id.Equals(currentSpecPlanId)).ToList();
                bool isTrueUser = trainingPlanSpec.Join(appContext.TrainingPlanHeads, x => x.HeadId, y => y.Id, (x, y) => y.UserId).Equals(userId);
                if (isTrueUser)
                {
                    int headPlan = appContext.TrainingPlanSpecs.Find(currentSpecPlanId).HeadId;
                    var lines = appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(headPlan)).Include(x => x.Exercise).ToList();
                    var currentLine = lines.Where(x => x.Id.Equals(currentSpecPlanId)).FirstOrDefault();
                    var totalExercise = lines.Count(x => x.Id != 0);
                    var totalSets = lines.Max(x => x.Set);
                    specHandler.FillDocument(lines);
                    var nextLine = specHandler.GetNextLine(currentLine);
                    var prevLine = specHandler.GetPreviousLine(currentLine);
                    TrainingStepViewModel model = new TrainingStepViewModel
                    {
                        CurrenSpecPlanId = currentSpecPlanId,
                        CurrentSpecId = 0,
                        TotalExercises = totalExercise,
                        TotalSets = totalSets,
                        Set = currentLine.Set,
                        Exercise = currentLine.Exercise.Name,
                        ExerciseDescription = currentLine.Exercise.Description,
                    };
                    return View("NextStep", model);
                }
            }
            ModelState.AddModelError("SpecId shouldnt equals 0", "SpecId shouldnt equals 0");
            return RedirectToAction("Start");
        }

        [HttpGet]
        public IActionResult NextStep(int currentSpecTrainId, int currentSpecPlanId)
        {
            var nextSpecPlanLine = FindNextSpecLine<TrainingPlanSpec>(currentSpecPlanId);
            if (nextSpecPlanLine != null)
            {
                var nextSpecTrainLine = FindNextSpecLine<TrainingSpec>(currentSpecTrainId);
                TrainingStepViewModel model = new TrainingStepViewModel()
                {
                    CurrenSpecPlanId = nextSpecPlanLine.Id,
                    TotalExercises = 777,   //TODO: подключить расчет
                    TotalSets = 333           //TODO: подключить расчет
                };
                if (nextSpecTrainLine != null)
                {
                    model.CurrentSpecId = nextSpecTrainLine.Id;
                    model.ExerciseId = nextSpecTrainLine.Exercise.Id;
                    model.Exercise = nextSpecTrainLine.Exercise.Name;
                    model.ExerciseDescription = nextSpecTrainLine.Exercise.Description;
                    model.Set = nextSpecTrainLine.Set;
                    model.Weight = nextSpecTrainLine.Weight;
                    model.Amount = nextSpecTrainLine.Amount;
                }
                else
                {
                    model.CurrentSpecId = 0;
                    model.ExerciseId = nextSpecPlanLine.Exercise.Id;
                    model.Exercise = nextSpecPlanLine.Exercise.Name;
                    model.ExerciseDescription = nextSpecPlanLine.Exercise.Description;
                    model.Set = nextSpecPlanLine.Set;
                    model.Weight = nextSpecPlanLine.Weight;
                    model.Amount = nextSpecPlanLine.Amount;
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Finish");
            }
        }

        [HttpPost]
        public IActionResult NextStep(TrainingStepViewModel modelFact)
        {
            return View(new TrainingStepViewModel());
        }

        [HttpGet]
        public IActionResult PreviousStep(int currentSpecId, int currentSpecPlanId)
        {
            var previousSpecTrainLine = FindPreviousSpecLine<TrainingSpec>(currentSpecId);
            var previousSpecPlanLine = FindPreviousSpecLine<TrainingPlanSpec>(currentSpecPlanId);
            if (previousSpecTrainLine != null & previousSpecPlanLine != null)
            {
                return RedirectToAction("NextStep", new { currentSpecTrainId=previousSpecTrainLine.Id, currentSpecPlanId=previousSpecPlanLine.Id });
            }
            else
            {
                return RedirectToAction("NextStep", new { currentSpecTrainId=currentSpecId, currentSpecPlanId=currentSpecPlanId });
            }
        }

        [HttpGet]
        public IActionResult Finish(int lastspecId)
        {
            return View();
        }

        #region private methods
        private T FindNextSpecLine<T>(int currentSpecId) where T : class, ISpec, IOrdered
        {
            var specs = appContext.Set<T>();
            int headId = specs.Select(x => x.HeadId).FirstOrDefault();
            var lines = specs.Where(x => x.HeadId.Equals(headId));
            var line = lines.Where(x => x.Id.Equals(currentSpecId)).ToList().FirstOrDefault();
            var documentHandler = HttpContext.RequestServices.GetService<DocumentSpecHandler<T>>();
            documentHandler.FillDocument(lines);
            return documentHandler.GetNextLine(line);
        }

        private T FindPreviousSpecLine<T>(int currentSpecId) where T : class, ISpec, IOrdered
        {
            var specs = appContext.Set<T>();
            int headId = specs.Select(x => x.HeadId).FirstOrDefault();
            var lines = specs.Where(x => x.HeadId.Equals(headId));
            var line = lines.Where(x => x.Id.Equals(currentSpecId)).ToList().FirstOrDefault();
            var documentHandler = HttpContext.RequestServices.GetService<DocumentSpecHandler<T>>();
            documentHandler.FillDocument(lines);
            return documentHandler.GetPreviousLine(line);
        }
        #endregion
    }
}
