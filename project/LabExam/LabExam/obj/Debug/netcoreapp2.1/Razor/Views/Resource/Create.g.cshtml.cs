#pragma checksum "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\Resource\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a4d2c2c52af39c13bee0712bc2da5f4b2696366e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Resource_Create), @"mvc.1.0.view", @"/Views/Resource/Create.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Resource/Create.cshtml", typeof(AspNetCore.Views_Resource_Create))]
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
#line 1 "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\_ViewImports.cshtml"
using LabExam;

#line default
#line hidden
#line 2 "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\_ViewImports.cshtml"
using LabExam.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a4d2c2c52af39c13bee0712bc2da5f4b2696366e", @"/Views/Resource/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_Resource_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LabExam.Models.Entities.Cource>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/validation/jquery.validate.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/validation/messages_zh.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Resource", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal margin-top-30px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Upload", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\Resource\Create.cshtml"
  
    ViewData["Title"] = "实验室安全教育在线-添加资源";
    Layout = "~/Views/Shared/_BackEnd_Layout.cshtml";

#line default
#line hidden
            BeginContext(159, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(161, 63, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4884670c386e48409ca0008906d906f0", async() => {
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
            BeginContext(224, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(226, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5a2ca1a81b46423e89c4694815e1d5cc", async() => {
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
            BeginContext(281, 251, true);
            WriteLiteral("\r\n<div class=\" bc-clr-white padding-10px  border-little-grey-all\" data-height-all>\r\n    <h4 class=\"border-light-down font-size-15 font-weight-500 padding-bottom-10px \">\r\n        <span class=\" glyphicon glyphicon-plus\"></span> 添加课程资源\r\n    </h4>\r\n\r\n    ");
            EndContext();
            BeginContext(532, 4113, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "70acf4cb0f634a14ab005e0e8593b6eb", async() => {
                BeginContext(668, 457, true);
                WriteLiteral(@"
        <div class=""form-group"">
            <label for=""Name"" class=""col-sm-2 control-label"">
                <span class=""glyphicon glyphicon-object-align-bottom""></span>
                资源名称
            </label>
            <div class=""col-sm-10"">
                <input type=""text""  class=""form-control"" required data-max-width=""500"" id=""Name"" name=""Name""
                       placeholder=""资源名称"">
            </div>
        </div>
        ");
                EndContext();
                BeginContext(1126, 23, false);
#line 26 "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\Resource\Create.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(1149, 349, true);
                WriteLiteral(@"
        <div class=""form-group"">
            <label for=""CourceId"" class=""col-sm-2 control-label"">
                <span class=""glyphicon glyphicon-zoom-in""></span>
                所属课程
            </label>
            <div class=""col-sm-10"">
                <select id=""CourceId"" name=""CourceId"" class=""form-control"" data-max-width=""500"">
");
                EndContext();
#line 34 "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\Resource\Create.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
                BeginContext(1571, 24, true);
                WriteLiteral("                        ");
                EndContext();
                BeginContext(1595, 51, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef0122df8dc94aa28fa2dd3a663ffbb4", async() => {
                    BeginContext(1626, 1, true);
                    WriteLiteral(" ");
                    EndContext();
                    BeginContext(1628, 9, false);
#line 36 "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\Resource\Create.cshtml"
                                                   Write(item.Name);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#line 36 "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\Resource\Create.cshtml"
                           WriteLiteral(item.CourceId);

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
                BeginContext(1646, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 37 "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\Resource\Create.cshtml"
                    }

#line default
#line hidden
                BeginContext(1671, 439, true);
                WriteLiteral(@"                </select>
            </div>
        </div>
        <div class=""form-group"">
            <label for=""ResourceType"" class=""col-sm-2 control-label"">
                <span class=""glyphicon glyphicon-font""></span>
                资源类型
            </label>
            <div class=""col-sm-10"">
                <select id=""ResourceType"" name=""ResourceType"" class=""form-control"" data-max-width=""500"">
                    ");
                EndContext();
                BeginContext(2110, 32, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c15f66a52f854de18362ce77f6e7a9c4", async() => {
                    BeginContext(2128, 5, true);
                    WriteLiteral(" 视频资源");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2142, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(2164, 32, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a663461119ce49e1bdc001f13decba72", async() => {
                    BeginContext(2182, 5, true);
                    WriteLiteral(" 资源链接");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2196, 2100, true);
                WriteLiteral(@"
                </select>
                <span class=""help-block"">
                    只有视频资源才上传文件
                </span>
            </div>
        </div>
        <div class=""form-group"">
            <label for=""ResourceUrl"" class=""col-sm-2 control-label"">
                <span class=""glyphicon glyphicon-log-out""></span>
                资源链接
            </label>
            <div class=""col-sm-10"">
                <input type=""text"" class=""form-control"" id=""ResourceUrl"" name=""ResourceUrl"" data-max-width=""500""
                       placeholder=""资源url链接"" />
                <span class=""help-block"">
                    只有链接资源才需要填写
                </span>
            </div>
        </div>
        <div class=""form-group"">
            <label for=""LengthOfStudy"" class=""col-sm-2 control-label"">
                <span class=""glyphicon glyphicon-zoom-in""></span>
                学习时长
            </label>
            <div class=""col-sm-10"">
                <input type=""number"" required min=""0");
                WriteLiteral(@""" class=""form-control"" data-max-width=""500"" name=""LengthOfStudy"" id=""LengthOfStudy"" placeholder=""最低学习时长"">
            </div>
        </div>
        <div class=""form-group"">
            <label for=""loadFile"" class=""col-sm-2 control-label"">
                <span class=""glyphicon glyphicon-arrow-up""></span>
                上传文件
            </label>
            <div class=""col-sm-10"">
                <input type=""file"" name=""loadFile"" id=""loadFile"">
            </div>
        </div>
        <div class=""form-group"">
            <label for=""Description"" class=""col-sm-2 control-label"">
                <span class=""glyphicon glyphicon-eye-close""></span>
                资源描述
            </label>
            <div class=""col-sm-10"">
                <textarea type=""text"" rows=""10"" class=""form-control"" id=""Description"" name=""Description"" data-max-width=""500""
                          placeholder=""描述信息""></textarea>
            </div>
        </div>

        <div class=""form-group"">
            <div ");
                WriteLiteral("class=\"col-sm-offset-2 col-sm-10\">\r\n                ");
                EndContext();
                BeginContext(4296, 145, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3e57f47e2f374ed7aa2b12beb4a83201", async() => {
                    BeginContext(4382, 55, true);
                    WriteLiteral(" <span class=\"glyphicon glyphicon-send\"></span> 返回列表页面 ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(4441, 197, true);
                WriteLiteral("\r\n                <button type=\"submit\" class=\"  margin-left-25px  btn btn-primary\">  <span class=\"glyphicon glyphicon-floppy-saved\"></span> 立即保存 </button>\r\n            </div>\r\n        </div>\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4645, 1240, true);
            WriteLiteral(@"
</div>
<div class=""modal fade"" id=""program"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"" data-backdrop=""static"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>
                <h4 class=""modal-title font-weight-600 font-size-14 padding-top-10px text-info"">拼命加载数据中 <span id=""val-speed""></span> </h4>
            </div>
            <div class=""modal-body"">
                <div class=""progress progress-striped active"">
                    <div class=""progress-bar progress-bar-primary"" role=""progressbar"" aria-valuenow=""0""
                         aria-valuemin=""0"" aria-valuemax=""100"" style=""width: 0%;"">
                        <span class=""sr-only"">0% 完成（信息）</span>
                    </div>
                </div>
                <p class="" text-left"">
                   ");
            WriteLiteral(" <span class=\" glyphicon glyphicon-cloud-upload \"></span>\r\n                    <span id=\"data-info\">疯狂拼命搬运数据中, 请勿中途关闭窗口........</span>\r\n                </p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(5904, 8822, true);
                WriteLiteral(@"
    <script>
        $(""form"").validate({
            //错误提示信息
            messages: {
                Name: {
                    required: ""请填写资源名称""
                },
                LengthOfStudy: {
                    required: ""请填写题干"",
                    min: ""不要输入负数啊"",
                    digit: ""请填写整数"",
                    max: ""最长学习时间 {0}分钟""
                },
                Description: {
                    required: ""请填写你的资源描述"",
                    maxlength: ""描述过长！请不要超过{0}字""
                }
            },
            //验证规则
            rules: {
                Name: {
                    required: true
                },
                //使用空间 name 名称
                LengthOfStudy: {
                    required: true,
                    digits: true,
                    min: 0,
                    max: 120
                },
                Description: {
                    required: true,
                    maxlength: 400
                }
            ");
                WriteLiteral(@"},
            errorClass: ""text-primary"",
            submitHandler: function(form) {

                if ($('#ResourceType').val() == ""0"") {

                    if ($('#ResourceUrl').val() == null || $('#ResourceUrl').val() == """") {
                        onMask(""错误"", ""请填写资源链接URL！"");
                        return;
                    }
                    $.ajax({
                        url: ""/Resource/Link"",
                        type: ""post"",
                        dataType: ""json"",
                        data: {
                            Name: $('#Name').val(),
                            CourceId: $('#CourceId').val(),
                            Description: $('#Description').val(),
                            LengthOfStudy: $('#LengthOfStudy').val(),
                            ResourceUrl: $('#ResourceUrl').val(),
                            ResourceType: $('#ResourceType').val(),
                            __RequestVerificationToken: $('input[name=""__RequestVerificati");
                WriteLiteral(@"onToken""]').val()
                        },
                        success: function(json, textStatus, jqXhr) {
                            //debug
                            console.log(json);
                            //end debug
                            if (json.isOk) {
                                onMask(json.title, json.message);
                            } else {
                                onMask(json.title, json.message);
                            }
                        },
                        error: function(jqXHR, textStatus, errorThrown) {
                            onMask(""错误"", ""网络连接失败..."");
                        }
                    });
                } else if ($('#ResourceType').val() == ""1"") {

                    if ($('#loadFile').val() == null || $('#loadFile').val() == """") {
                        onMask(""错误"", ""请选择文件！"");
                        return;
                    }
                    else {
                        var fileo = ");
                WriteLiteral(@"$('input[type=""file""]').val();
                        var extension = fileo.split('.')[fileo.split('.').length - 1];

                        if (extension.toLowerCase() != ""mp4"") {
                            onMask(""错误"", ""请上传MP4文件！"");
                            return;
                        }
                    }

                    $.ajax({
                        url: ""/Resource/Exisit"",
                        type: ""post"",
                        dataType: ""json"",
                        data: {
                            name: $('#Name').val(),
                            type: $('#ResourceType').val()
                        },
                        success: function(json, textStatus, jqXhr) {
                            //debug
                            console.log(json);
                            //end debug
                            if (json.isOk) {
                                if (!json.isHave) {
                                    $('#program').modal('show");
                WriteLiteral(@"');
                                    //form.submit();

                                    var formData = new FormData($('form')[0]);


                                    var time = new Date().getTime(); //记录当前时间

                                    var percentage = null; //记录当前进度

                                    var velocity = null; //记录当前上传速度

                                    var loaded = 0; //记录已上传文件字节大小
                                    $.ajax({
                                        url: '/Resource/Upload',
                                        type: ""post"",
                                        data: formData,
                                        contentType: false, // 必须 不设置内容类型
                                        processData: false, // 必须 不处理数据
                                        xhr: function xhr() {
                                            //获取原生的xhr对象
                                            var xhr = $.ajaxSettings.xhr();
                   ");
                WriteLiteral(@"                         if (xhr.upload) {
                                                //添加 progress 事件监听
                                                xhr.upload.addEventListener('progress',
                                                    function (e) {
                                                        var nowDate = new Date().getTime();
                                                        //每一秒刷新一次状态
                                                        if (nowDate - time >= 1000) {
                                                            //已上传文件字节数/总字节数
                                                            percentage = parseInt(e.loaded / e.total * 100);
                                                            //当前已传大小(字节数)-一秒前已传文件大小(字节数)
                                                            velocity = (e.loaded - loaded) / 1024;
                                                            if (percentage >= 99) {
                                            ");
                WriteLiteral(@"                    $('.progress-bar-primary').css(""width"", ""100%"");
                                                            } else {
                                                                //修改上次记录时间及数据大小
                                                                time = nowDate;
                                                                loaded = e.loaded;
                                                            }
                                                            $('.progress-bar-primary').css(""width"", `${percentage}%`);
                                                            $('#val-speed').text(`上传速度: ${velocity}KB/s`);
                                                        } else {
                                                            return;
                                                        }
                                                    },
                                                    false);
                                    ");
                WriteLiteral(@"        }
                                            return xhr;
                                        },
                                        success: function (json, textStatus, jqXhr) {
                                            console.log(json);
                                            if (json.isOk) {
                                                $('.progress-bar-primary').css(""width"", ""100%"");
                                                $('#program #data-info').text('上传完毕！！！');
                                                form.reset();
                                            } else {
                                                onMask(json.title, json.message);
                                            }
          
                                        },
                                        error: function (jqXHR, textStatus, errorThrown) {
                                            console.log(errorThrown);
                                        }
 ");
                WriteLiteral(@"                                   });
                                }
                                else {
                                    onMask(json.title, json.message);
                                }
                            } else {
                                onMask(json.title, json.message);
                            }
                        },
                        error: function(jqXHR, textStatus, errorThrown) {
                            onMask(""错误"", ""网络连接失败..."");
                        }
                    });
                }
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LabExam.Models.Entities.Cource>> Html { get; private set; }
    }
}
#pragma warning restore 1591
