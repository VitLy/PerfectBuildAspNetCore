using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using PerfectBuild.Controllers;
using System;
using System.Collections.Generic;

namespace PerfectBuild.Models.Report
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BarChart<Tx, Ty> : Diagram<Tx, Ty>
    {
        [JsonProperty(PropertyName = "data")]
        public List<DataSeries<Tx, Ty>> Data { get; set; } = new List<DataSeries<Tx, Ty>>();

        public BarChart(string tittle, IList<Point<Tx, Ty>> data, IStringLocalizer<StatisticController> localizer) : base(tittle,localizer)
        {
            this.localizer = localizer;
            FillData(data);
        }

        private void FillData(IList<Point<Tx, Ty>> data)
        {
            if (data != null)
            {
                var dataSeriesParameters = new DataSeriesParameters<Tx, Ty>(ChartType.bar, true, localizer["LegendExerciseRecord"], XValueType.empty, data, "");
                dataSeriesParameters.ToolTipContent = "{y}({indexLabel})";
                var dataSeries = new DataSeries<Tx, Ty>(dataSeriesParameters);
                {
                    Data.Add(dataSeries);
                }
            }
            else throw new ArgumentNullException(nameof(data));
        }


    }
}
