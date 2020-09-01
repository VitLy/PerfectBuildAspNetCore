using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectBuild.Models.Report.ExerciseStatistics
{
    public class StatisticsModel
    {
        public Dictionary<string, List<Point<DateTime, float>>> GetExerciseData(UserGeneralData userData)
        {
            var workOutByDay = userData.userSpecs.Where(x => x.ExerciseId.Equals(userData.ExerciseId))
                .GroupBy(x => x.Date, p => new { p.Amount, p.Weight })
                .Select(x => new Point<DateTime,float> {X= x.Key, Y= x.Sum(p => p.Weight * p.Amount) }).ToList();

            var result = new Dictionary<string, List<Point<DateTime, float>>>();
            result.Add("Average Weight", workOutByDay);

            return result;
        }
    }
}