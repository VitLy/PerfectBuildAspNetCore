using System;

namespace PerfectBuild.Models.Document
{
    public class Line:IComparable
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int ExerciseId { get; set; }
        public string Exercise { get; set; }
        public byte Set { get; set; }
        public float Weight { get; set; }
        public byte Amount { get; set; }

        public int CompareTo(object obj)
        {
            Line line = (Line)obj;
            return Order.CompareTo(line.Order);
        }
    }
}
