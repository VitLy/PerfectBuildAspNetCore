using Newtonsoft.Json;

namespace PerfectBuild.Models.Report
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Title
    {
        [JsonProperty(PropertyName = "text")]
        private readonly string text;

        public Title(string text)
        {
            this.text = text;
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Diagram<Tx, Ty>
    {
        [JsonProperty(PropertyName = "title")]
        public Title Tittle { get; set; }

        public Diagram(string tittle)
        {
            this.Tittle = new Title(tittle);
        }
    }
}