using PerfectBuild.Models.Report;

namespace PerfectBuild.Services.Report
{
    public abstract class ChartProvider
    {
        public abstract string GetLineChart(Diagram<long,float> diagram);
        public abstract string GetColumnChart(Diagram<string,int> diagram);
        public abstract string GetBarChart(Diagram<string,int> diagram);
        public abstract string GetPieChart(Diagram<float,string> diagram);
    }
}
 