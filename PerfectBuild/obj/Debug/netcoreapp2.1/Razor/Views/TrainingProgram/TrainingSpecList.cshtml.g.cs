#pragma checksum "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c5a3deeb884b11e8ea79993b0d11277fdf23bb52"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TrainingProgram_TrainingSpecList), @"mvc.1.0.view", @"/Views/TrainingProgram/TrainingSpecList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/TrainingProgram/TrainingSpecList.cshtml", typeof(AspNetCore.Views_TrainingProgram_TrainingSpecList))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 4 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\_ViewImports.cshtml"
using PerfectBuild.Models;

#line default
#line hidden
#line 5 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\_ViewImports.cshtml"
using PerfectBuild.Infrastructure;

#line default
#line hidden
#line 6 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\_ViewImports.cshtml"
using PerfectBuild.Models.ViewModels;

#line default
#line hidden
#line 7 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c5a3deeb884b11e8ea79993b0d11277fdf23bb52", @"/Views/TrainingProgram/TrainingSpecList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f1b3fd339342b04f6e87691148996dacff1f1e24", @"/Views/_ViewImports.cshtml")]
    public class Views_TrainingProgram_TrainingSpecList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AddModifyTrainingSpecViewModel<TrainingProgramSpec>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddModifyTrainingSpecLine", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-specId", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteTrainingSpecLine", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(150, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 5 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
  
    int i = 1;

#line default
#line hidden
            BeginContext(175, 47, true);
            WriteLiteral("\r\n<div class=\"bg-primary text-white\">\r\n    <h3>");
            EndContext();
            BeginContext(223, 37, false);
#line 10 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
   Write(Localizer["TittleTrainigProgramSpec"]);

#line default
#line hidden
            EndContext();
            BeginContext(260, 16, true);
            WriteLiteral(":</h3>\r\n    <h4>");
            EndContext();
            BeginContext(277, 22, false);
#line 11 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
   Write(Model.TrainingHeadName);

#line default
#line hidden
            EndContext();
            BeginContext(299, 2, true);
            WriteLiteral(", ");
            EndContext();
            BeginContext(302, 22, false);
#line 11 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                            Write(Model.TrainingHeadDate);

#line default
#line hidden
            EndContext();
            BeginContext(324, 2, true);
            WriteLiteral(" (");
            EndContext();
            BeginContext(327, 26, false);
#line 11 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                                                     Write(Model.TrainingHeadCategory);

#line default
#line hidden
            EndContext();
            BeginContext(353, 135, true);
            WriteLiteral(")</h4>\r\n</div>\r\n\r\n<div class=\"container-fluid\">\r\n    <div class=\"row\">\r\n        <div class=\"col-1\">#</div>\r\n        <div class=\"col-4\">");
            EndContext();
            BeginContext(489, 27, false);
#line 17 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                      Write(LocalizerShared["Exercise"]);

#line default
#line hidden
            EndContext();
            BeginContext(516, 35, true);
            WriteLiteral("</div>\r\n        <div class=\"col-1\">");
            EndContext();
            BeginContext(552, 22, false);
#line 18 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                      Write(LocalizerShared["Set"]);

#line default
#line hidden
            EndContext();
            BeginContext(574, 35, true);
            WriteLiteral("</div>\r\n        <div class=\"col-2\">");
            EndContext();
            BeginContext(610, 25, false);
#line 19 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                      Write(LocalizerShared["Weight"]);

#line default
#line hidden
            EndContext();
            BeginContext(635, 35, true);
            WriteLiteral("</div>\r\n        <div class=\"col-2\">");
            EndContext();
            BeginContext(671, 25, false);
#line 20 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                      Write(LocalizerShared["Amount"]);

#line default
#line hidden
            EndContext();
            BeginContext(696, 35, true);
            WriteLiteral("</div>\r\n        <div class=\"col-1\">");
            EndContext();
            BeginContext(731, 155, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "377e30dbf1b44ea89b609d2d1e43f6e2", async() => {
                BeginContext(860, 22, false);
#line 21 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                                                                                                                                                      Write(LocalizerShared["Add"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-headId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 21 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                                                                           WriteLiteral(Model.TrainingHeadId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["headId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-headId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["headId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-specId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["specId"] = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(886, 80, true);
            WriteLiteral("</div>\r\n        <div class=\"col-1\"></div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n");
            EndContext();
#line 26 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
         foreach (TrainingProgramSpec spec in Model.TrainingSpecs)
        {

#line default
#line hidden
            BeginContext(1045, 31, true);
            WriteLiteral("            <div class=\"col-1\">");
            EndContext();
            BeginContext(1078, 3, false);
#line 28 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                           Write(i++);

#line default
#line hidden
            EndContext();
            BeginContext(1082, 39, true);
            WriteLiteral("</div>\r\n            <div class=\"col-4\">");
            EndContext();
            BeginContext(1122, 18, false);
#line 29 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                          Write(spec.Exercise.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1140, 39, true);
            WriteLiteral("</div>\r\n            <div class=\"col-1\">");
            EndContext();
            BeginContext(1180, 8, false);
#line 30 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                          Write(spec.Set);

#line default
#line hidden
            EndContext();
            BeginContext(1188, 39, true);
            WriteLiteral("</div>\r\n            <div class=\"col-2\">");
            EndContext();
            BeginContext(1228, 11, false);
#line 31 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                          Write(spec.Weight);

#line default
#line hidden
            EndContext();
            BeginContext(1239, 39, true);
            WriteLiteral("</div>\r\n            <div class=\"col-2\">");
            EndContext();
            BeginContext(1279, 11, false);
#line 32 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                          Write(spec.Amount);

#line default
#line hidden
            EndContext();
            BeginContext(1290, 39, true);
            WriteLiteral("</div>\r\n            <div class=\"col-1\">");
            EndContext();
            BeginContext(1329, 165, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5a6bc0132b4848748f94f85f4fee5d2c", async() => {
                BeginContext(1465, 25, false);
#line 33 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                                                                                                                                                                 Write(LocalizerShared["Modify"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-headId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 33 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                                                                                                       WriteLiteral(Model.TrainingHeadId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["headId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-headId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["headId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 33 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                                                                                                                                                WriteLiteral(spec.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["specId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-specId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["specId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1494, 39, true);
            WriteLiteral("</div>\r\n            <div class=\"col-1\">");
            EndContext();
            BeginContext(1533, 161, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a4918918f5374739baaf1743e2adbeb3", async() => {
                BeginContext(1665, 25, false);
#line 34 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                                                                                                                                                             Write(LocalizerShared["Delete"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-headId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 34 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                                                                                                   WriteLiteral(Model.TrainingHeadId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["headId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-headId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["headId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 34 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                                                                                                                                            WriteLiteral(spec.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["specId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-specId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["specId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1694, 8, true);
            WriteLiteral("</div>\r\n");
            EndContext();
#line 35 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
        }

#line default
#line hidden
            BeginContext(1713, 16, true);
            WriteLiteral("    </div>\r\n    ");
            EndContext();
            BeginContext(1729, 102, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "efb155df31a84c918427ecf10b965b08", async() => {
                BeginContext(1797, 30, false);
#line 37 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\TrainingProgram\TrainingSpecList.cshtml"
                                                                  Write(LocalizerShared["CloseButton"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1831, 12, true);
            WriteLiteral("\r\n</div>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer Localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHtmlLocalizer<SharedResource> LocalizerShared { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AddModifyTrainingSpecViewModel<TrainingProgramSpec>> Html { get; private set; }
    }
}
#pragma warning restore 1591
