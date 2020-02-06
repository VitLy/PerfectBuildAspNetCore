using PerfectBuild.Models.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class TrainigPlanViewModel
    {
        public DayOfWeek CurrentTrainingDay { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public List<TrainingPlanSpec> Lines { get; set; }
    }
}
