using PerfectBuild.Infrastructure;
using PerfectBuild.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Services.Report
{
    internal static class RowsXAxisConverter
    {
        internal static Dictionary<string, List<Point<long, float>>> DateToMillisecondsTimestamp(Dictionary<string, List<Point<DateTime, float>>> userRows)
        {
            var convertedRows = new Dictionary<string, List<Point<long, float>>>();
            foreach (var group in userRows)
            {
                var key = group.Key;
                var points = new List<Point<long, float>>();
                foreach (var point in group.Value)
                {
                    points.Add(new Point<long, float> { X = point.X.ToLocalTime().MillisecondsTimestamp(), Y = point.Y });
                }
                convertedRows.Add(key, points);
            }
            return convertedRows;
        }

        internal static Dictionary<string, List<Point<int, float>>> ToSerialNumbersWithDateLabel(Dictionary<string, List<Point<DateTime, float>>> userRows)
        {
            var convertedRows = new Dictionary<string, List<Point<int, float>>>();
            foreach (var group in userRows)
            {
                var key = group.Key;
                var points = new List<Point<int, float>>();
                var i = 0;
                foreach (var point in group.Value)
                {
                    points.Add(new Point<int, float> { X = ++i, Y = point.Y,Label= point.X.ToLocalTime().ToString("MMMM dd, yyyy") });
                }
                convertedRows.Add(key, points);
            }
            return convertedRows;
        }

        internal static Dictionary<string, List<Point<float>>> EraseX(Dictionary<string, List<Point<DateTime, float>>> userRows)
        {
            var convertedRows = new Dictionary<string, List<Point<float>>>();
            foreach (var group in userRows)
            {
                var key = group.Key;
                var points = new List<Point<float>>();
                var i = 0;
                foreach (var point in group.Value)
                {
                    points.Add(new Point<float> { Y = point.Y });
                }
                convertedRows.Add(key, points);
            }
            return convertedRows;
        }
    }
}
