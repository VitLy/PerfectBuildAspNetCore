using System.Collections.Generic;

namespace PerfectBuild.Models.Report
{
    public class DataSeriesParameters<Tx,Ty>
    {
        public ChartType ChartType { get; }

        public bool ShowInLegend {get;}
        public string LegendText {get;}
        public XValueType XValueType {get;}
        public IList<Point<Tx, Ty>> Points { get; set; }
        public string ToolTipContent { get; set; }

        public DataSeriesParameters(ChartType chartType,bool showInLegend,string legendText,XValueType xValueType,IList<Point<Tx, Ty>> points,string toolTipContent)
        {
            this.ChartType = chartType;
            this.ShowInLegend = showInLegend;
            this.LegendText = legendText;
            this.XValueType = xValueType;
            this.Points = points;
            this.ToolTipContent = toolTipContent;
        }
    }
}
