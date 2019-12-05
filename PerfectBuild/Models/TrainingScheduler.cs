using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Models
{
    public partial class TrainingScheduler
    {
        [Key]
        public int Id { get; set; }

        public int ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }

        public int TrProgramId { get; set; }

        [ForeignKey("TrProgramId")]
        public Exercise Exercise { get; set; }

        [Required]
        public DateTime Day { get; set; }
    }
}
