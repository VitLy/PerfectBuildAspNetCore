using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class BodyStatisticsChartViewModel : StatisticsCommonDataChartViewModel
    {
        public IList<SelectedBodyParam> UserBodyParam { get; set; }
    }
}
