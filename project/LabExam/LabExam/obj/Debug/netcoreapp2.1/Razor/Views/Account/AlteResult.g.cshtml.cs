#pragma checksum "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Account\AlteResult.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f95193c535906211ebb01a772efa4e22c98a1ff5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_AlteResult), @"mvc.1.0.view", @"/Views/Account/AlteResult.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/AlteResult.cshtml", typeof(AspNetCore.Views_Account_AlteResult))]
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
#line 1 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\_ViewImports.cshtml"
using LabExam;

#line default
#line hidden
#line 2 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\_ViewImports.cshtml"
using LabExam.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f95193c535906211ebb01a772efa4e22c98a1ff5", @"/Views/Account/AlteResult.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_AlteResult : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Account\AlteResult.cshtml"
  
    ViewData["Title"] = "实验室安全教育在线-修改结果";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(99, 1722, true);
            WriteLiteral(@"<nav class=""narbar-mobile-fui navbar navbar-default navbar-fixed-top   bc-clr-white"" role=""navigation"">
    <div class=""container-fluid"">
        <div class=""navbar-header"">
            <button type=""button"" class=""navbar-toggle"" data-toggle=""collapse"" data-target=""#example-navbar-collapse"">
                <span class=""icon-bar""></span>
                <span class=""icon-bar""></span>
                <span class=""icon-bar""></span>
            </button>
            <a class=""navbar-brand"" href=""#"">
                <span class="" text-primary font-weight-700"">SICNU 实验室安全教育</span>
            </a>
        </div>
        <div class=""collapse navbar-collapse "" id=""example-navbar-collapse"">
            <ul class=""nav navbar-nav "">
                <li role=""presentation""><a href=""#""> <span class="" text-primary font-weight-500 "">系统公告</span> </a></li>
                <li role=""presentation""><a href=""#""><span class="" text-primary font-weight-500 "">使用帮助</span></a></li>
                <li role=""presentatio");
            WriteLiteral(@"n""><a href=""#""><span class="" text-primary font-weight-500 "">申请加入考试</span></a></li>
                <li role=""presentation""><a href=""#""><span class="" text-primary font-weight-500 "">密码修改</span></a></li>
                <li role=""presentation""><a href=""#""><span class="" text-primary font-weight-500 "">用户登录</span></a></li>
            </ul>
        </div>
    </div>
</nav>
<div class="" container"" data-min-height=""900"">
    <div class="" margin-top-15px padding-15px bc-clr-white border-little-grey-all "" data-min-height=""70"">
        <h4 class="" border-light-down padding-bottom-5px text-primary "">提示信息</h4>
        <div>
            <h4 class="" text-center font-size-17 font-weight-700 ""> ");
            EndContext();
            BeginContext(1822, 14, false);
#line 33 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Account\AlteResult.cshtml"
                                                               Write(ViewBag.Header);

#line default
#line hidden
            EndContext();
            BeginContext(1836, 226, true);
            WriteLiteral(" </h4>\r\n            <div class=\" text-center margin-top-25px margin-bottom-15px\">\r\n                <a href=\"/Home/Index\" class=\" btn  btn-primary \"> <span class=\" glyphicon glyphicon-ok \"></span> 前往系统主页</a>\r\n                <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2062, "\"", 2086, 1);
#line 36 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Account\AlteResult.cshtml"
WriteAttributeValue("", 2069, ViewBag.RightUrl, 2069, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2087, 75, true);
            WriteLiteral(" class=\" btn btn-default \"> <span class=\"glyphicon glyphicon-user\"></span> ");
            EndContext();
            BeginContext(2163, 18, false);
#line 36 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Account\AlteResult.cshtml"
                                                                                                                 Write(ViewBag.RightTitle);

#line default
#line hidden
            EndContext();
            BeginContext(2181, 64, true);
            WriteLiteral("</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2264, 132, true);
                WriteLiteral("\r\n    <script>\r\n        $(\'body\').removeClass(\"bc-clr-grey-little\");\r\n        $(\'body\').addClass(\"bc-clr-primary\");\r\n    </script>\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
