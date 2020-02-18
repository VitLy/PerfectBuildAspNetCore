using PerfectBuild.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Models
{
    public class TrainingHead:IHead
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public int Number { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        public int Calories { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int TrainingPlanHeadId { get; set; }
      
        public virtual ICollection<TrainingSpec> TrainingSpec { get; set; }
    }
}
