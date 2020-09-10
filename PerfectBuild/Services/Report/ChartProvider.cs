using PerfectBuild.Models.Report;

namespace PerfectBuild.Services.Report
{
    public abstract class ChartProvider
    {
        public abstract string GetLineChart<Tx, Ty>(Diagram<Tx, Ty> diagram);
        public abstract string GetColumnChart<Tx, Ty>(Diagram<Tx, Ty> diagram);
        public abstract string GetBarChart<Tx, Ty>(Diagram<Tx, Ty> diagram);
    }
}
