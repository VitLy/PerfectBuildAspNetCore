﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Models
{
    public class TrainingProgramSpec
    {
        [Key]
        public int Id { get; set; }

        public int ProgramHeadId { get; set; }

        [ForeignKey("ProgramHeadId")]
        public TrainingProgramHead TrainingProgramHead { get; set; }

        public int ExId { get; set; }

        [ForeignKey("ExId")]
        public Exercise Exercise { get; set; }

        [Required]
        public float Weight { get; set; }

        [Required]
        public byte Set { get; set; }

        [Required]
        public byte Amount { get; set; }
    }
}
