using PerfectBuild.Models.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class AddTrainingManuallyViewModel
    {
        public IEnumerable<Exercise> Exercises { get; set; }
        [Required]
        public string TrainingName { get; set; }
        [Required]
        public int NumDocument { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Duration { get; set; }  //minutes
        public int Calories { get; set; }
        public int HeadId { get; set; }

        public IEnumerable<TrainingSpec> Spec { get; set; }
    }
}
