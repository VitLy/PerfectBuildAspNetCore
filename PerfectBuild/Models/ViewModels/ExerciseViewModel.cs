using PerfectBuild.Infrastructure;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class ExerciseViewModel:PaginationModelView
    {
        public List<Exercise> Items { get; set; }
        public string SortBy { get; set; } = "name";
        public FieldSort CurrentSort { get; set; } = FieldSort.nameAscend;
    }
}
