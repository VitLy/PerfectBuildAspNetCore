using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PerfectBuild.Models;
using PerfectBuild.Models.ViewModels;
using System.Collections.Generic;

namespace PerfectBuild.Infrastructure.TagHelpers
{
    /// <summary>
    /////            <div class="form-row">
    //                <div class="form-check form-check-inline">
    //                    <input class="form-check-input" name="userBodyParam" type="checkbox" value="[Breast, False]" />
    //                    <label class="form-check-label" for="userBodyParam"></label>
    //                </div>
    //                <div class="form-check form-check-inline">
    //                <input class="form-check-input" name="userBodyParam" type="checkbox" value="[Buttock, False]" />
    //                <label class="form-check-label" for="userBodyParam"></label>
    //                    </div>
    //                <div class="form-check form-check-inline">
    //                <input class="form-check-input" name="userBodyParam" type="checkbox" value="[Thigh, False]" />
    //                <label class="form-check-label" for="userBodyParam"></label>
    //                    </div>
    //                <div class="form-check form-check-inline">
    //                <input class="form-check-input" name="userBodyParam" type="checkbox" value="[Waist, False]" /><label class="form-check-label" for="userBodyParam"></label></div><div class="form-check form-check-inline"><input class="form-check-input" name="userBodyParam" type="checkbox" value="[Weight, False]" /><label class="form-check-label" for="userBodyParam"></label></div>
    //            </div>
    /// </summary>

    [HtmlTargetElement("body-params-in-row")]
    public class BodyParamsInRowTagHelper : TagHelper
    {
        public IEnumerable<SelectedBodyParam> BodyParameters { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "form-row");
            output.TagMode = TagMode.StartTagAndEndTag;

            TagBuilder divCheck = new TagBuilder("div");
            divCheck.AddCssClass("form-check form-check-inline");
            int count = 0;

            foreach (var item in BodyParameters)
            {
                string name = "userBodyParam" + (++count).ToString();

                TagBuilder input = new TagBuilder("input");
                input.TagRenderMode = TagRenderMode.SelfClosing;
                input.AddCssClass("form-check-input");
                input.MergeAttribute("type", "checkbox");
                input.MergeAttribute("value", item.ToString());
                input.MergeAttribute("id", name);
                input.MergeAttribute("name", name);

                if (item.Select)
                {
                    input.MergeAttribute("checked", "checked");
                }

                TagBuilder label = new TagBuilder("label");
                label.AddCssClass("form-check-label");
                label.InnerHtml.Append(item.BodyParameter.ToString());
                label.MergeAttribute("for", name);

                divCheck.InnerHtml.AppendHtml(input);
                divCheck.InnerHtml.AppendHtml(label);
            }
                output.Content.AppendHtml(divCheck);
        }
    }
}
