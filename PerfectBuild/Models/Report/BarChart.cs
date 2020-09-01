using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Models.Report
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BarChart<Tx,Ty>:Diagram<Tx,Ty>
    {
        [JsonProperty(PropertyName = "data")]
        public List<DataSeries<Tx, Ty>> Data { get; set; } = new List<DataSeries<Tx, Ty>>();

        public BarChart(string tittle, Dictionary<string, Point<Tx, Ty>[]> data) :base(tittle) 
        {
            FillData(data);
        }

        private void FillData(Dictionary<string, Point<Tx, Ty>[]> data)
        {
            if (data != null)
            {
                foreach (var item in data)
                {
                    var dataSeries = new DataSeries<Tx, Ty>(ChartType.bar, "toDo", true, item.Key, XValueType.dateTime, item.Value);
                    {
                        Data.Add(dataSeries);
                    }
                }
            }
            else throw new ArgumentNullException(nameof(data));
        }
    }
}
