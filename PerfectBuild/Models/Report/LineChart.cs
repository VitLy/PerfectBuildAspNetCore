using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PerfectBuild.Models.Report
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LineChart<Tx, Ty> : Diagram<Tx, Ty>
    {
        [JsonProperty(PropertyName = "data")]
        public List<DataSeries<Tx, Ty>> Data { get; set; } = new List<DataSeries<Tx, Ty>>();
     
        public LineChart(string title, Dictionary<BodyParameter,Point<Tx, Ty>[]> data) : base(title)
        {
            FillData(data);
        }

        private void FillData(Dictionary<BodyParameter,Point<Tx, Ty>[]> data)
        {
            if (data != null)
            {
                foreach (var item in data)
                {
                    var dataSeries = new DataSeries<Tx, Ty>(ChartType.line, "toDo",true,item.Key.ToString(),XValueType.dateTime,  item.Value);
                    {
                        Data.Add(dataSeries);
                    }
                }
            }
            else throw new ArgumentNullException(nameof(data));
        }
    }

}