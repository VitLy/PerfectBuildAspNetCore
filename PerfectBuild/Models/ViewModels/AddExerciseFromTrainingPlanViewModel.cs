using System;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class AddExerciseFromTrainingPlanViewModel
    {
        public IEnumerable<TrainingProgramHead> TrainingPrograms { get; set; }
        public DayOfWeek DayTraining { get; set; }
        public int HeadId { get; set; }
        public int ProgramHeadId { get; set; }
    }
}
