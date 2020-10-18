using PerfectBuild.Models.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class AddTrainingManuallyViewModel
    {
        public IEnumerable<Exercise> Exercises { get; set; }
        [Required(ErrorMessage ="TrainingNameRequired")]
        public string TrainingName { get; set; }
        [Required(ErrorMessage = "NumDocumentRequired")]
        public int NumDocument { get; set; }
        [Required(ErrorMessage = "DateRequired")]
        public DateTime Date { get; set; }

        public double Duration { get; set; }  //minutes
        public int Calories { get; set; }
        public int HeadId { get; set; }

        public IEnumerable<TrainingSpec> Spec { get; set; }
    }
}
