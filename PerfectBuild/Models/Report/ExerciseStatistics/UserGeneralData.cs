using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Models.Report.ExerciseStatistics
{
    public class UserGeneralData
    {
        public string UserId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int ExerciseId { get; set; }

        public IEnumerable<Exercise> UserExercises { get; set; }
        public IList<TrainingHead> UserHead { get; set; }
        public IList<TrainingSpec> UserSpecs { get; set; }
    }
}
