using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class TrainingProgramHeadViewModel
    {
        public List<Exercise> Exercises { get; set; }
        [Required]
        public List<Category> Categories { get; set; }

        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "NameRequired")]
        [MaxLength(40,ErrorMessage = "NameMaxLength")]
        public string Name { get; set; }
        [MaxLength(250, ErrorMessage = "DescriptionMaxLength")]
        public string Description { get; set; }
    }
}
