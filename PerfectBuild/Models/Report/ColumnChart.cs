using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PerfectBuild.Models.Report
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ColumnChart<Tx,Ty>:Diagram<Tx,Ty>
    {
        [JsonProperty(PropertyName = "data")]
        public List<DataSeries<Tx, Ty>> Data { get; set; } = new List<DataSeries<Tx, Ty>>();

        public ColumnChart(string tittle,Dictionary<string, List<Point<Tx,Ty>>> data, IStringLocalizer localizer) :base(tittle,localizer) 
        {
            FillData(data);
        }

        private void FillData(Dictionary<string, List<Point<Tx,Ty>>> data)
        {
            if (data != null)
            {
                foreach (var item in data)
                {
                    var dataSeriesParameters = new DataSeriesParameters<Tx, Ty>(ChartType.column, true, item.Key, XValueType.empty, item.Value, "");
                    var dataSeries = new DataSeries<Tx,Ty>(dataSeriesParameters);
                    {
                        Data.Add(dataSeries);
                    }
                }
            }
            else throw new ArgumentNullException(nameof(data));
        }
    }
}
