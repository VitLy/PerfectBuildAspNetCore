using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class AddModifyTrainingSpecViewModel<T>
    {
        public int TrainingHeadId { get; set; }
        public string TrainingHeadName { get; set; }
        public DateTime TrainingHeadDate { get; set; }
        public string TrainingHeadCategory { get; set; }
        public List<T> TrainingSpecs { get; set; }
    }
}
