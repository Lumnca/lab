#pragma checksum "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e7b8befa192145595d118a8e8924723a728acdfd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_Paper), @"mvc.1.0.view", @"/Views/Student/Paper.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Student/Paper.cshtml", typeof(AspNetCore.Views_Student_Paper))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e7b8befa192145595d118a8e8924723a728acdfd", @"/Views/Student/Paper.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_Student_Paper : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LabExam.Models.Entities.ExaminationPaper>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Student", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Exam", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-info continue-exam"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(62, 363, true);
            WriteLiteral(@"<table class=""table table-hover"">
    <thead>
        <tr>
            <th>编号</th>
            <th>学号</th>
            <th>通过分数</th>
            <th>允许考试时间</th>
            <th>试卷总分</th>
            <th>考试得分</th>
            <th>考试时间</th>
            <th>是否完成</th>
            <th class="" text-right"">操作</th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 17 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
      
        int index = 1;
    

#line default
#line hidden
            BeginContext(464, 4, true);
            WriteLiteral("    ");
            EndContext();
#line 20 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
            BeginContext(505, 65, true);
            WriteLiteral("        <tr>\r\n            <td> <label class=\" label label-info\"> ");
            EndContext();
            BeginContext(572, 7, false);
#line 23 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
                                               Write(index++);

#line default
#line hidden
            EndContext();
            BeginContext(580, 31, true);
            WriteLiteral("</label></td>\r\n            <td>");
            EndContext();
            BeginContext(612, 14, false);
#line 24 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
           Write(item.StudentId);

#line default
#line hidden
            EndContext();
            BeginContext(626, 23, true);
            WriteLiteral("</td>\r\n            <td>");
            EndContext();
            BeginContext(650, 14, false);
#line 25 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
           Write(item.PassScore);

#line default
#line hidden
            EndContext();
            BeginContext(664, 23, true);
            WriteLiteral("</td>\r\n            <td>");
            EndContext();
            BeginContext(688, 13, false);
#line 26 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
           Write(item.ExamTime);

#line default
#line hidden
            EndContext();
            BeginContext(701, 23, true);
            WriteLiteral("</td>\r\n            <td>");
            EndContext();
            BeginContext(725, 15, false);
#line 27 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
           Write(item.TotleScore);

#line default
#line hidden
            EndContext();
            BeginContext(740, 58, true);
            WriteLiteral("</td>\r\n            <td><label class=\" label label-danger\">");
            EndContext();
            BeginContext(800, 10, false);
#line 28 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
                                               Write(item.Score);

#line default
#line hidden
            EndContext();
            BeginContext(811, 33, true);
            WriteLiteral("分</label> </td>\r\n            <td>");
            EndContext();
            BeginContext(845, 12, false);
#line 29 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
           Write(item.AddTime);

#line default
#line hidden
            EndContext();
            BeginContext(857, 47, true);
            WriteLiteral("</td>\r\n            <td class=\"font-weight-600\">");
            EndContext();
            BeginContext(906, 24, false);
#line 30 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
                                    Write(item.IsFinish?"完成":"未完成");

#line default
#line hidden
            EndContext();
            BeginContext(931, 45, true);
            WriteLiteral("</td>\r\n            <td class=\" text-right\">\r\n");
            EndContext();
#line 32 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
                 if (!item.IsFinish)
                {

#line default
#line hidden
            BeginContext(1033, 20, true);
            WriteLiteral("                    ");
            EndContext();
            BeginContext(1053, 125, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1c00089017bd4d36ab0569faba3cdc6b", async() => {
                BeginContext(1170, 4, true);
                WriteLiteral("继续考试");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            BeginWriteTagHelperAttribute();
#line 34 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
                                                                                                                     Write(item.PaperId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("data-item-id", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1178, 88, true);
            WriteLiteral("\r\n                    <button class=\"btn btn-sm btn-primary abandon-exam\" data-item-id=\"");
            EndContext();
            BeginContext(1267, 12, false);
#line 35 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
                                                                                 Write(item.PaperId);

#line default
#line hidden
            EndContext();
            BeginContext(1279, 17, true);
            WriteLiteral("\">放弃考试</button>\r\n");
            EndContext();
#line 36 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(1356, 74, true);
            WriteLiteral("                    <button class=\"btn btn-sm btn-default \" data-item-id=\"");
            EndContext();
            BeginContext(1431, 12, false);
#line 39 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
                                                                     Write(item.PaperId);

#line default
#line hidden
            EndContext();
            BeginContext(1443, 21, true);
            WriteLiteral("\">无可操作</button>    \r\n");
            EndContext();
#line 40 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
                }

#line default
#line hidden
            BeginContext(1483, 52, true);
            WriteLiteral("                \r\n            </td>\r\n        </tr>\r\n");
            EndContext();
#line 44 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Paper.cshtml"
    }

#line default
#line hidden
            BeginContext(1542, 24, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LabExam.Models.Entities.ExaminationPaper>> Html { get; private set; }
    }
}
#pragma warning restore 1591
