using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public float Weight { get; set; }
        [Required]
        public byte Amount { get; set; }
    }
}
