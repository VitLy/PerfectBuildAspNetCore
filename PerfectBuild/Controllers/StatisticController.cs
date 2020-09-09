﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using PerfectBuild.Data;
using PerfectBuild.Infrastructure;
using PerfectBuild.Models;
using PerfectBuild.Models.Report;
using PerfectBuild.Models.Report.ExerciseStatistics;
using PerfectBuild.Models.ViewModels;
using PerfectBuild.Services.Report;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectBuild.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class StatisticController : Controller
    {
        private readonly ApplicationContext appContext;
        private readonly ChartProvider chartProvider;
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<StatisticController> localizer;

        public StatisticController(ApplicationContext appContext, ChartProvider chartProvider, UserManager<User> userManager, IStringLocalizer<StatisticController> localizer)
        {
            this.appContext = appContext;
            this.chartProvider = chartProvider;
            this.userManager = userManager;
            this.localizer = localizer;
        }
        [HttpGet]
        public IActionResult BodyStat()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var dayFrom = DateTime.Now.ToLocalTime();
            var dayTo = DateTime.Now.ToLocalTime();
            return View(new BodyStatisticsChartViewModel
            {
                UserId = userId,
                DayFrom = dayFrom,
                DayTo = dayTo,
                UserBodyParam = new List<SelectedBodyParam>
                {
                    new SelectedBodyParam {BodyParameter=BodyParameter.Breast},
                    new SelectedBodyParam {BodyParameter=BodyParameter.Pelvis},
                    new SelectedBodyParam {BodyParameter=BodyParameter.Thigh},
                    new SelectedBodyParam {BodyParameter=BodyParameter.Waist},
                    new SelectedBodyParam {BodyParameter=BodyParameter.Weight}
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BodyStat(string userId, DateTime dayFrom, DateTime dayTo, IList<SelectedBodyParam> userBodyParam)
        {
            if (ModelState.IsValid)
            {
                var userParamPoint = new Dictionary<BodyParameter, Point<long, float>[]>();
                Point<long, float>[] points = new Point<long, float>[1];
                foreach (var param in userBodyParam)
                {
                    if (param.Select)
                    {
                        switch (param.BodyParameter)
                        {
                            case BodyParameter.Breast:
                                points = appContext.Params
                              .Where(prm => prm.UserId.Equals(userId, StringComparison.InvariantCulture) & prm.Breast > 0)
                              .Where(prm => prm.Date.ToLocalTime() >= dayFrom & prm.Date.ToLocalTime() <= dayTo)
                              .Select(pts => new Point<long, float>
                              {
                                  X = pts.Date.ToLocalTime().MillisecondsTimestamp(),
                                  Y = pts.Breast
                              }).OrderBy(pts => pts.X).ToArray();
                                break;
                            case BodyParameter.Pelvis:
                                points = appContext.Params
                            .Where(prm => prm.UserId.Equals(userId, StringComparison.InvariantCulture) & prm.Buttock > 0)
                            .Where(prm => prm.Date.ToLocalTime() >= dayFrom & prm.Date.ToLocalTime() <= dayTo)
                            .Select(pts => new Point<long, float>
                            {
                                X = pts.Date.ToLocalTime().MillisecondsTimestamp(),
                                Y = pts.Buttock
                            }).OrderBy(pts => pts.X).ToArray();
                                break;
                            case BodyParameter.Thigh:
                                points = appContext.Params
                              .Where(prm => prm.UserId.Equals(userId, StringComparison.InvariantCulture) & prm.Thigh > 0)
                              .Where(prm => prm.Date.ToLocalTime() >= dayFrom & prm.Date.ToLocalTime() <= dayTo)
                              .Select(pts => new Point<long, float>
                              {
                                  X = pts.Date.ToLocalTime().MillisecondsTimestamp(),
                                  Y = pts.Thigh
                              }).OrderBy(pts => pts.X).ToArray();
                                break;
                            case BodyParameter.Waist:
                                points = appContext.Params
                            .Where(prm => prm.UserId.Equals(userId, StringComparison.InvariantCulture) & prm.Waist > 0)
                            .Where(prm => prm.Date.ToLocalTime() >= dayFrom & prm.Date.ToLocalTime() <= dayTo)
                            .Select(pts => new Point<long, float>
                            {
                                X = pts.Date.ToLocalTime().MillisecondsTimestamp(),
                                Y = pts.Waist
                            }).OrderBy(pts => pts.X).ToArray();
                                break;
                            case BodyParameter.Weight:
                                points = appContext.Params
                              .Where(prm => prm.UserId.Equals(userId, StringComparison.InvariantCulture) & prm.Weight > 0)
                              .Where(prm => prm.Date.ToLocalTime() >= dayFrom & prm.Date.ToLocalTime() <= dayTo)
                              .Select(pts => new Point<long, float>
                              {
                                  X = pts.Date.ToLocalTime().MillisecondsTimestamp(),
                                  Y = pts.Weight
                              }).OrderBy(x => x.X).ToArray();
                                break;
                        }
                        userParamPoint.Add(param.BodyParameter, points);
                    }
                }
                LineChart<long, float> lineChart = new LineChart<long, float>(localizer["TittleLineChart"], userParamPoint);
                var jsonData = chartProvider.GetLineChart(lineChart);

                var viewModel = new BodyStatisticsChartViewModel { UserId = userId, DayFrom = dayFrom, DayTo = dayTo, UserBodyParam = userBodyParam, ChartDataJSON = jsonData };
                return View(viewModel);
            }
            return RedirectToAction("BodyStat");
        }

        [HttpGet]
        public IActionResult ExerciseStat()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var dayFrom = DateTime.Now.ToLocalTime();
            var dayTo = DateTime.Now.ToLocalTime();
            var userExercises = appContext.TrainingHeads.Where(x => x.UserId.Equals(userManager.GetUserId(HttpContext.User)))
                .Join(appContext.TrainingSpecs, x => x.Id, y => y.HeadId, (x, y) => new { y.ExId })
                .Join(appContext.Exercises, x => x.ExId, y => y.Id, (x, y) => new { ExerciseName = y.Name, ExerciseId = y.Id })
                .OrderBy(x => x.ExerciseName).Distinct().ToList();

            int exerciseName = userExercises.FirstOrDefault().ExerciseId;

            var model = new ExerciseStatisticsChartViewModel
            {
                UserId = userId,
                DayFrom = dayFrom,
                DayTo = dayTo,
                Exercise = exerciseName,
                Exercises = new List<SelectListItem>()
            };
            foreach (var item in userExercises)
            {
                model.Exercises.Add(new SelectListItem { Text = item.ExerciseName, Value = item.ExerciseId.ToString(), Selected = false });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExerciseStat(ExerciseStatisticsChartViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<TrainingHead> userHeads = GetUserHead(model);
                List<TrainingSpec> userSpec = GetUserSpec(userHeads);

                var userExercises = appContext.Exercises.OrderBy(x => x.Name);

                var statisticsModel = new StatisticsModel();
                var userRows = statisticsModel.GetExerciseData(new UserGeneralData
                { UserId = model.UserId, DateFrom = model.DayFrom, DateTo = model.DayTo, UserHead = userHeads, UserSpecs = userSpec, UserExercises = userExercises, ExerciseId = model.Exercise });

                var userRowsInGradualNumberDateLabel = RowsXAxisConverter.ToSerialNumbersWithDateLabel(userRows);

                ColumnChart<int, float> columnChart = new ColumnChart<int, float>(localizer["TittleColumnChart"], userRowsInGradualNumberDateLabel);

                var jsonData = chartProvider.GetColumnChart(columnChart);

                var exercises = userExercises.Select((x) => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

                var viewModel = new ExerciseStatisticsChartViewModel()
                {
                    UserId = model.UserId,
                    DayFrom = model.DayFrom,
                    DayTo = model.DayTo,
                    Exercise = model.Exercise,
                    Exercises = exercises,
                    ChartDataJSON = jsonData
                };
                return View(viewModel);
            }
            return RedirectToAction("ExerciseStat");
        }

        [HttpGet]
        public IActionResult ExerciseRecords()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var dayFrom = DateTime.Now.ToLocalTime();
            var dayTo = DateTime.Now.ToLocalTime();
            return View(new ExerciseRecordsChartViewModel { UserId = userId, DayFrom = dayFrom, DayTo = dayTo });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExerciseRecords(ExerciseRecordsChartViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = userManager.GetUserId(HttpContext.User);
                var dayFrom = DateTime.Now.ToLocalTime();
                var dayTo = DateTime.Now.ToLocalTime();

                var userHeads = GetUserHead(model);
                var userSpec = GetUserSpec(userHeads);
                var userExercises = appContext.Exercises.OrderBy(x => x.Name);

                StatisticsModel statisticsModel = new StatisticsModel();

                List<Point<int,float>> exerciseRecordsRow= statisticsModel.GetExerciseRecords(
                    new UserGeneralData { UserId = userId, DateFrom = dayFrom, DateTo = dayTo, UserSpecs = userSpec, UserHead = userHeads,UserExercises=userExercises });

                return View(model);
            }
            return RedirectToAction(nameof(ExerciseRecords));
        }

        [HttpGet]
        public IActionResult PlanExecution()
        {
            return View();
        }

        #region private Methods
        private List<TrainingSpec> GetUserSpec(List<TrainingHead> userHeads)
        {
            return userHeads
                                .Join(appContext.TrainingSpecs, x => x.Id, y => y.HeadId, (x, y) => new TrainingSpec
                                { HeadId = y.HeadId, Set = y.Set, ExId = y.ExId, Weight = y.Weight, Amount = y.Amount, }).ToList();
        }

        private List<TrainingHead> GetUserHead(StatisticsCommonDataChartViewModel model)
        {
            return appContext.TrainingHeads.Where(x => x.UserId.Equals(model.UserId) & x.Date >= model.DayFrom.ToUniversalTime() & x.Date <= model.DayTo.ToUniversalTime()).ToList();
        } 
        #endregion
    }
}