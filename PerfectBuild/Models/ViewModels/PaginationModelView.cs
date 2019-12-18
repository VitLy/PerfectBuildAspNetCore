using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Models.ViewModels
{
    public abstract class PaginationModelView
    {
        public int CurrentPage { get; set; } = 1;
        public int ItemOnPages { get; set; } = 1;
        public int TotalItems { get; set; } = 1;

    }
}
