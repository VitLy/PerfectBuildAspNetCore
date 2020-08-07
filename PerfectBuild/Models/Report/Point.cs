using Newtonsoft.Json;

namespace PerfectBuild.Models.Report
{
    [JsonObject]
    public struct Point<Tx, Ty>
    {
        [JsonProperty(PropertyName ="x")]
        public Tx X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public Ty Y { get; set; }
        //[JsonProperty]
        [JsonIgnore]
        public string Label { get; set; }
    }
}