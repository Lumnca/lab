#pragma checksum "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0415b5ef8199324429641e779d8dcd2d0d66f254"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_ReApplications), @"mvc.1.0.view", @"/Views/Student/ReApplications.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Student/ReApplications.cshtml", typeof(AspNetCore.Views_Student_ReApplications))]
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
#line 1 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
using LabExam.Models.Map;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0415b5ef8199324429641e779d8dcd2d0d66f254", @"/Views/Student/ReApplications.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_Student_ReApplications : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LabExam.Models.Entities.ApplicationForReExamination>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(100, 409, true);
            WriteLiteral(@"
    <table class=""table table-hover"" id=""student-list"">
        <thead>
            <tr>
                <th>编号</th>
                <th>申请学号</th>
                <th>姓名</th>
                <th>性别</th>
                <th>申请时间</th>
                <th>通知邮件</th>
                <th>申请状态</th>
                <th class="" text-right "">操作</th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 18 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
          
            int index = 1;
        

#line default
#line hidden
            BeginContext(560, 8, true);
            WriteLiteral("        ");
            EndContext();
#line 21 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(609, 72, true);
            WriteLiteral("            <tr>\r\n                <td><label class=\" label label-info\"> ");
            EndContext();
            BeginContext(683, 7, false);
#line 24 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
                                                  Write(index++);

#line default
#line hidden
            EndContext();
            BeginContext(691, 35, true);
            WriteLiteral("</label></td>\r\n                <td>");
            EndContext();
            BeginContext(727, 14, false);
#line 25 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
               Write(item.StudentId);

#line default
#line hidden
            EndContext();
            BeginContext(741, 49, true);
            WriteLiteral("</td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(791, 17, false);
#line 27 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
               Write(item.Student.Name);

#line default
#line hidden
            EndContext();
            BeginContext(808, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(877, 24, false);
#line 30 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
                Write(item.Student.Sex?"男":"女");

#line default
#line hidden
            EndContext();
            BeginContext(902, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(970, 12, false);
#line 33 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
               Write(item.AddTime);

#line default
#line hidden
            EndContext();
            BeginContext(982, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1050, 10, false);
#line 36 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
               Write(item.Email);

#line default
#line hidden
            EndContext();
            BeginContext(1060, 100, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    <label class=\" label label-info\">");
            EndContext();
            BeginContext(1162, 122, false);
#line 39 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
                                                 Write(item.ApplicationStatus == ApplicationStatus.Submit ?"等待审核中":(item.ApplicationStatus == ApplicationStatus.Pass?"已通过":"未通过"));

#line default
#line hidden
            EndContext();
            BeginContext(1285, 163, true);
            WriteLiteral("</label>\r\n                </td>\r\n                <td class=\" text-right \">\r\n                    <button class=\"btn btn-sm btn-default detail-button\" data-item-id=\"");
            EndContext();
            BeginContext(1449, 22, false);
#line 42 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
                                                                                  Write(item.ApplicationExamId);

#line default
#line hidden
            EndContext();
            BeginContext(1471, 142, true);
            WriteLiteral("\" >\r\n                        <span class=\"glyphicon glyphicon-sunglasses\"></span>\r\n                        详情\r\n                    </button>\r\n");
            EndContext();
#line 46 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
                     if (item.ApplicationStatus == ApplicationStatus.Submit)
                    {

#line default
#line hidden
            BeginContext(1714, 91, true);
            WriteLiteral("                        <button class=\"btn btn-sm btn-default delete-button\" data-item-id=\"");
            EndContext();
            BeginContext(1806, 22, false);
#line 48 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
                                                                                      Write(item.ApplicationExamId);

#line default
#line hidden
            EndContext();
            BeginContext(1828, 150, true);
            WriteLiteral("\">\r\n                            <span class=\"glyphicon glyphicon-trash\"></span>\r\n                            删除申请\r\n                        </button>\r\n");
            EndContext();
#line 52 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
                    }

#line default
#line hidden
            BeginContext(2001, 42, true);
            WriteLiteral("                </td>\r\n            </tr>\r\n");
            EndContext();
#line 55 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\ReApplications.cshtml"
        }

#line default
#line hidden
            BeginContext(2054, 32, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LabExam.Models.Entities.ApplicationForReExamination>> Html { get; private set; }
    }
}
#pragma warning restore 1591
