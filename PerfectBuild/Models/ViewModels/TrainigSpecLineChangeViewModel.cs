using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class TrainigSpecLineChangeViewModel
    {
        
        public List<Exercise> Exercises { get; set; }

        public int HeadId { get; set; }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ExerciseId { get; set; }
        [Required]
        [Range(1,255)]
        public byte Set { get; set; }
        [Required]
        [Range(0.1f,300f)]
        public float Weight { get; set; }
        [Required]
        [Range(1, 255)]
        public byte Amount { get; set; }
    }
}
