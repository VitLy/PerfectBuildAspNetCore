using PerfectBuild.Models.Document;
using System;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class AddTrainingManuallyViewModel
    {
        public IEnumerable<Exercise> Exercises { get; set; }
        public string TrainingName { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeStart{ get; set; }
        public DateTime TimeEnd { get; set; }
        public int Calories { get; set; }
        public int HeadId { get; set; }

        public IEnumerable<TrainingSpec> Spec { get; set; }
    }
}
