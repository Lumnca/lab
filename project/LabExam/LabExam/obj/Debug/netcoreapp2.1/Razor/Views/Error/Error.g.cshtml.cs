#pragma checksum "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Error\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c7e66bfe9246f9c12e820884c4e175b520eb8718"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Error_Error), @"mvc.1.0.view", @"/Views/Error/Error.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Error/Error.cshtml", typeof(AspNetCore.Views_Error_Error))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c7e66bfe9246f9c12e820884c4e175b520eb8718", @"/Views/Error/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_Error_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Error\Error.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(29, 29, true);
            WriteLiteral("\r\n\r\n<!doctype html>\r\n<html>\r\n");
            EndContext();
            BeginContext(58, 5900, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fc48fdcda5c84f55b890131b2a95b313", async() => {
                BeginContext(64, 82, true);
                WriteLiteral("\r\n    <meta charset=\"utf-8\">\r\n    <title>404错误页效果</title>\r\n\r\n    <style>\r\n        ");
                EndContext();
                BeginContext(147, 88, true);
                WriteLiteral("@import url(\'https://fonts.googleapis.com/css?family=Montserrat:400,600,700\');\r\n        ");
                EndContext();
                BeginContext(236, 2365, true);
                WriteLiteral(@"@import url('https://fonts.googleapis.com/css?family=Catamaran:400,800');

        .error-container {
            text-align: center;
            font-size: 180px;
            font-family: 'Catamaran', sans-serif;
            font-weight: 800;
            margin: 20px 15px;
        }

            .error-container > span {
                display: inline-block;
                line-height: 0.7;
                position: relative;
                color: #FFB485;
            }

            .error-container > span {
                display: inline-block;
                position: relative;
                vertical-align: middle;
            }

                .error-container > span:nth-of-type(1) {
                    color: #D1F2A5;
                    animation: colordancing 4s infinite;
                }

                .error-container > span:nth-of-type(3) {
                    color: #F56991;
                    animation: colordancing2 4s infinite;
                }

    ");
                WriteLiteral(@"            .error-container > span:nth-of-type(2) {
                    width: 120px;
                    height: 120px;
                    border-radius: 999px;
                }

                    .error-container > span:nth-of-type(2):before,
                    .error-container > span:nth-of-type(2):after {
                        border-radius: 0%;
                        content: """";
                        position: absolute;
                        top: 0;
                        left: 0;
                        width: inherit;
                        height: inherit;
                        border-radius: 999px;
                        box-shadow: inset 30px 0 0 rgba(209, 242, 165, 0.4), inset 0 30px 0 rgba(239, 250, 180, 0.4), inset -30px 0 0 rgba(255, 196, 140, 0.4), inset 0 -30px 0 rgba(245, 105, 145, 0.4);
                        animation: shadowsdancing 4s infinite;
                    }

                    .error-container > span:nth-of-type(2):before {
              ");
                WriteLiteral(@"          -webkit-transform: rotate(45deg);
                        -moz-transform: rotate(45deg);
                        transform: rotate(45deg);
                    }

        .screen-reader-text {
            position: absolute;
            top: -9999em;
            left: -9999em;
        }

        ");
                EndContext();
                BeginContext(2602, 1203, true);
                WriteLiteral(@"@keyframes shadowsdancing {
            0% {
                box-shadow: inset 30px 0 0 rgba(209, 242, 165, 0.4), inset 0 30px 0 rgba(239, 250, 180, 0.4), inset -30px 0 0 rgba(255, 196, 140, 0.4), inset 0 -30px 0 rgba(245, 105, 145, 0.4);
            }

            25% {
                box-shadow: inset 30px 0 0 rgba(245, 105, 145, 0.4), inset 0 30px 0 rgba(209, 242, 165, 0.4), inset -30px 0 0 rgba(239, 250, 180, 0.4), inset 0 -30px 0 rgba(255, 196, 140, 0.4);
            }

            50% {
                box-shadow: inset 30px 0 0 rgba(255, 196, 140, 0.4), inset 0 30px 0 rgba(245, 105, 145, 0.4), inset -30px 0 0 rgba(209, 242, 165, 0.4), inset 0 -30px 0 rgba(239, 250, 180, 0.4);
            }

            75% {
                box-shadow: inset 30px 0 0 rgba(239, 250, 180, 0.4), inset 0 30px 0 rgba(255, 196, 140, 0.4), inset -30px 0 0 rgba(245, 105, 145, 0.4), inset 0 -30px 0 rgba(209, 242, 165, 0.4);
            }

            100% {
                box-shadow: inset 30px 0 0 rgba(209,");
                WriteLiteral(" 242, 165, 0.4), inset 0 30px 0 rgba(239, 250, 180, 0.4), inset -30px 0 0 rgba(255, 196, 140, 0.4), inset 0 -30px 0 rgba(245, 105, 145, 0.4);\r\n            }\r\n        }\r\n\r\n        ");
                EndContext();
                BeginContext(3806, 433, true);
                WriteLiteral(@"@keyframes colordancing {
            0% {
                color: RGB(48,115,172);
            }

            25% {
                color: RGB(41,144,232);
            }

            50% {
                color: RGB(214,216,218);
            }

            75% {
                color: RGB(135,182,203);
            }

            100% {
                color: RGB(48,115,172);
            }
        }

        ");
                EndContext();
                BeginContext(4240, 1711, true);
                WriteLiteral(@"@keyframes colordancing2 {
            0% {
                color: RGB(48,115,172);
            }

            25% {
                color: RGB(41,144,232);
            }

            50% {
                color: RGB(214,216,218);
            }

            75% {
                color: RGB(135,182,203);
            }

            100% {
                color: RGB(48,115,172);
            }
        }

        /* demo stuff */
        * {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        body {
            background-color: RGB(48,115,172);
            margin-bottom: 50px;
        }

        html, button, input, select, textarea {
            font-family: 'Montserrat', Helvetica, sans-serif;
            color: white;
        }

        h1 {
            text-align: center;
            margin: 30px 15px;
        }

        .zoom-area {
            max-width: 490px;
            ma");
                WriteLiteral(@"rgin: 30px auto 30px;
            font-size: 16px;
            font-weight: 600;
            text-align: center;
        }

        .link-container {
            text-align: center;
        }

        a.more-link {
            text-transform: uppercase;
            font-size: 13px;
            background-color: #92a4ad;
            padding: 10px 15px;
            border-radius: 0;
            color: #416475;
            display: inline-block;
            margin-right: 5px;
            margin-bottom: 5px;
            line-height: 1.5;
            text-decoration: none;
            margin-top: 50px;
            letter-spacing: 1px;
        }
    </style>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5958, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(5960, 260, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b73746c5df72455a8ceb9a515eb09eb9", async() => {
                BeginContext(5966, 12, true);
                WriteLiteral("\r\n\r\n    <h1>");
                EndContext();
                BeginContext(5979, 12, false);
#line 199 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Error\Error.cshtml"
   Write(ViewBag.Code);

#line default
#line hidden
                EndContext();
                BeginContext(5991, 222, true);
                WriteLiteral(" 错误页面 !</h1>\r\n    <p class=\"zoom-area\">页面错误!!! </p>\r\n    <section class=\"error-container\">\r\n        <span>4</span>\r\n        <span><span class=\"screen-reader-text\">0</span></span>\r\n        <span>4</span>\r\n    </section>\r\n\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6220, 11, true);
            WriteLiteral("\r\n</html>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
