using PerfectBuild.Models.Document;
using System;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class LastTraining
    {
        public long StartedTraining { get; set; }
        public long EndTraining { get; set; }
        public List<Line> Exercises { get; set; }
    }
}
