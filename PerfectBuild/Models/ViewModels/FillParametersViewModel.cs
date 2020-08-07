using System;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class ParametersViewModel
    {
        [Required]
        public DateTime Date { get; set; }

        public float Weight { get; set; }

        public int Breast { get; set; }

        public int Buttock { get; set; }

        public int Waist { get; set; }

        public int Thigh { get; set; }
    }
}
