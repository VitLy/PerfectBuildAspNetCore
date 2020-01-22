using PerfectBuild.Models.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Models.ViewModels
{
    public class ListItemViewModel<T> : PaginationModelView
    {
        public List<T> Items { get; set; }

        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemOnPages);
            }
        }
    }
}
