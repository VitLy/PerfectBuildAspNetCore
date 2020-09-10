using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using PerfectBuild.Infrastructure;

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
        [JsonIgnore]
        protected IStringLocalizer localizer;

        [JsonProperty(PropertyName = "title")]
        public Title Tittle { get; set; }
      
        public Diagram(string tittle, IStringLocalizer localizer)
        {
            this.Tittle = new Title(tittle);
            this.localizer = localizer;
        }
    }
}