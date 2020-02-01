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
    public class TrainingJournalController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext appContext;
        private readonly DocumentHeadHandler<TrainingHead> headHandler;
        private readonly DocumentSpecHandler<TrainingSpec> documentSpecHandler;

        public TrainingJournalController(UserManager<User> userManager, ApplicationContext appContext,
            DocumentHeadHandler<TrainingHead> headHandler, DocumentSpecHandler<TrainingSpec> documentSpecHandler)
        {
            this.userManager = userManager;
            this.appContext = appContext;
            this.headHandler = headHandler;
            this.documentSpecHandler = documentSpecHandler;
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
            var userHead = appContext.TrainingHeads.Where(x => x.UserId.Equals(userId));

            var query = from spec in appContext.TrainingSpecs
                        group spec by spec.HeadId into groupedSpec
                        join head in userHead on groupedSpec.FirstOrDefault().HeadId equals head.Id
                        let duration = head.DateEnd - head.Date
                        select new JournalColumnViewModel
                        {
                            HeadId = head.Id,
                            Number = head.Number,
                            Date = head.Date,
                            DocumName = head.Name,
                            Duration = duration.TotalMinutes,
                            Calories = head.Calories,
                            ExerciseCount = groupedSpec.Count(x => x.Id != 0),
                            SetMax = groupedSpec.Max(x => x.Set)
                        };

            return View(query.ToList());
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
                Date = DateTime.UtcNow,
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
                       $"Document:{model.NumDocument} with same nuber is present in DB");
                    return RedirectToAction("AddTrainingManually");
                }
                else
                {
                    var head = headHandler.GetHead(model.NumDocument, userId, model.TrainingName, model.Date);
                    head.DateEnd = model.Date.AddMinutes(model.Duration);
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
                        Date = head.Date,
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
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddModifySpecLine(TrainigSpecLineChangeViewModel model)
        {
            string userId = userManager.GetUserId(HttpContext.User);
            if (model != null)
            {
                var spec = new TrainingSpec
                {
                    HeadId = model.HeadId,
                    Set = model.Set,
                    ExId = model.ExerciseId,
                    Weight = model.Weight,
                    Amount = model.Amount,
                    Id = model.Id
                };
                if (model.Id == 0)
                {
                    documentSpecHandler.FillDocument(appContext.TrainingSpecs.Where(x => x.HeadId.Equals(model.HeadId)).ToList());
                    spec.Order = documentSpecHandler.GetLastOrder();
                    await appContext.TrainingSpecs.AddAsync(spec);
                }
                else
                {
                    spec = appContext.TrainingSpecs.Find(model.Id);
                    spec.Set = model.Set;
                    spec.ExId = model.ExerciseId;
                    spec.Weight = model.Weight;
                    spec.Amount = model.Amount;
                    appContext.TrainingSpecs.Update(spec);
                }
                await appContext.SaveChangesAsync();
                int headId = model.HeadId;
                return RedirectToAction("ViewTrainingSpecs", new { headId = headId });
            }
            return RedirectToAction("Index", "Home");
        }

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
