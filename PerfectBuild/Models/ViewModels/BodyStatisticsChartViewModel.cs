using Microsoft.AspNetCore.Mvc;
using PerfectBuild.Infrastructure;
using System;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class BodyStatisticsChartViewModel
    {
        public string UserId { get; set; }
        public DateTime DayFrom { get; set; }
        public DateTime DayTo { get; set; }
        public IEnumerable<SelectedBodyParam> UserBodyParam { get; set; }

        public string ChartDataJSON { get; set; }
    }
}
