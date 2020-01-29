using System;

namespace PerfectBuild.Models.Interfaces
{
    public interface IHead
    {
        int Id { get; set; }

        DateTime Date { get; set; }

        int Number { get; set; }

        string Name { get; set; }

        string UserId { get; set; }
    }
}
