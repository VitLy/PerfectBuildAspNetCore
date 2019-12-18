using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PerfectBuild.Infrastructure.TagHelpers
{
    [HtmlTargetElement("paginator")]
    public class PaginatorTagHelper : TagHelper
    {
        IUrlHelperFactory urlHelperFactory;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public string PageAction { get; set; }

        public PaginatorTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder ul = new TagBuilder("ul");
            ul.Attributes.Add("class", "pagination");
            TagBuilder nav = new TagBuilder("nav");
            nav.Attributes.Add("arial-lebel", "");
            for (int i = 1; i <= TotalPage; i++)
            {
                TagBuilder li = new TagBuilder("li");
                if (i == CurrentPage)
                {
                    li.Attributes.Add("class", "page-item active");
                }
                else
                {
                    li.Attributes.Add("class", "page-item");
                }

                TagBuilder a = new TagBuilder("a");
                a.Attributes.Add("class", "page-link");
                string href = urlHelper.Action(PageAction, new { currentPage = i });
                a.InnerHtml.SetContent(i.ToString());
                a.Attributes.Add("href", href);
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }
            nav.InnerHtml.AppendHtml(ul);
            output.Content.SetHtmlContent(nav);
        }
    }
}
