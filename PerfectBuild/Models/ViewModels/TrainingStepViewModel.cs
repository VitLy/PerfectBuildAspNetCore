using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class TrainingStepViewModel
    {
        public int CurrenSpecPlanId { get; set; }
        public int CurrentSpecId { get; set; }

        public int TotalExercises { get; set; }
        public int TotalSets { get; set; }

        public string Exercise { get; set; }
        public int ExerciseId { get; set; }
        public string ExerciseDescription { get; set; }
        [Required]
        public byte Set { get; set; }  
        [Required]
        public float Weight { get; set; }
        [Required]
        public byte Amount { get; set; } 

    }
}
