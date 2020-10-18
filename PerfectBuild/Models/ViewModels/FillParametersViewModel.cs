using System;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class ParametersViewModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Range(0, 600, ErrorMessage = "WeightRange")]  // TODO: разобраться с локализацией пользовательского атрибута, https://docs.microsoft.com/ru-ru/archive/blogs/mvpawardprogram/asp-net-core-mvc
        public float Weight { get; set; }

        [Range(0, 1100, ErrorMessage = "BreastRange")]
        public int Breast { get; set; }

        [Range(0, 200, ErrorMessage = "PelvisRange")]
        public int Pelvis { get; set; }

        [Range(0, 250, ErrorMessage = "WaistRange")]
        public int Waist { get; set; }

        [Range(0, 200, ErrorMessage = "ThighRange")]
        public int Thigh { get; set; }
    }
}
