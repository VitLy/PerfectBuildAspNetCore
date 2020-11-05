using PerfectBuild.Infrastructure;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class CategoryViewModel:PaginationModelView
    {
        public List<Category> Items { get; set; }
        public string SortBy { get; set; } = "name";
        public FieldSort CurrentSort { get; set; }
    }
}
