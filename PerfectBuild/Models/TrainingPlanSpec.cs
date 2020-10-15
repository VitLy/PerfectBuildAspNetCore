using PerfectBuild.Models.Document;
using PerfectBuild.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Models
{
    public class TrainingPlanSpec : ISpec,IOrdered
    {
        [Key]
        public int Id { get; set; }

        public int HeadId { get; set; }
       
        public TrainingPlanHead TrainingPlanHead { get; set; }

        public int ExId { get; set; }

        [ForeignKey("ExId")]
        public Exercise Exercise { get; set; }

        [Required]
        [Range(0.1f, 300f)]
        public float Weight { get; set; }

        [Required]
        [Range(1, 255)]
        public byte Set { get; set; }

        [Required]
        [Range(1, 255)]
        public byte Amount { get; set; }

        public int Order { get; set; }
    }
}
