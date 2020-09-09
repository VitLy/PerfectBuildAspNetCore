using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectBuild.Models.Report.ExerciseStatistics
{
    public class StatisticsModel
    {
        private static void CheckNullInputData(UserGeneralData userData)
        {
            if (userData == null)
            {
                throw new NullReferenceException(nameof(userData));
            }
        }

        public Dictionary<string, List<Point<DateTime, float>>> GetExerciseData(UserGeneralData userData)
        {
            CheckNullInputData(userData);

            var workOutByDay = userData.UserSpecs.Where(x => x.ExId.Equals(userData.ExerciseId))
                .Join(userData.UserHead,x=>x.HeadId,y=>y.Id,(x,y)=>new {y.Date,x.Amount,x.Weight })
                .GroupBy(x => x.Date, p => new { p.Amount, p.Weight })
                .Select(x => new Point<DateTime, float> { X = x.Key.Date, Y = x.Sum(p => p.Weight * p.Amount) }).ToList();

            var result = new Dictionary<string, List<Point<DateTime, float>>>();
            result.Add("Average Weight", workOutByDay);

            return result;
        }

        public List<Point<int, float>> GetExerciseRecords(UserGeneralData userData)
        {
            CheckNullInputData(userData);
            int i = 0;

            var result = userData.UserSpecs.GroupBy(x => new { x.HeadId, x.ExId }, (x, y) => new { x.HeadId, x.ExId, Total = y.Sum(p => p.Weight * p.Amount) })
                .GroupBy(x => x.ExId, (x, y) => new { Record = y.OrderByDescending(p => p.Total).FirstOrDefault() })
                .Join(userData.UserHead, x => x.Record.HeadId, y => y.Id, (x, y) => new Point<int, float> { X = ++i, Y = x.Record.Total, Label = y.Date.ToString() }).ToList();


            return result;
        }
    }
}