namespace PerfectBuild.Models.Interfaces
{
    interface ITrainingSpec
    {
        int ExId { get; set; }
        float Weight { get; set; }
        byte Set { get; set; }
        byte Amount { get; set; }
    }
}
