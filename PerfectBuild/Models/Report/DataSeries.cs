using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;

namespace PerfectBuild.Models.Report
{
    public enum ChartType
    {
        line, bar, radar, pie, doughnat, polarArea, bubble, scatter, area, mixed, column
    }

    public enum XValueType
    {
        empty,dateTime
    }

    [JsonObject(MemberSerialization.OptIn, Description = "data")]
    [JsonArrayAttribute]
    public class DataSeries<Tx,Ty>
    {
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        private readonly ChartType type;

        [JsonProperty(PropertyName = "name")]
        private readonly string name;

        [JsonProperty(PropertyName = "showInLegend")]
        private readonly bool showInLegend;

        [JsonProperty(PropertyName = "legendText")]
        private readonly string legendText;

        [JsonProperty(PropertyName = "xValueType")]
        [JsonConverter(typeof(StringEnumConverter))]
        private readonly XValueType xValueType;

        [JsonProperty(PropertyName = "toolTipContent")]
        private readonly string toolTipContent;

        [JsonProperty(PropertyName = "dataPoints")]
        private readonly Point<Tx,Ty>[] points;

        public DataSeries(DataSeriesParameters<Tx,Ty> seriesParameters )
        {
            if (seriesParameters != null) 
            {
            this.type = seriesParameters.ChartType;
            this.legendText = seriesParameters.LegendText;
            this.showInLegend = seriesParameters.ShowInLegend;
            this.points = seriesParameters.Points.ToArray();
            this.xValueType = seriesParameters.XValueType;
            this.toolTipContent = seriesParameters.ToolTipContent;
            }
        }
    }
}
