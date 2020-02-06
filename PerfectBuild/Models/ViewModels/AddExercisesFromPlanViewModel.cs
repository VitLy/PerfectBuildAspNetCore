using System;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class AddExercisesFromPlanViewModel
    {
        public IEnumerable<DayOfWeek> TrainingDays { get; set; }
        public int HeadId { get; set; }
        public int PlanHeadId { get; set; }
    }
}
