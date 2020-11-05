using PerfectBuild.Infrastructure;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class UnitViewModel:PaginationModelView
    {
        public List<Unit> Items { get; set; }
        public string SortBy { get; set; } = "name";
        public FieldSort CurrentSort { get; set; }
    }
}
