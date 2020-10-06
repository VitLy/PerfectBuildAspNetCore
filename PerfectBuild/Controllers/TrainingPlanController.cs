using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PerfectBuild.Data;
using PerfectBuild.Infrastructure;
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
    public class TrainingPlanController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext appContext;
        private readonly DocumentSpecHandler<TrainingPlanSpec> documentSpecHandler;
        private readonly ITrainigDayConverter trainigDayConverter;
        private readonly SpecLineValidator specLineValidator;
        private readonly IStringLocalizer<SharedResource> sharedLocalizer;
        private readonly IStringLocalizer<TrainingPlanController> localizer;

        //TODO: При выходе(при удалении строки спецификации?) из документа - проверка на наличие спецификации, если нет - удалить заголовок

        public TrainingPlanController(UserManager<User> userManager, ApplicationContext appContext,
            DocumentSpecHandler<TrainingPlanSpec> documentSpecHandler, ITrainigDayConverter trainigDayConverter,SpecLineValidator specLineValidator,
            IStringLocalizer<SharedResource> sharedLocalizer, IStringLocalizer<TrainingPlanController> localizer)
        {
            this.appContext = appContext;
            this.userManager = userManager;
            this.documentSpecHandler = documentSpecHandler;
            this.trainigDayConverter = trainigDayConverter;
            this.specLineValidator = specLineValidator;
            this.sharedLocalizer = sharedLocalizer;
            this.localizer = localizer;
        }

        [HttpGet]
        public IActionResult Show(DayOfWeek? dayTraining = null)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            int headId = 0;

            if (TempData["dayTraining"] != null)
            {
                dayTraining = (DayOfWeek)TempData["dayTraining"];
            }
            else if (dayTraining == null)
            {
                dayTraining = FindFirstTrainingDay(userId);
            }
            headId = FindHeadIdByDay(dayTraining.Value, userId);

            var viewModel = new TrainigPlanViewModel
            {
                Id = headId,
                CurrentTrainingDay = dayTraining.Value,
                Lines = new List<TrainingPlanSpec>()
            };

            if (headId != 0)
            {
                viewModel.Lines = appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(headId)).Include(x => x.Exercise).OrderBy(x => x.Order).ToList();
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddModifyTrainingPlanLine(DayOfWeek dayTraining, int headId)
        {
            var exercises = appContext.Exercises.OrderBy(x=>x.Name).ToList();
            int specId = 0;
            var model = new TrainigSpecLineChangeViewModel
            {
                Exercises = exercises,
                HeadId = headId
            };

            if (TempData["specId"] != null && TempData["headId"] != null && TempData["dayTraining"] != null)
            {
                specId = (int)TempData["specId"];
                headId = (int)TempData["headId"];
                dayTraining = (DayOfWeek)TempData["dayTraining"];

                var line = appContext.TrainingPlanSpecs.Find(specId);
                model.Id = specId;
                model.HeadId = headId;
                model.Set = line.Set;
                model.Weight = line.Weight;
                model.Name = line.Exercise.Name;
                model.Amount = line.Amount;
            }
            ViewBag.TrainingDay = dayTraining;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyTrainingPlanLine(TrainigSpecLineChangeViewModel viewModel)
        {
            if (viewModel != null)
            {
                var exercises = appContext.Exercises.ToList();
                if (!specLineValidator.IsSpecLineHasCorrectWeight(viewModel.ExerciseId,viewModel.Weight,exercises, out string shortMessage, out string longMessage)) 
                {
                    ModelState.AddModelError(shortMessage, longMessage);
                }

                if (ModelState.IsValid)
                {
                    int headId = viewModel.HeadId;
                    if (viewModel.HeadId == 0)
                    {
                        headId = await CreateTrainigPlanHead(viewModel.DayTraining,CreateTrainingPlanName(viewModel.DayTraining));
                    }
                    documentSpecHandler.FillDocument(appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(headId)).ToList());
                    var planSpecLine = new TrainingPlanSpec
                    {
                        HeadId = headId,
                        ExId = viewModel.ExerciseId,
                        Set = viewModel.Set,
                        Weight = viewModel.Weight,
                        Amount = viewModel.Amount,
                        Order = documentSpecHandler.GetLastOrder()
                    };

                    if (viewModel.Id == 0)
                    {
                        await appContext.TrainingPlanSpecs.AddAsync(planSpecLine);
                        await appContext.SaveChangesAsync();
                    }
                    else
                    {
                        planSpecLine.Id = viewModel.Id;
                        var changingProgramPlanSpec = appContext.TrainingPlanSpecs.Find(planSpecLine.Id);
                        if (changingProgramPlanSpec != null)
                        {
                            changingProgramPlanSpec.ExId = planSpecLine.ExId;
                            changingProgramPlanSpec.Set = planSpecLine.Set;
                            changingProgramPlanSpec.Weight = planSpecLine.Weight;
                            changingProgramPlanSpec.Amount = planSpecLine.Amount;
                        }
                        appContext.TrainingPlanSpecs.Update(changingProgramPlanSpec);
                        await appContext.SaveChangesAsync();
                    }
                }
                else
                {
                    viewModel.Exercises = exercises.OrderBy(x => x.Name).ToList();
                    ViewBag.TrainingDay = viewModel.DayTraining;
                    return View(viewModel);
                }
            }
            TempData["dayTraining"] = viewModel.DayTraining;
            return RedirectToAction("Show");
        }

        [HttpGet]
        public IActionResult Modify(int id, int headId, DayOfWeek dayTraining)
        {
            if (id != 0 && headId != 0)
            {
                TempData["specId"] = id;
                TempData["headId"] = headId;
                TempData["dayTraining"] = dayTraining;
                return RedirectToAction("AddModifyTrainingPlanLine");
            }
            TempData["dayTraining"] = dayTraining;
            return RedirectToAction("Show");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int headId, DayOfWeek dayTraining)
        {
            if (id != 0 && headId != 0)
            {
                var deletingLine = appContext.TrainingPlanSpecs.Where(x => x.Id.Equals(id) & x.HeadId.Equals(headId)).FirstOrDefault();
                if (deletingLine != null)
                {
                    appContext.TrainingPlanSpecs.Remove(deletingLine);
                    await appContext.SaveChangesAsync();
                }
            }
            TempData["dayTraining"] = dayTraining;
            return RedirectToAction("Show");
        }

        [HttpGet]
        public async Task<IActionResult> Clear(int headId, DayOfWeek dayTraining)
        {
            if (headId != 0)
            {
                var deletingLines = appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(headId)).ToList();

                if (deletingLines != null)
                {
                    appContext.TrainingPlanSpecs.RemoveRange(deletingLines);
                    await appContext.SaveChangesAsync();
                }
            }
            TempData["dayTraining"] = dayTraining;
            return RedirectToAction("Show");
        }

        [HttpGet]
        public async Task<IActionResult> LineUp(DayOfWeek dayTraining, int headId, int specId)
        {
            var lines = appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(headId))?.ToList();
            var line = lines?.Where(x => x.Id.Equals(specId))?.FirstOrDefault();
            if (lines != null & line != null)
            {
                documentSpecHandler.FillDocument(lines);
                lines = (List<TrainingPlanSpec>)documentSpecHandler.MoveLineUp(line);
                if (lines != null)
                {
                    await SaveMovedLine(lines);
                };
            }
            TempData["dayTraining"] = dayTraining;
            return RedirectToAction("Show");
        }

        [HttpGet]
        public async Task<IActionResult> LineDown(DayOfWeek dayTraining, int headId, int specId)
        {
            var lines = appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(headId))?.ToList();
            var line = lines?.Where(x => x.Id.Equals(specId))?.FirstOrDefault();
            if (lines != null & line != null)
            {
                documentSpecHandler.FillDocument(lines);
                lines = (List<TrainingPlanSpec>)documentSpecHandler.MoveLineDown(line);
                if (lines != null)
                {
                    await SaveMovedLine(lines);
                };
            }
            TempData["dayTraining"] = dayTraining;
            return RedirectToAction("Show");
        }

        [HttpGet]
        public IActionResult AddExFromTrainProgram(DayOfWeek dayTraining, int headId)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var model = new AddExercisesFromTrainingProgramViewModel
            {
                DayTraining = dayTraining,
                HeadId = headId,
                TrainingPrograms = appContext.TrainingProgramHeads.Where(x => x.UserId.Equals(userId)).Include(x => x.Category).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExFromTrainProgram(AddExercisesFromTrainingProgramViewModel viewModel)
        {
            if (viewModel != null)
            {
                var trainingProgramSpecs = appContext.TrainingProgramSpecs.Where(x => x.HeadId.Equals(viewModel.ProgramHeadId)).ToList();
                if (trainingProgramSpecs.Count != 0)
                {
                    var userId = userManager.GetUserId(HttpContext.User);
                    int headId = viewModel.HeadId;
                    byte SetNum = 1;
                    List<TrainingPlanSpec> lines;

                    #region Find last order number in current training plan
                    if (headId == 0)
                    {
                        headId = await CreateTrainigPlanHead(viewModel.DayTraining, CreateTrainingPlanName(viewModel.DayTraining));
                        lines = new List<TrainingPlanSpec>();
                    }
                    else
                    {
                        lines = appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(viewModel.HeadId)).ToList();
                        if (lines.Count != 0)
                        {
                            SetNum = Convert.ToByte(lines.Max(x => x.Set) + 1);
                        }
                    }
                    documentSpecHandler.FillDocument(lines);
                    int lastOrderNum = documentSpecHandler.GetLastOrder();
                    int step = documentSpecHandler.GetOrderStep();
                    #endregion
                    lines = new List<TrainingPlanSpec>();
                    foreach (var trainingProgramLine in trainingProgramSpecs)
                    {
                        lines.Add(new TrainingPlanSpec
                        {
                            HeadId = headId,
                            Set = SetNum,
                            ExId = trainingProgramLine.ExId,
                            Weight = trainingProgramLine.Weight,
                            Amount = trainingProgramLine.Amount,
                            Order = lastOrderNum += step
                        });
                    }
                    await appContext.TrainingPlanSpecs.AddRangeAsync(lines);
                    await appContext.SaveChangesAsync();
                }
            }
            TempData["dayTraining"] = viewModel.DayTraining;
            return RedirectToAction("Show");
        }

        [HttpGet]
        public IActionResult GetSpecLines(int headId)
        {
            return RedirectToAction("GetSpecLine", "TrainingProgram", new { headId });
        }

        [HttpGet]
        public IActionResult GetProgramHeadData(int headId)
        {
            var headProgramData = appContext.TrainingProgramHeads.Where(x => x.Id == headId).Include(x => x.Category).Select(selector => new
            {
                id = selector.Id,
                category = selector.Category.Name,
                date = selector.Date.ToString("d"),
                description = selector.Description ?? String.Empty,
            }).FirstOrDefault();

            var headData = Json(headProgramData);
            return headData;
        }

        #region private methods
        private async Task SaveMovedLine(IEnumerable<TrainingPlanSpec> lines)
        {
            appContext.TrainingPlanSpecs.UpdateRange(lines);
            await appContext.SaveChangesAsync();
        }
        private DayOfWeek FindFirstTrainingDay(string userId)
        {
            var query = from spec in appContext.TrainingPlanSpecs
                        join head in appContext.TrainingPlanHeads on spec.HeadId equals head.Id
                        where head.UserId.Equals(userId)
                        orderby head.TrainingDays
                        select new { head.TrainingDays, head.UserId, spec.Id };
            var result = query.FirstOrDefault()?.TrainingDays;
            return result == null ? DayOfWeek.Monday : trainigDayConverter.ByteToDays(result.Value).FirstOrDefault();
        }

        private int FindHeadIdByDay(DayOfWeek dayTraining, string userId)
        {
            var head = appContext.TrainingPlanHeads.Where(x => x.UserId.Equals(userId) & x.TrainingDays.Equals(trainigDayConverter.DaysToByte(dayTraining))).FirstOrDefault();
            int headId = head != null ? head.Id : 0;
            return headId;
        }

        private async Task<int> CreateTrainigPlanHead(DayOfWeek dayTraining,string trainingName)
        {
            string userId = userManager.GetUserId(HttpContext.User);
            byte trainingDay = trainigDayConverter.DaysToByte(dayTraining);
            TrainingPlanHead planHead = new TrainingPlanHead
            {
                Date = DateTime.UtcNow,
                Name = trainingName,
                TrainingDays = trainingDay,
                UserId = userId
            };
            await appContext.TrainingPlanHeads.AddAsync(planHead);
            await appContext.SaveChangesAsync();
            return appContext.TrainingPlanHeads.Where(x => x.UserId.Equals(userId) && x.TrainingDays.Equals(trainingDay)).FirstOrDefault().Id;
        }

        private string CreateTrainingPlanName(DayOfWeek dayTraining)
        {
            return $"{localizer["Tittle"]}-{sharedLocalizer[dayTraining.ToString()]}"; 
        }

        #endregion
    }
}
