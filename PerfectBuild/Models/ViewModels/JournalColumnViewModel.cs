using System;

namespace PerfectBuild.Models.ViewModels
{
    public class JournalColumnViewModel
    {
        public int HeadId { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public string DocumName { get; set; }
        public int Calories { get; set; }
        public int ExerciseCount { get; set; }
        public int SetMax { get; set; }
        public double Duration { get; set; }
    }
}
