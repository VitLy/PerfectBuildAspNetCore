﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PerfectBuild.Data;
using PerfectBuild.Infrastructure;
using PerfectBuild.Models;
using PerfectBuild.Models.Report;
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
                    new SelectedBodyParam {BodyParameter=BodyParameter.Buttock},
                    new SelectedBodyParam {BodyParameter=BodyParameter.Thigh},
                    new SelectedBodyParam {BodyParameter=BodyParameter.Waist},
                    new SelectedBodyParam {BodyParameter=BodyParameter.Weight}
                }
            });
        }
        [HttpPost]
        public IActionResult BodyStat(string userId, DateTime dayFrom, DateTime dayTo, IEnumerable<SelectedBodyParam> userBodyParam)
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
                      .Where(x => x.UserId.Equals(userId, StringComparison.InvariantCulture) | x.Date <= dayFrom | x.Date >= dayTo)
                      .Select(x => new Point<long, float>
                      {
                          X = x.Date.MillisecondsTimestamp(),
                          Y = x.Breast
                      }).ToArray();
                        break;
                    case BodyParameter.Buttock:
                        points = appContext.Params
                    .Where(x => x.UserId.Equals(userId, StringComparison.InvariantCulture) | x.Date <= dayFrom | x.Date >= dayTo)
                    .Select(x => new Point<long, float>
                    {
                        X = x.Date.MillisecondsTimestamp(),
                        Y = x.Breast
                    }).ToArray();
                        break;
                    case BodyParameter.Thigh:
                        points = appContext.Params
                      .Where(x => x.UserId.Equals(userId, StringComparison.InvariantCulture) | x.Date <= dayFrom | x.Date >= dayTo)
                      .Select(x => new Point<long, float>
                      {
                          X = x.Date.MillisecondsTimestamp(),
                          Y = x.Breast
                      }).ToArray();
                        break;
                    case BodyParameter.Waist:
                        points = appContext.Params
                    .Where(x => x.UserId.Equals(userId, StringComparison.InvariantCulture) | x.Date <= dayFrom | x.Date >= dayTo)
                    .Select(x => new Point<long, float>
                    {
                        X = x.Date.MillisecondsTimestamp(),
                        Y = x.Breast
                    }).ToArray();
                        break;
                    case BodyParameter.Weight:
                        points = appContext.Params
                      .Where(x => x.UserId.Equals(userId, StringComparison.InvariantCulture) & x.Date >= dayFrom & x.Date <= dayTo & x.Weight > 0)
                      .Select(x => new Point<long, float>
                      {
                          X = x.Date.MillisecondsTimestamp(),
                          Y = x.Weight
                      }).ToArray();
                        break;
                }
                }
                
                userParamPoint.Add(param.BodyParameter, points);
            }
            LineChart<long, float> lineChart = new LineChart<long, float>(localizer["Tittle"], userParamPoint);
            var jsonData = chartProvider.GetLineChart(lineChart);
            var result = jsonData.Substring(1, jsonData.Length - 2);//обрезал лишние кавычки в начали и конце строки

            var viewModel = new BodyStatisticsChartViewModel { UserId = userId, DayFrom = dayFrom, DayTo = dayTo, UserBodyParam = userBodyParam, ChartDataJSON = result };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ExerciseStat()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ExerciseProgress()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PlanExecution()
        {
            return View();
        }

    }
}