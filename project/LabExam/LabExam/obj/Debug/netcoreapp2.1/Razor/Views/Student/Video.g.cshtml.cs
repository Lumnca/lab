#pragma checksum "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Video.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2ffeee412aa02ee3f10b1f38e908208b12a7ee31"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_Video), @"mvc.1.0.view", @"/Views/Student/Video.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Student/Video.cshtml", typeof(AspNetCore.Views_Student_Video))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ffeee412aa02ee3f10b1f38e908208b12a7ee31", @"/Views/Student/Video.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_Student_Video : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LabExam.Models.Entities.Progress>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Student\Video.cshtml"
  
    ViewData["Title"] = "实验室安全教育在线-视频学习";
    Layout = "~/Views/Shared/_Student.cshtml";

#line default
#line hidden
            BeginContext(152, 5482, true);
            WriteLiteral(@"
<div class="" container margin-top-15px"">
    <div class=""row  bc-clr-white border-thumali"">
        <div id=""col-md-9-ms"" class=""  padding-15  col-md-9 "">
            <div id=""vedio-open-layout"" data-vedio-open-style=""html5"" muted preload=""metadata"">
                <video id=""my-video"" class=""video-js"" controls preload=""auto"" width=""100%""
                       data-setup=""{}"">
                    <source src=""../Resources/VedioList/1.mp4"" type=""video/mp4"">
                    <source src=""../Resources/VedioList/2015419433498.mp4"" type=""video/mp4"">
                    <p class=""vjs-no-js"">
                        不支持
                    </p>
                </video>
                <!-- html 播放器
                <video controls autoplay=""false"" width=""100%"" class="" img-thumbnail"" >
                    <source src=""../Resources/VedioList/2015430159923.mp4"" type=""video/mp4"">
                    <source src=""../Resources/VedioList/2015419433498.mp4"" type=""video/mp4"">
                    <p>
  ");
            WriteLiteral(@"                      人世间最痛苦的事情
                    </p>
                </video>
                -->
            </div>
            <div>

            </div>
            <div class=""vedio-control float-layout"">
                <span class="" glyphicon glyphicon-play""></span>
                <label class="" padding-left-35px""><input type=""checkbox"" name=""isAutoOpen"" id=""isAutoOpen""> 自动播放下一个视频</label>
                <a href=""#"" class="" padding-left-15px""><span class="" glyphicon glyphicon-fire""></span> Flash播放器</a>
                <a href=""#"" class="" padding-left-15px""><span class="" glyphicon glyphicon-header""></span> Html播放</a>
                <button class=""btn btn-default margin-left-15px float-right""> <span class="" glyphicon glyphicon-ok""></span> 看过啦</button>
            </div>
            <div class="" margin-top-15px"">
                <div class=""alert alert-danger alert-dismissible"" role=""alert"">
                    <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close");
            WriteLiteral(@"""><span aria-hidden=""true"">&times;</span></button>
                    <strong>Warning!</strong> 如果无法播放请切换播放器 或者联系技术人员
                </div>
            </div>
            <div class="" vedio-open-need-waiting-list margin-top-25px"">
                <h4 class=""title-Splitter text-info"">
                    <span class=""glyphicon glyphicon-expand"">视频列表
                </h4>
                <div class=""float-layout margin-bottom-15px"">
                    <a href=""#"" class=""color-grey"">
                        <span class=""glyphicon glyphicon-play-circle""></span> 第一节
                        化学元素和实验器具的基本使用
                    </a>
                    <span>
                        规定时间:35 分钟 已完成
                    </span>

                    <a href=""#"" class="" btn btn-info btn-sm float-right"">播放 </a>
                </div>
                <div class=""float-layout margin-bottom-15px"">
                    <a href=""#"" class="" color-red"">
                        <span class=""glyphicon glyphico");
            WriteLiteral(@"n-play-circle""></span> 第二节
                        化学元素和实验器具的基本使用
                    </a>
                    <span>
                        规定时间:35 分钟 正在进行
                    </span>
                    <a href=""#"" class="" btn btn-info btn-sm float-right"">播放 </a>
                </div>
                <div class=""float-layout margin-bottom-15px"">
                    <a href=""#"">
                        <span class=""glyphicon glyphicon-play-circle""></span> 第三节
                        化学元素和实验器具的基本使用
                    </a>
                    <span>
                        规定时间:35 分钟 尚未观看
                    </span>
                    <a href=""#"" class="" btn btn-info btn-sm float-right"">播放 </a>
                </div>
            </div>
        </div>
        <div class="" col-md-3 padding-15"">
            <div class="" row"">
                <div class="" col-md-5"">
                    <img src=""../Resources/PagesStudent/Icon/teacherIcon.png"" width=""100px"" class="" img-circle img-thumbnai");
            WriteLiteral(@"l "" alt=""老师图片"" />
                </div>
                <div class="" col-md-7"">
                    <div>
                        <p class="" font-weight-600 margin-top-20px font-size-16"">主讲人：高育梁</p>
                        <p class="" font-weight-500 margin-top-20px font-size-14 text-info "">职称：副教授</p>
                    </div>
                </div>
            </div>
            <div>
                <div class="" margin-top-25px"">
                    <div class="" title-Splitter"">
                        <span class="" font-size-16 font-weight-500 text-danger"">课程简介</span>
                    </div>
                    <div class=""text-indent-15px  padding-top-15px"">
                        到目前为止，我们已经在本课程中查看了很多文本，但是网页只会使用文本而感到无聊。
                        让我们开始研究如何使网络变得活跃，内容更加有趣！本单元探讨了如何使用HTML在您
                        的网页中包含多媒体，包括可以包含图像的不同方式，以及如何嵌入视频，音频甚至整个网页。
                    </div>
                </div>
            </div>
            <div>
                <p class="" font-weight-300 m");
            WriteLiteral(@"argin-top-20px font-size-14"">课程学分: 2分</p>
                <p class="" font-weight-300 margin-top-10px font-size-14"">是否必修: 必修</p>
                <p class="" font-weight-300 margin-top-20px font-size-14"">
                    <span class="" glyphicon glyphicon-play-circle""></span> 1345
                </p>
            </div>
        </div>
    </div>
</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LabExam.Models.Entities.Progress>> Html { get; private set; }
    }
}
#pragma warning restore 1591
