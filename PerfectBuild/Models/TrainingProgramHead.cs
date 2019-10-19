using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Model
{
    public class TrainingProgramHead
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        Category Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(40), Required]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public virtual ICollection<TrainingProgramSpec> TrainingProgramSpec { get; set; }
    }
}
