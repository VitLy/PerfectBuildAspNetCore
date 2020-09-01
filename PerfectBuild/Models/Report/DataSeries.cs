using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

        [JsonProperty(PropertyName = "dataPoints")]
        private readonly Point<Ty>[] point;

        public DataSeries(ChartType chartType, string name, bool showInLegend,string legendText, XValueType xValueType,Point<Tx, Ty>[] point)
        {
            this.type = chartType;
            this.name = name;
            this.legendText = legendText;
            this.showInLegend = showInLegend;
            this.point = point;
            this.xValueType = xValueType;
        }

        public DataSeries(ChartType chartType, string name, bool showInLegend, string legendText, XValueType xValueType, Point<Ty>[] point)
        {
            this.type = chartType;
            this.name = name;
            this.legendText = legendText;
            this.showInLegend = showInLegend;
            this.point = point;
            this.xValueType = xValueType;
        }
    }
}
