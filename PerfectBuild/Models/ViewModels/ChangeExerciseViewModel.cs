using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Models.ViewModels
{
    public class ChangeExerciseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitId { get; set; }
        public bool OwnWeight { get; set; }

        public List<Unit> Units { get; set; }
    }
}
