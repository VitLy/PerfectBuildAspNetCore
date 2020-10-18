using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class TrainingJournalController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext appContext;
        private readonly DocumentHeadHandler<TrainingHead> headHandler;
        private readonly DocumentSpecHandler<TrainingSpec> documentSpecHandler;
        private readonly ITrainigDayConverter dayConverter;
        private readonly SpecLineValidator specLineValidator;

        public TrainingJournalController(UserManager<User> userManager, ApplicationContext appContext,
            DocumentHeadHandler<TrainingHead> headHandler, DocumentSpecHandler<TrainingSpec> documentSpecHandler,
            ITrainigDayConverter dayConverter, SpecLineValidator specLineValidator)
        {
            this.userManager = userManager;
            this.appContext = appContext;
            this.headHandler = headHandler;
            this.documentSpecHandler = documentSpecHandler;
            this.dayConverter = dayConverter;
            this.specLineValidator = specLineValidator;
        }

        [HttpGet]
        public async Task<IActionResult> List(int headId = 0)
        {
            if (headId != 0)
            {
                if (!IsHeadHasSpec(headId))
                {
                    await DeleteDocument(headId);
                }
            }
            var userId = userManager.GetUserId(HttpContext.User);
            var trainingHeads = appContext.TrainingHeads.Where(x => x.UserId.Equals(userId));

            var query = from heads in trainingHeads
                        join specs in appContext.TrainingSpecs
                        on heads.Id equals specs.HeadId
                        into grouped
                        let duration = heads.DateEnd - heads.Date
                        select new JournalColumnViewModel
                        {
                            HeadId = heads.Id,
                            Number = heads.Number,
                            Date = heads.Date.ToLocalTime(),
                            DocumName = heads.Name,
                            Duration = Math.Round(duration.TotalMinutes),
                            Calories = heads.Calories,
                            ExerciseCount = grouped.Count(x => x.Id != 0),
                            SetMax = grouped.Any() ? grouped.Max(x => x.Set) : 0
                        };

            var result1 = query.ToList();
            var result2 = result1?.OrderBy(x => x.Date);
            return View(result2);
        }

        [HttpGet]
        public IActionResult Details(int headId = 0)
        {
            if (headId != 0)
            {
                return View(appContext.TrainingSpecs.Where(x => x.HeadId.Equals(headId)).OrderBy(x => x.Set).ToList());
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult AddTrainingManually()
        {
            string userId = userManager.GetUserId(HttpContext.User);

            AddTrainingManuallyViewModel model = new AddTrainingManuallyViewModel
            {
                Spec = new List<TrainingSpec>(),
                Date = DateTime.Now,
                NumDocument = headHandler.GetNumberDocument(userId),
                TrainingName = ""
            };
            return View("AddTrainingManually", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainingManually(AddTrainingManuallyViewModel model)
        {
            if (model != null)
            {
                string userId = userManager.GetUserId(HttpContext.User);
                if (headHandler.IsNumberPresent(model.NumDocument, userId))
                {
                    ModelState.AddModelError($"Document:{model.NumDocument} is present in DB",
                       $"Document:{model.NumDocument} with same number is present in DB");
                    return RedirectToAction("AddTrainingManually");
                }
                else
                {
                    var head = headHandler.GetHead(model.NumDocument, userId, model.TrainingName, model.Date.ToUniversalTime());
                    head.DateEnd = model.Date.AddMinutes(model.Duration).ToUniversalTime();
                    head.UserId = userId;
                    head.Calories = model.Calories;
                    await appContext.AddAsync(head);
                    await appContext.SaveChangesAsync();
                    int headId = headHandler.FindId(model.NumDocument, userId);
                    return RedirectToAction("ViewTrainingSpecs", new { headId });
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ViewTrainingSpecs(int headId = 0)
        {
            if (headId != 0)
            {
                var head = appContext.TrainingHeads.Where(x => x.Id == headId)?.FirstOrDefault();
                if (head != null)
                {
                    var spec = appContext.TrainingSpecs.Where(x => x.HeadId == headId).Include(x => x.Exercise).OrderBy(x => x.Order);
                    AddTrainingManuallyViewModel model = new AddTrainingManuallyViewModel
                    {
                        HeadId = head.Id,
                        NumDocument = head.Number,
                        TrainingName = head.Name,
                        Date = head.Date.ToLocalTime(),
                        Calories = head.Calories,
                        Spec = spec
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AddModifySpecLine(int headId, int id, DateTime date)
        {
            if (headId != 0)
            {
                ViewBag.TrainingDay = date;
                TrainigSpecLineChangeViewModel model = new TrainigSpecLineChangeViewModel
                {
                    Exercises = appContext.Exercises.ToList(),
                    HeadId = headId,
                };

                if (id != 0) //existed Line
                {
                    ViewBag.Tittle = "Modify";
                    var spec = appContext.TrainingSpecs.Include(x => x.Exercise).Where(x => x.Id == id).ToList().FirstOrDefault();
                    model.Id = id;
                    model.Name = spec.Exercise.Name;
                    model.Set = spec.Set;
                    model.Weight = spec.Weight;
                    model.Amount = spec.Amount;
                    return View(model);
                }
                else //new Line
                {
                    ViewBag.Tittle = "Add";
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddModifySpecLine(TrainigSpecLineChangeViewModel viewModel)
        {
            string userId = userManager.GetUserId(HttpContext.User);
            if (viewModel == null)
            {
                ModelState.AddModelError("Incorrect TrainigSpecLineChangeViewModel", "Incorrect TrainigSpecLineChangeViewModel");
                return RedirectToAction("AddTrainingManually");
            }

            var exercises = appContext.Exercises.ToList();
            if (!specLineValidator.IsSpecLineHasCorrectWeight(viewModel.ExerciseId, viewModel.Weight, exercises, out string shortMessage, out string longMessage))
            {
                ModelState.AddModelError(shortMessage, longMessage);
            }

            if (ModelState.IsValid)
            {
                var spec = new TrainingSpec
                {
                    HeadId = viewModel.HeadId,
                    Set = viewModel.Set,
                    ExId = viewModel.ExerciseId,
                    Weight = viewModel.Weight,
                    Amount = viewModel.Amount,
                    Id = viewModel.Id
                };
                if (viewModel.Id == 0)
                {
                    documentSpecHandler.FillDocument(appContext.TrainingSpecs.Where(x => x.HeadId.Equals(viewModel.HeadId)).ToList());
                    spec.Order = documentSpecHandler.GetLastOrder();
                    await appContext.TrainingSpecs.AddAsync(spec);
                }
                else
                {
                    spec = appContext.TrainingSpecs.Find(viewModel.Id);
                    spec.Set = viewModel.Set;
                    spec.ExId = viewModel.ExerciseId;
                    spec.Weight = viewModel.Weight;
                    spec.Amount = viewModel.Amount;
                    appContext.TrainingSpecs.Update(spec);
                }
                await appContext.SaveChangesAsync();
                int headId = viewModel.HeadId;
                return RedirectToAction("ViewTrainingSpecs", new { headId = headId });
            }
            else
            {
                viewModel.Exercises = exercises;
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDocument(int headId)
        {
            if (headId != 0)
            {
                using (var transaction = appContext.Database.BeginTransaction())
                {
                    try
                    {
                        var specs = appContext.TrainingSpecs.Where(x => x.HeadId.Equals(headId));
                        if (specs.Count() != 0)
                        {
                            appContext.TrainingSpecs.RemoveRange(specs);
                            await appContext.SaveChangesAsync();
                        }
                        var head = appContext.TrainingHeads.Where(x => x.Id.Equals(headId));
                        appContext.TrainingHeads.RemoveRange(head);
                        await appContext.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch
                    {
                        ModelState.AddModelError("Error transaction delete document", "Error transaction delete document");
                    }
                }
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> LineUp(int headId, int specId)
        {
            var lines = appContext.TrainingSpecs.Where(x => x.HeadId.Equals(headId))?.ToList();
            var line = lines?.Where(x => x.Id.Equals(specId))?.FirstOrDefault();
            if (lines != null & line != null)
            {
                documentSpecHandler.FillDocument(lines);
                lines = (List<TrainingSpec>)documentSpecHandler.MoveLineUp(line);
                if (lines != null)
                {
                    await SaveMovedLine(lines);
                };
            }
            return RedirectToAction("ViewTrainingSpecs", new { headId = headId });
        }

        [HttpGet]
        public async Task<IActionResult> LineDown(int headId, int specId)
        {
            var lines = appContext.TrainingSpecs.Where(x => x.HeadId.Equals(headId))?.ToList();
            var line = lines?.Where(x => x.Id.Equals(specId))?.FirstOrDefault();
            if (lines != null & line != null)
            {
                documentSpecHandler.FillDocument(lines);
                lines = (List<TrainingSpec>)documentSpecHandler.MoveLineDown(line);
                if (lines != null)
                {
                    await SaveMovedLine(lines);
                };
            }
            return RedirectToAction("ViewTrainingSpecs", new { headId = headId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLine(int id, int headId)
        {
            if (headId != 0 && id != 0)
            {
                var spec = appContext.TrainingSpecs.Find(id);
                if (spec.HeadId.Equals(headId))
                {
                    appContext.TrainingSpecs.Remove(spec);
                    await appContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("ViewTrainingSpecs", new { headId });
        }

        [HttpGet]
        public async Task<IActionResult> Clear(int headId)
        {
            if (headId != 0)
            {
                var specs = appContext.TrainingSpecs.Where(x => x.HeadId.Equals(headId));
                if (specs.Count() != 0)
                {
                    appContext.TrainingSpecs.RemoveRange(specs);
                    await appContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("ViewTrainingSpecs", new { headId });
        }

        [HttpGet]
        public IActionResult AddExFromTrainPlan(int headId)
        {
            if (headId != 0)
            {
                var userId = userManager.GetUserId(HttpContext.User);
                var trainingDays = dayConverter.ByteToDays(appContext.TrainingPlanHeads.Where(x => x.UserId.Equals(userId)).Select(x => x.TrainingDays).ToList());

                var model = new AddExercisesFromPlanViewModel
                {
                    TrainingDays = trainingDays,
                    HeadId = headId,
                };
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddExFromTrainPlan(AddExercisesFromPlanViewModel model)
        {
            if (model != null & model.HeadId != 0)
            {
                var trainingPlanSpecs = appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(model.PlanHeadId)).ToList();
                var userId = userManager.GetUserId(HttpContext.User);
                int headId = model.HeadId;
                byte SetNum = 1;
                List<TrainingSpec> lines;
                if (trainingPlanSpecs.Count != 0)
                {
                    lines = appContext.TrainingSpecs.Where(x => x.HeadId.Equals(model.HeadId)).ToList();
                    if (lines.Count != 0)
                    {
                        SetNum = Convert.ToByte(lines.Max(x => x.Set) + 1);
                    }

                    documentSpecHandler.FillDocument(lines);
                    int lastOrderNum = documentSpecHandler.GetLastOrder();
                    int step = documentSpecHandler.GetOrderStep();

                    lines = new List<TrainingSpec>();
                    foreach (var trainingPlanLine in trainingPlanSpecs)
                    {
                        lines.Add(new TrainingSpec
                        {
                            HeadId = headId,
                            Set = SetNum,
                            ExId = trainingPlanLine.ExId,
                            Weight = trainingPlanLine.Weight,
                            Amount = trainingPlanLine.Amount,
                            Order = lastOrderNum += step
                        });
                    }
                    await appContext.TrainingSpecs.AddRangeAsync(lines).ConfigureAwait(false);
                    await appContext.SaveChangesAsync().ConfigureAwait(false);
                    return RedirectToAction("ViewTrainingSpecs", new { headId = headId });
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetPlanSpecJson(DayOfWeek? day = null)
        {
            if (day != null)
            {
                var userId = userManager.GetUserId(HttpContext.User);
                var specPlan = appContext.TrainingPlanHeads.Where(x => x.TrainingDays.Equals(dayConverter.DaysToByte(day.Value)) & x.UserId.Equals(userId))
                    .Join(appContext.TrainingPlanSpecs.Include(x => x.Exercise),
                    head => head.Id,
                    spec => spec.HeadId,
                    (head, spec) => new { spec.Id, Exercise = spec.Exercise.Name, spec.Set, spec.Weight, spec.Amount, spec.Order });

                return Json(specPlan);
            }
            else throw new ArgumentNullException(nameof(day));
        }

        [HttpGet]
        public IActionResult GetPlanHeadJson(DayOfWeek? day = null)
        {
            if (day != null)
            {
                var userId = userManager.GetUserId(HttpContext.User);
                var headPlan = appContext.TrainingPlanHeads.Where(x => x.TrainingDays.Equals(dayConverter.DaysToByte(day.Value)) & x.UserId.Equals(userId)).Select(selector => new
                {
                    id = selector.Id,
                }).FirstOrDefault();


                return Json(headPlan);
            }
            else throw new ArgumentException(nameof(day));
        }

        #region private methods
        private async Task SaveMovedLine(IEnumerable<TrainingSpec> lines)
        {
            appContext.TrainingSpecs.UpdateRange(lines);
            await appContext.SaveChangesAsync();
        }

        private bool IsHeadHasSpec(int headId)
        {
            return appContext.TrainingSpecs.Where(x => x.HeadId.Equals(headId)).Any();
        }
        #endregion
    }
}
