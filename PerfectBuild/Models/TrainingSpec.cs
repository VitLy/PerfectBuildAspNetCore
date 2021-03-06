﻿using PerfectBuild.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Models
{
    public class TrainingSpec : ISpec, IOrdered
    {
        [Key]
        public int Id { get; set; }

        public int HeadId { get; set; }

        [ForeignKey("HeadId")]
        public TrainingHead TrainingHead { get; set; }

        [Required]
        [Range(0.1f, 300f)]
        public float Weight { get; set; }

        [Required]
        [Range(1, 255)]
        public byte Set { get; set; }

        [Required]
        [Range(1, 255)]
        public byte Amount { get; set; }

        public int ExId { get; set; }

        [ForeignKey("ExId")]
        public Exercise Exercise { get; set; }

        public int Order { get; set; }
    }
}
