using Newtonsoft.Json;
using PerfectBuild.Models.Report;

namespace PerfectBuild.Services.Report
{
    public class CanvasJSProvider : ChartProvider
    {
        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();

        public override string GetBarChart<Tx, Ty>(Diagram<Tx, Ty> diagram)
        {
            return Serialize(diagram);
        }

        public override string GetColumnChart<Tx,Ty>(Diagram<Tx, Ty> diagram)
        {
            return Serialize(diagram);
        }

        public override string GetLineChart<Tx,Ty>(Diagram<Tx, Ty> diagram)
        {
            return Serialize(diagram);
        }

        private string Serialize<Tx,Ty>(Diagram<Tx, Ty> diagram)
        {
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            var diafgramData=JsonConvert.SerializeObject(diagram, jsonSerializerSettings);
            var result = diafgramData.Substring(1, diafgramData.Length - 2);//обрезал лишние кавычки в начале и конце строки
            return result;
        }
    }
}
