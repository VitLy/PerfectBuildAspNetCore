using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
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
        public IList<SelectedBodyParam> BodyParameters { get; set; }
        private IStringLocalizer<BodyParamsInRowTagHelper> localizer;

        public BodyParamsInRowTagHelper(IStringLocalizer<BodyParamsInRowTagHelper> localizer)
        {
            this.localizer = localizer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "form-row");
            output.TagMode = TagMode.StartTagAndEndTag;

            TagBuilder divCheck = new TagBuilder("div");
            divCheck.AddCssClass("form-check form-check-inline");

            for(int i = 0; i < BodyParameters.Count; i++)
            {
                string name = BodyParameters[i].BodyParameter.ToString();

                TagBuilder inputBodyParam = new TagBuilder("input");
                inputBodyParam.TagRenderMode = TagRenderMode.SelfClosing;
                inputBodyParam.MergeAttribute("hidden", "hidden");
                inputBodyParam.MergeAttribute("name", "[" + i + "].BodyParameter");
                inputBodyParam.MergeAttribute("value", name);

                TagBuilder inputCheck = new TagBuilder("input");
                inputCheck.TagRenderMode = TagRenderMode.SelfClosing;
                inputCheck.AddCssClass("form-check-input");
                inputCheck.MergeAttribute("type", "checkbox");
                inputCheck.MergeAttribute("value", "true");
                inputCheck.MergeAttribute("id", "["+i+"].BodyParameter");
                inputCheck.MergeAttribute("name", "[" + i + "].Select");

                if (BodyParameters[i].Select)
                {
                    inputCheck.MergeAttribute("checked", "checked");
                }

                TagBuilder label = new TagBuilder("label");
                label.AddCssClass("form-check-label");
                label.InnerHtml.Append(localizer[name]+":");
                label.MergeAttribute("for", "[" + i + "].BodyParameter");

                divCheck.InnerHtml.AppendHtml(label);
                divCheck.InnerHtml.AppendHtml(inputCheck);
                divCheck.InnerHtml.AppendHtml(inputBodyParam);
            }
                output.Content.AppendHtml(divCheck);
        }
    }
}


//@model IList<BodyParamsViewModel>

//        @for(int i = 0; i<Model.Count; i++)
//        {
//            <div class="form-group">
//                <input type = "checkbox" asp-for=@Model[i].IsSelected /> 
//                <label asp-for=@Model[i].IsSelected>@Model[i].Name</label>
//                <input hidden = "hidden" asp-for=@Model[i].Name/>
//            </div>

//        }
//        <input type = "submit" />
//    </ form >