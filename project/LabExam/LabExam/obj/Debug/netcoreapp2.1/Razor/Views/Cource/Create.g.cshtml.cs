#pragma checksum "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Cource\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b05a99d63d97e7e93d4797debfb019ae6c5cd9c3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cource_Create), @"mvc.1.0.view", @"/Views/Cource/Create.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Cource/Create.cshtml", typeof(AspNetCore.Views_Cource_Create))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b05a99d63d97e7e93d4797debfb019ae6c5cd9c3", @"/Views/Cource/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_Cource_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LabExam.Models.Entities.Module>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/validation/jquery.validate.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/validation/messages_zh.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Cource", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal margin-top-30px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(52, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Cource\Create.cshtml"
  
    ViewData["Title"] = "实验室安全教育在线-添加课程";
    Layout = "~/Views/Shared/_BackEnd_Layout.cshtml";

#line default
#line hidden
            BeginContext(159, 63, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4e990790d7c44b8d9cbe5085453160fc", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(222, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(224, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "813ad48df67148d7aeb8b182805bc3d6", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(279, 249, true);
            WriteLiteral("\r\n<div class=\" bc-clr-white padding-10px  border-little-grey-all\" data-height-all>\r\n    <h4 class=\"border-light-down font-size-15 font-weight-500 padding-bottom-10px \">\r\n        <span class=\" glyphicon glyphicon-plus\"></span> 添加课程信息\r\n    </h4>\r\n    ");
            EndContext();
            BeginContext(528, 3141, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "47b55c1cbb9a4c559f0526921d788394", async() => {
                BeginContext(574, 445, true);
                WriteLiteral(@"
        <div class=""form-group"">
            <label for=""courceName"" class=""col-sm-2  control-label"">
                <span class=""glyphicon glyphicon-folder-open""></span>
                课程名称
            </label>
            <div class=""col-sm-8"">
                <input type=""text"" class=""form-control"" required data-max-width=""400"" id=""courceName"" name=""courceName"" placeholder=""请输入课程名称"">
            </div>
        </div>
        ");
                EndContext();
                BeginContext(1020, 23, false);
#line 23 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Cource\Create.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(1043, 361, true);
                WriteLiteral(@"
        <div class=""form-group"">
            <label for=""ModuleId"" class=""col-sm-2 control-label"">
                <span class=""glyphicon glyphicon-object-align-bottom""></span>
                所属模块
            </label>
            <div class=""col-sm-10"">
                <select name=""ModuleId"" id=""ModuleId"" class=""form-control"" data-max-width=""400"">
");
                EndContext();
#line 31 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Cource\Create.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
                BeginContext(1477, 24, true);
                WriteLiteral("                        ");
                EndContext();
                BeginContext(1501, 51, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eb0a5dfc8ff24339bcea0c3523632c37", async() => {
                    BeginContext(1534, 9, false);
#line 33 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Cource\Create.cshtml"
                                                   Write(item.Name);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#line 33 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Cource\Create.cshtml"
                            WriteLiteral(item.ModuleId);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1552, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 34 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Cource\Create.cshtml"
                    }

#line default
#line hidden
                BeginContext(1577, 1642, true);
                WriteLiteral(@"                </select>
            </div>
        </div>
        <div class=""form-group"">
            <label for=""courceName"" class=""col-sm-2  control-label"">
                <span class=""glyphicon glyphicon-fire""></span>
                学分
            </label>
            <div class=""col-sm-8"">
                <input class=""form-control"" type=""number"" required data-max-width=""400"" id=""Mark"" name=""Mark"" placeholder=""请输入学分"">
            </div>
        </div>
        <div class=""form-group"">
            <label class=""col-sm-2  control-label"">
                <span class=""glyphicon glyphicon-folder-open""></span>
                课程描述
            </label>
            <div class=""col-sm-8"">
                <textarea class=""form-control"" id=""description"" name=""description"" data-max-width=""400"" data-min-height=""120"" ></textarea>
            </div>
        </div>
        <div class=""form-group"">
            <label  class=""col-sm-2 control-label"">
                <span class=""	glyphicon glyphi");
                WriteLiteral(@"con-user""></span>
                添加人员
            </label>
            <div class=""col-sm-10"">
                <p class=""form-control-static"">自动识别</p>
            </div>
        </div>
        <div class=""form-group"">
            <label class=""col-sm-2 control-label"">
                <span class=""glyphicon glyphicon-th""></span>
                添加时间
            </label>
            <div class=""col-sm-10"">
                <p class=""form-control-static"">自动识别</p>
            </div>
        </div>
        <div class=""form-group"">
            <div class=""col-sm-offset-2 col-sm-10"">
                ");
                EndContext();
                BeginContext(3219, 188, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "934c93b2a90d470c89576b251fc073c6", async() => {
                    BeginContext(3289, 114, true);
                    WriteLiteral("\r\n                    <span class=\"glyphicon glyphicon-send\"></span>\r\n                    返回列表页面\r\n                ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3407, 255, true);
                WriteLiteral("\r\n                <button type=\"submit\" class=\"  margin-left-25px  btn btn-primary\">\r\n                    <span class=\"glyphicon glyphicon-floppy-saved\"></span>\r\n                    立即添加\r\n                </button>\r\n            </div>\r\n        </div>\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3669, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(3698, 1875, true);
                WriteLiteral(@"
    <script>
        $(""form"").validate({
            //错误提示信息
            messages: {
                courceName: {
                    required: ""请填写课程名称""
                },
                Mark: {
                    required: ""请填写学分""
                },
                description: {
                    required: ""请填写课程描述""
                }
            },
            //验证规则
            rules: {
                courceName: {
                    required: true
                },
                Mark: {
                    required: true
                },
                description: {
                    required: true
                }
            },
            errorClass: ""text-primary"",
            submitHandler: function (form) {
                var token = $('form input[name=""__RequestVerificationToken""]').val();
                $.ajax({
                    url: ""/Cource/Create"",
                    type: ""post"",
                    dataType: ""json"",
                ");
                WriteLiteral(@"    data: {
                        __RequestVerificationToken: token,
                        name: $('form input[name=""courceName""]').val(),
                        moduleId: $('#ModuleId').val(),
                        mark: $('#Mark').val(),
                        description: $('#description').val()
                    },
                    success: function (json, textStatus, jqXhr) {
                        //debug
                        console.log(json);
                        //end debug
                        form.reset();
                        onMask(json.title, json.message);
                    },
                    error: function(jqXHR, textStatus, errorThrown) {
                        onMask(""错误"", ""网络连接失败..."");
                    }
                });
            }
        });
    </script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LabExam.Models.Entities.Module>> Html { get; private set; }
    }
}
#pragma warning restore 1591
