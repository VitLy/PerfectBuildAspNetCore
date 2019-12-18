using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class TrainigProgramHeadViewModel
    {
        public List<Exercise> Exercises { get; set; }
        [Required]
        public List<Category> Categories { get; set; }

        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
