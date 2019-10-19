using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Model
{
    public class TrainingHead
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int UserProfileId { get; set; }

        [ForeignKey("UserProfileId")]
        Profile Profile { get; set; }

        public virtual ICollection<TrainingSpec> TrainingSpec { get; set; }
    }
}
