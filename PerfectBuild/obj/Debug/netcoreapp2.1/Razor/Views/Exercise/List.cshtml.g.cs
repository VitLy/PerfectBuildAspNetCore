#pragma checksum "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\Exercise\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "476ebf4aeeb54b562df4fa16969bb21665e06437"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Exercise_List), @"mvc.1.0.view", @"/Views/Exercise/List.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Exercise/List.cshtml", typeof(AspNetCore.Views_Exercise_List))]
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
#line 2 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\_ViewImports.cshtml"
using PerfectBuild.Models;

#line default
#line hidden
#line 3 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\_ViewImports.cshtml"
using PerfectBuild.Infrastructure;

#line default
#line hidden
#line 4 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\_ViewImports.cshtml"
using PerfectBuild.Models.ViewModels;

#line default
#line hidden
#line 5 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"476ebf4aeeb54b562df4fa16969bb21665e06437", @"/Views/Exercise/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a5406697e68548f8f61dab49083073ca68599ed1", @"/Views/_ViewImports.cshtml")]
    public class Views_Exercise_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Exercise>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(31, 17, true);
            WriteLiteral("\r\n<div>\r\n    <h1>");
            EndContext();
            BeginContext(49, 5, false);
#line 4 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\Exercise\List.cshtml"
   Write(Model);

#line default
#line hidden
            EndContext();
            BeginContext(54, 20, true);
            WriteLiteral("</h1>\r\n\r\n    <div>\r\n");
            EndContext();
#line 7 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\Exercise\List.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(123, 56, true);
            WriteLiteral("            <div class=\"row\">\r\n                <div col>");
            EndContext();
            BeginContext(180, 7, false);
#line 10 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\Exercise\List.cshtml"
                    Write(item.Id);

#line default
#line hidden
            EndContext();
            BeginContext(187, 33, true);
            WriteLiteral("</div>\r\n                <div col>");
            EndContext();
            BeginContext(221, 9, false);
#line 11 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\Exercise\List.cshtml"
                    Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(230, 33, true);
            WriteLiteral("</div>\r\n                <div col>");
            EndContext();
            BeginContext(264, 16, false);
#line 12 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\Exercise\List.cshtml"
                    Write(item.Description);

#line default
#line hidden
            EndContext();
            BeginContext(280, 28, true);
            WriteLiteral("</div>\r\n            </div>\r\n");
            EndContext();
#line 14 "D:\My\Programm\Projects\PerfectBuildWeb\PerfectBuild\Views\Exercise\List.cshtml"
        }

#line default
#line hidden
            BeginContext(319, 20, true);
            WriteLiteral("    </div>\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Exercise>> Html { get; private set; }
    }
}
#pragma warning restore 1591
