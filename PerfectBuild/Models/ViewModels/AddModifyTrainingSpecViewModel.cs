using PerfectBuild.Models.Document;
using System;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class AddModifyTrainingSpecViewModel
    {
        public int TrainingHeadId { get; set; }
        public string TrainingHeadName { get; set; }
        public DateTime TrainingHeadDate { get; set; }
        public string TrainingHeadCategory { get; set; }
        public List<TrainingProgramSpec> TrainingSpecs { get; set; }
    }
}
