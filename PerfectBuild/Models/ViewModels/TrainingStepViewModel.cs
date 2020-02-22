using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class TrainingStepViewModel
    {
        public int HeadTrainingPlanId { get; set; }
        public int HeadTrainingId { get; set; }

        public int CurrentSpecPlanId { get; set; }
        public int CurrentSpecId { get; set; }

        public string Exercise { get; set; }
        public int ExerciseId { get; set; }
        public string ExerciseDescription { get; set; }
       
        [Required]
        public byte Set { get; set; }  
        [Required]
        public float Weight { get; set; }
        [Required]
        public byte Amount { get; set; } 

        public int Order { get; set; }

        public bool IsFinishedTraining { get; set; }

    }
}
