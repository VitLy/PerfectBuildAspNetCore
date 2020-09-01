using System;

namespace PerfectBuild.Models.ViewModels
{
    public abstract class StatisticsCommonDataChartViewModel
    {
        public string UserId { get; set; }
        public DateTime DayFrom { get; set; }
        public DateTime DayTo { get; set; }
        public string ChartDataJSON { get; set; }
    }
}