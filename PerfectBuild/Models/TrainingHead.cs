using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Models
{
    public class TrainingHead
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int TrainigPlanHeadId { get; set; }

        public TrainingPlanHead TrainingPlanHead { get; set; }
       
        public virtual ICollection<TrainingSpec> TrainingSpec { get; set; }
    }
}
