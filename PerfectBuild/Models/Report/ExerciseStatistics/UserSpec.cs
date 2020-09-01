using System;

namespace PerfectBuild.Models.Report.ExerciseStatistics
{
    public class UserSpec
    {
        public DateTime Date { get; set; }
        public int Set { get; set; }
        public int ExerciseId { get; set; }
        public float Weight { get; set; }
        public int Amount { get; set; }
    }
}
