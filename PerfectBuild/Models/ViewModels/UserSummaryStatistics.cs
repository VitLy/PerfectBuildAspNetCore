using PerfectBuild.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Models.ViewModels
{
    public class UserSummaryStatistics
    {
        public string UserName { get; set; }
        public long Date { get; set; }
        public string UserId { get; set; }
        public LastTraining LastTraining { get; set; }
        public int Calories { get; set; }

    }
}
