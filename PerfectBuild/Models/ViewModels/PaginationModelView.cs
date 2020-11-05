using System;

namespace PerfectBuild.Models.ViewModels
{
    public abstract class PaginationModelView
    {
        public int CurrentPage { get; set; } = 1;
        public int ItemOnPages { get; set; } = 6;
        public int TotalItems { get; set; } = 1;

        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemOnPages);
            }
        }

    }
}
