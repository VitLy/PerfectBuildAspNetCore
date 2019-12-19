using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Models
{
    [Table("UserParam")]
    public partial class Param
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public DateTime Date { get; set; }   // пока не знаю как организовать одновременное ограничение Unique=Date+Profile

        [Column(TypeName = "float")]
        public float Weight { get; set; }

        public int Breast { get; set; }

        public int Buttock { get; set; }

        public int Waist { get; set; }

        public int Thigh { get; set; }
    }
}




