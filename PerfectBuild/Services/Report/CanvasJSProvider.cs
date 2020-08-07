using Newtonsoft.Json;
using PerfectBuild.Models.Report;
using System;
using System.Collections.Generic;

namespace PerfectBuild.Services.Report
{
    public class CanvasJSProvider : ChartProvider
    {
        public override string GetBarChart(Diagram<string, int> diagram)
        {
            throw new NotImplementedException();
        }

        public override string GetColumnChart(Diagram<string, int> diagram)
        {
            throw new NotImplementedException();
        }

        public override string GetLineChart(Diagram<long, float> diagram)
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(diagram, jsonSerializerSettings);
        }

        public override string GetPieChart(Diagram<float, string> diagram)
        {
            throw new NotImplementedException();
        }
    }
}
