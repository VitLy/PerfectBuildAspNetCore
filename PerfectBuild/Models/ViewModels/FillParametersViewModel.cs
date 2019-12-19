using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Models.ViewModels
{
    public class FillParametersViewModel
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
