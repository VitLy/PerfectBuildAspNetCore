using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class TrainigSpecLineChangeViewModel
    {
        public List<Exercise> Exercises { get; set; }
        public DayOfWeek DayTraining { get; set; }

        public int HeadId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ExerciseId { get; set; }
        [Range(1, 255, ErrorMessage = "SetRange")]
        [Required(ErrorMessage = "SetRequired")]
        public byte Set { get; set; }
        [Range(0.1f, 300f, ErrorMessage = "WeightRange")]
        [Required(ErrorMessage = "WeightRequired")]
        public float Weight { get; set; }
        [Range(1, 255, ErrorMessage = "AmountRange")]
        [Required(ErrorMessage = "AmountRequired")]
        public byte Amount { get; set; }
        public int Order { get; set; }
    }
}
