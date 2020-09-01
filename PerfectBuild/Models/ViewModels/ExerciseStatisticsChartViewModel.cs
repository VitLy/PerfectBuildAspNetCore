using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class ExerciseStatisticsChartViewModel:StatisticsCommonDataChartViewModel
    {
        public int Exercise { get; set; }

        public IList<SelectListItem> Exercises { get; set; }

    }
}
