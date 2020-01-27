using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Infrastructure.TagHelpers
{
    [HtmlTargetElement("trainingDay")]
    public class TrainingDayTagHelper : TagHelper
    {
        private static readonly Dictionary<int, DayOfWeek> day = new Dictionary<int, DayOfWeek> {
            { 0, DayOfWeek.Monday },{ 1, DayOfWeek.Tuesday },{ 2, DayOfWeek.Wednesday },{ 3, DayOfWeek.Thursday },
            { 4, DayOfWeek.Friday },{ 5, DayOfWeek.Saturday },{ 6, DayOfWeek.Sunday }
        };

        IUrlHelperFactory urlHelperFactory;
        private IStringLocalizer<TrainingDayTagHelper> localizer;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public DayOfWeek CurrentDay { get; set; }
        public string Action { get; set; }

        public TrainingDayTagHelper(IUrlHelperFactory urlHelperFactory, IStringLocalizer<TrainingDayTagHelper> localizer)
        {
            this.urlHelperFactory = urlHelperFactory;
            this.localizer = localizer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder div = new TagBuilder("div");
            var currentDay = CurrentDay;
            for (int i = 0; i < 7; i++)
            {
                TagBuilder subdiv = new TagBuilder("div");
                TagBuilder a = new TagBuilder("a");
                if (day[i].Equals(CurrentDay))
                {
                    a.Attributes.Add("class", "btn btn-primary");
                }
                else
                {
                    a.Attributes.Add("class", "btn btn-light");
                }
                string href = urlHelper.Action(Action, new { dayTraining = day[i] });
                a.Attributes.Add("href", href);
                string localDay = localizer[day[i].ToString()];
                a.InnerHtml.SetContent(localDay);
                subdiv.InnerHtml.AppendHtml(a);
                div.InnerHtml.AppendHtml(subdiv);
            }
            output.Content.SetHtmlContent(div);
        }
    }
}
