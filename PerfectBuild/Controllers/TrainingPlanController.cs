﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectBuild.Data;
using PerfectBuild.Models;
using PerfectBuild.Models.Document;
using PerfectBuild.Models.Interfaces;
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
        private readonly DocumentHandler<TrainingPlanSpec> documentHandler;
        private readonly ITrainigDayConverter trainigDayConverter;

        public TrainingPlanController(UserManager<User> userManager, ApplicationContext appContext,
            DocumentHandler<TrainingPlanSpec> documentHandler, ITrainigDayConverter trainigDayConverter)
        {
            this.appContext = appContext;
            this.userManager = userManager;
            this.documentHandler = documentHandler;
            this.trainigDayConverter = trainigDayConverter;
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
                Lines = new List<Line>()
            };

            if (headId != 0)
            {
                viewModel.Lines = GetLinesFromSpec(appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(headId)).Include(x => x.Exercise).OrderBy(x => x.Order).ToList());
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddModifyTrainingPlanLine(DayOfWeek dayTraining, int headId)
        {
            var exercises = appContext.Exercises.ToList();
            int specId = 0;
            var model = new TrainigSpecLineChangeViewModel
            {
                Exercises = exercises
            };

            if (TempData["specId"] != null && TempData["specId"] != null && TempData["dayTraining"] != null)
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
        public async Task<IActionResult> AddModifyTrainingPlanLine(TrainigSpecLineChangeViewModel viewModel, DayOfWeek dayTraining)
        {
            if (viewModel != null)
            {
                int headId = viewModel.HeadId;
                if (viewModel.HeadId == 0)
                {
                    headId = await CreateTrainigPlanHead(dayTraining);
                }
                documentHandler.FillDocument(appContext.TrainingPlanSpecs.Where(x => x.HeadId.Equals(headId)).ToList());
                var planSpecLine = new TrainingPlanSpec
                {
                    HeadId = headId,
                    ExId = viewModel.ExerciseId,
                    Set = viewModel.Set,
                    Weight = viewModel.Weight,
                    Amount = viewModel.Amount,
                    Order = documentHandler.GetLastOrder()
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
            TempData["dayTraining"] = dayTraining;
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
                documentHandler.FillDocument(lines);
                lines = (List<TrainingPlanSpec>)documentHandler.MoveLineUp(line);
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
                documentHandler.FillDocument(lines);
                lines = (List<TrainingPlanSpec>)documentHandler.MoveLineDown(line);
                if (lines != null)
                {
                    await SaveMovedLine(lines);
                };
            }
            TempData["dayTraining"] = dayTraining;
            return RedirectToAction("Show");
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

        private async Task<int> CreateTrainigPlanHead(DayOfWeek dayTraining)
        {
            string userId = userManager.GetUserId(HttpContext.User);
            byte trainingDay = trainigDayConverter.DaysToByte(dayTraining);
            TrainingPlanHead planHead = new TrainingPlanHead
            {
                Date = DateTime.UtcNow,
                Name = "Created by hands",
                TrainingDays = trainingDay,
                UserId = userId
            };
            await appContext.TrainingPlanHeads.AddAsync(planHead);
            await appContext.SaveChangesAsync();
            return appContext.TrainingPlanHeads.Where(x => x.UserId.Equals(userId) && x.TrainingDays.Equals(trainingDay)).FirstOrDefault().Id;
        }

        private List<Line> GetLinesFromSpec(List<TrainingPlanSpec> trPlanSpeclist)
        {
            List<Line> lines = new List<Line>();
            foreach (var item in trPlanSpeclist)
            {
                lines.Add(new Line { Id = item.Id, ExerciseId = item.ExId, Exercise = item.Exercise.Name, Set = item.Set, Weight = item.Weight, Amount = item.Amount, Order = item.Order });
            }
            return lines;
        }


        #endregion
    }
}