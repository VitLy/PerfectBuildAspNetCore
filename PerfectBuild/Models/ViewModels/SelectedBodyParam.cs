using System.Text;

namespace PerfectBuild.Models.ViewModels
{
    public class SelectedBodyParam
    {
        public BodyParameter BodyParameter { get; set; }
        public bool Select { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("[").Append("\"").Append(this.BodyParameter.ToString()).Append("\"").Append(",")
                .Append("\"").Append(Select.ToString()).Append("\"").Append("]");

            return result.ToString();
        }
    }
}
