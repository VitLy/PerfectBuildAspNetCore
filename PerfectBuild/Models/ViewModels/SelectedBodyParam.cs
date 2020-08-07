using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectBuild.Models.ViewModels
{
    public class SelectedBodyParam
    {
        public BodyParameter BodyParameter { get; set; }
        public bool Select { get; set; } = false;

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("[").Append("\"").Append(this.BodyParameter.ToString()).Append("\"").Append(",")
                .Append("\"").Append(Select.ToString()).Append("\"").Append("]");

            return result.ToString();
        }
    }
}
