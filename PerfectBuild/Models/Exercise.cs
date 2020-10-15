using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Models
{
    public partial class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(45, ErrorMessage = "Exercise's name length should be less then 40 chars")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Exercise's description length should be less then 250 chars")]
        public string Description { get; set; }

        public int UnitId { get; set; }

        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }

        public bool OwnWeight { get; set; }

        public virtual ICollection<TrainingProgramSpec> TrainingProgramSpecs {get;set;}
        public virtual ICollection<TrainingPlanSpec> TrainingPlanSpecs {get;set;}
        public virtual ICollection<TrainingSpec> TrainingSpecs {get;set;}

    }
}




