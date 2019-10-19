using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Model
{
    public partial class Set
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TrPrSpecId { get; set; }

        [ForeignKey("TrPrSpecId")]
        public virtual TrainingProgramSpec TrainingProgramSpec { get; set; }

        [Required]
        public byte SetNum { get; set; }

        public float Weight { get; set; }

        [Required]
        public byte SetAmount { get; set; }
    }
}




