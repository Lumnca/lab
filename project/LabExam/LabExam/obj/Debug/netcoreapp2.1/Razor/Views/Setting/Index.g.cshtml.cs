#pragma checksum "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Setting\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "625f85c18bd3506fd43b45706497feeeefe3a4a4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Setting_Index), @"mvc.1.0.view", @"/Views/Setting/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Setting/Index.cshtml", typeof(AspNetCore.Views_Setting_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"625f85c18bd3506fd43b45706497feeeefe3a4a4", @"/Views/Setting/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_Setting_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LabExam.Models.Entities.Module>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/mustache/mustache.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(52, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Setting\Index.cshtml"
  
    ViewData["Title"] = "实验室安全教育在线-出题设置";
    Layout = "~/Views/Shared/_BackEnd_Layout.cshtml";

#line default
#line hidden
            BeginContext(159, 381, true);
            WriteLiteral(@"

<div class="" margin-5px bc-clr-white padding-10px  border-little-grey-all"" data-min-width=""900"" data-height-all>
    <h4 class="" border-light-down font-size-15 font-weight-700 padding-bottom-10px "">
        <span class="" glyphicon glyphicon-cog""></span> 考试出题设置
    </h4>
    <div class="" margin-top-10px"">
        <div class=""btn-group btn-group-justified"" role=""group"">
");
            EndContext();
#line 15 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Setting\Index.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(597, 136, true);
            WriteLiteral("                <div class=\"btn-group\" role=\"group\">\r\n                    <button type=\"button\" class=\"btn btn-default\" data-module-id=\"");
            EndContext();
            BeginContext(734, 13, false);
#line 18 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Setting\Index.cshtml"
                                                                             Write(item.ModuleId);

#line default
#line hidden
            EndContext();
            BeginContext(747, 99, true);
            WriteLiteral("\">\r\n                        <span class=\"glyphicon glyphicon-cog\"></span>\r\n                        ");
            EndContext();
            BeginContext(847, 9, false);
#line 20 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Setting\Index.cshtml"
                   Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(856, 57, true);
            WriteLiteral("\r\n                    </button>\r\n                </div>\r\n");
            EndContext();
#line 23 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Setting\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(928, 5495, true);
            WriteLiteral(@"        </div>
    </div>
    <div id=""module-setting-info"" class="" margin-top-10px padding-10px"">
        <div>
            <span class="" font-weight-400 font-size-14 label label-primary"">当前选择模块：</span>
            <input type=""text"" name=""moduleName""
                   value="""" disabled
                   id=""moduleName"" class="" border-down-only no-focus"" data-width=""200"">
            <span class=""margin-left-60px font-weight-400 font-size-14 label label-primary"">当前模块编号:</span>
            <input type=""text"" name=""moduleId""
                   value="""" disabled
                   id=""moduleId"" class="" border-down-only no-focus"">
        </div>
        <div class="" margin-top-20px"">
            <span class="" glyphicon glyphicon-calendar ""></span>
            <span class="" font-weight-600  text-primary "">最大考试次数:</span>
            <input type=""text"" name=""AllowMaxtimes""
                   value=""""
                   id=""AllowMaxtimes"" class="" text-center border-radius-6 "">
            <label");
            WriteLiteral(@" for="""">次</label>
        </div>
        <div class="" margin-top-20px"">
            <span class=""glyphicon glyphicon-saved ""></span>
            <span class="" font-weight-600  text-primary "">学生考试时间:</span>
            <input type=""text"" name=""ExamTimes""
                   value=""""
                   id=""ExamTimes"" class="" text-center border-radius-6 "">
            <label for="""">分钟</label>
        </div>
        <div class="" margin-top-40px"">
            <h4 class="" border-light-down font-size-14 font-weight-700 padding-bottom-10px "">
                <span class=""glyphicon glyphicon-scissors""></span> 题目设置
                <span class=""totalScore"">0 分</span>
            </h4>
        </div>
        <div class="" margin-top-20px"">
            <span class="" font-weight-700 font-size-14 margin-left-20px  label label-danger"">单选题数量:</span>
            <input type=""number"" name=""singleCount""
                   value=""物理信息模块""
                   id=""singleCount"" class=""margin-left-10px  border-radius-");
            WriteLiteral(@"6 padding-left-10px "" data-width=""240"" data-height=""27"">
            <span class="" font-weight-700 font-size-14 margin-left-40px  label label-danger"">单个题目分数:</span>
            <input type=""number"" name=""singleScore""
                   value=""""
                   id=""singleScore"" class=""margin-left-10px  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
        </div>
        <div class="" margin-top-20px"">
            <span class="" font-weight-700 font-size-14 margin-left-20px  label label-danger"">多选题数量:</span>
            <input type=""number"" name=""MultipleCount""
                   value=""物理信息模块""
                   id=""MultipleCount"" class=""margin-left-10px  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
            <span class="" font-weight-700 font-size-14 margin-left-40px  label label-danger"">单个题目分数:</span>
            <input type=""number"" name=""MultipleScore""
                   value=""""
                   id=""MultipleScore"" class=""margin-left-10p");
            WriteLiteral(@"x  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
        </div>
        <div class="" margin-top-20px"">
            <span class="" font-weight-700 font-size-14 margin-left-20px  label label-danger"">判断题数量:</span>
            <input type=""number"" name=""JudgeCount""
                   value=""物理信息模块""
                   id=""JudgeCount"" class=""margin-left-10px  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
            <span class="" font-weight-700 font-size-14 margin-left-40px label label-danger "">单个题目分数:</span>
            <input type=""number"" name=""JudgeScore""
                   value=""""
                   id=""JudgeScore"" class=""margin-left-10px  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
        </div>
        <div class="" margin-top-20px"">
            <span class="" glyphicon glyphicon-signal""></span>
            <span class="" font-weight-600  text-primary "">考试通过分数:</span>
            <input type=""number"" name=""AllowPassSco");
            WriteLiteral(@"re""
                   value=""""
                   id=""AllowPassScore"" class="" text-center border-down-only no-focus"">
            <label for="""">分</label>
        </div>
        <div class="" margin-top-40px"">
            <h4 class="" border-light-down font-size-14 font-weight-700 padding-bottom-10px "">
                <span class=""glyphicon glyphicon-tags""></span> 题目设置保存按钮
            </h4>
        </div>
        <div class="" margin-top-20px"">
            <button class="" btn btn-sm btn-primary margin-left-10px"" data-moduleId=""0"">
                <span class="" glyphicon glyphicon-fire ""></span>
                立即保存设置
            </button>
        </div>
    </div>
</div>
<div class=""modal fade"" id=""program"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"" data-backdrop=""static"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-labe");
            WriteLiteral(@"l=""Close""><span aria-hidden=""true"">&times;</span></button>
                <h4 class=""modal-title font-weight-400 font-size-14 padding-top-10px text-info"" >拼命版本数据中</h4>
            </div>
            <div class=""modal-body"">
                <span class="" glyphicon glyphicon-cloud-upload ""></span> 拼命搬运数据中........
            </div>
        </div>
    </div>
</div>
");
            EndContext();
            BeginContext(6423, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3d674b9fad794661ada06f375a9a8099", async() => {
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
            BeginContext(6477, 4849, true);
            WriteLiteral(@"
<script id=""template"" type=""x-tmpl-mustache"">
    <div class="" margin-top-10px padding-10px"">
        <div>
            <span class="" font-weight-400 font-size-14 label label-primary"">当前选择模块：</span>
            <input type=""text"" name=""moduleName"" disabled
                   value=""{{moduleName}}""
                   id=""moduleName"" class="" border-down-only no-focus"" data-width=""200"">
            <span class=""margin-left-60px font-weight-400 font-size-14 label label-primary"">当前模块编号:</span>
            <input type=""text"" name=""moduleId""
                   value=""{{moduleId}}"" disabled
                   id=""moduleId"" class="" border-down-only no-focus"">
        </div>
        <div class="" margin-top-20px"">
            <span class="" glyphicon glyphicon-calendar ""></span>
            <span class="" font-weight-600  text-primary "">最大考试次数:</span>
            <input type=""text"" name=""AllowMaxtimes""
                   value=""{{allowExamTime}}""
                   id=""AllowMaxtimes"" class="" text-center");
            WriteLiteral(@" border-radius-6 "">
            <label for="""">次</label>
        </div>
        <div class="" margin-top-20px"">
            <span class=""glyphicon glyphicon-saved ""></span>
            <span class="" font-weight-600  text-primary "">学生考试时间:</span>
            <input type=""text"" name=""ExamTimes""
                   value=""{{examTime}}""
                   id=""ExamTimes"" class="" text-center border-radius-6 "">
            <label for="""">分钟</label>
        </div>
        <div class="" margin-top-40px"">
            <h4 class="" border-light-down font-size-14 font-weight-700 padding-bottom-10px "">
                <span class=""glyphicon glyphicon-scissors""></span> 题目设置
                <span class=""totalScore"">当前总分：{{totalScore}}分</span>
            </h4>
        </div>
        <div class="" margin-top-20px"">
            <span class="" font-weight-700 font-size-14 margin-left-20px"">单选题数量:</span>
            <input type=""number"" name=""singleCount""
                   value=""{{single.count}}""
                 ");
            WriteLiteral(@"  id=""singleCount"" class=""margin-left-10px  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
            <span class="" font-weight-700 font-size-14 margin-left-40px"">单个题目分数:</span>
            <input type=""number"" name=""singleScore""
                   value=""{{single.score}}""
                   id=""singleScore"" class=""margin-left-10px  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
        </div>
        <div class="" margin-top-20px"">
            <span class="" font-weight-700 font-size-14 margin-left-20px"">多选题数量:</span>
            <input type=""number"" name=""MultipleCount""
                   value=""{{multiple.count}}""
                   id=""MultipleCount"" class=""margin-left-10px  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
            <span class="" font-weight-700 font-size-14 margin-left-40px"">单个题目分数:</span>
            <input type=""number"" name=""MultipleScore""
                   value=""{{multiple.score}}""
                ");
            WriteLiteral(@"   id=""MultipleScore"" class=""margin-left-10px  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
        </div>
        <div class="" margin-top-20px"">
            <span class="" font-weight-700 font-size-14 margin-left-20px"">判断题数量:</span>
            <input type=""number"" name=""JudgeCount""
                   value=""{{judge.count}}""
                   id=""JudgeCount"" class=""margin-left-10px  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
            <span class="" font-weight-700 font-size-14 margin-left-40px"">单个题目分数:</span>
            <input type=""number"" name=""JudgeScore""
                   value=""{{judge.score}}""
                   id=""JudgeScore"" class=""margin-left-10px  border-radius-6 padding-left-10px "" data-width=""240"" data-height=""27"">
        </div>
        <div class="" margin-top-20px"">
            <span class="" glyphicon glyphicon-signal""></span>
            <span class="" font-weight-600  text-primary "">考试通过分数:</span>
            <input type");
            WriteLiteral(@"=""number"" name=""AllowPassScore""
                   value=""{{passFloat}}""
                   id=""AllowPassScore"" class="" text-center border-down-only no-focus"">
            <label for="""">分</label>
        </div>
        <div class="" margin-top-40px"">
            <h4 class="" border-light-down font-size-14 font-weight-700 padding-bottom-10px "">
                <span class=""glyphicon glyphicon-tags""></span> 题目设置保存按钮
            </h4>
        </div>
        <div class="" margin-top-20px"">
            <button class="" btn btn-sm btn-primary margin-left-10px"" data-moduleId=""{{moduleId}}"" >
                <span class="" glyphicon glyphicon-fire "" ></span>
                立即保存设置
            </button>
        </div>
    </div>
</script>
");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(11345, 6623, true);
                WriteLiteral(@"
    <script>

        function Strategy() {
            this.allowExamTime = 3;
            this.passFloat = 60.0;
            this.examTime = 100.0;
            this.moduleName = """";
            this.moduleId = 1;
            this.totalScore = 100.0;
            this.single = {};
            this.multiple = {};
            this.judge = {};
        }

        $('.btn-group-justified').on('click',""button"",null,function(jqEvent) {
                $('.btn-group-justified .btn-primary').removeClass(""btn-primary"");
                $(this).addClass('btn-primary');

                var id = $(this).attr(""data-module-id"").trim();

                $('#program').modal('show');

                $.ajax({
                    url: ""Setting/LoadSetting"",
                    type: ""post"",
                    dataType: ""json"",
                    data: {
                        moduleId: id
                    },
                    success: function (json, textStatus, jqXhr) {
                ");
                WriteLiteral(@"        console.log(json);
                        if (json.isOk) {

                            var template = $('#template').html();
                            Mustache.parse(template);
                            var result = Mustache.render(template, json.moduleSetting);
                            $('#module-setting-info').html(result);
                            $('#program').modal('hide');
                        } else {
                            $('#program').modal('hide');
                            onMask(json.title, json.message);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        onMask(""错误"", errorThrown);
                    }
                });
            });

        $('#module-setting-info').on(""keypress keyup"",""input"",null,function(jqevent) {
                total();
        });

        function total() {
            var judgeCount = parseInt($('#JudgeCount').val() == ");
                WriteLiteral(@"""""? 0:$('#JudgeCount').val() );
            var judgeScore = parseInt($('#JudgeScore').val() == """"? 0:$('#JudgeScore').val() );
            var multipleCount = parseInt($('#MultipleCount').val() == """"? 0:$('#MultipleCount').val());
            var multipleScore = parseInt($('#MultipleScore').val() == """"? 0:$('#MultipleScore').val());
            var singleCount = parseInt($('#singleCount').val()==""""?0:$('#singleCount').val());
            var singleScore = parseInt($('#singleScore').val()==""""?0:$('#singleScore').val());
        
            var totalScore = judgeCount * judgeScore + multipleScore * multipleCount + singleCount * singleScore;
            $('.totalScore').text(`当前总分：${totalScore}分`);
            return totalScore;
        };

        $('#module-setting-info').on('click',""button"",null,
            function (jqEvent) {
                if ($('.btn-group-justified .btn-primary').length == 0) {
                    onMask(""提示"", ""请选择模块！""); return;
                }
                if (");
                WriteLiteral(@"$('#AllowMaxtimes').val() == """")
                {
                    onMask(""提示"", ""请填写最大考试次数"");return;
                }
                if ($('#ExamTimes').val() == """") {
                    onMask(""提示"", ""请填写考试时间""); return;
                }
                if ($('#singleCount').val() == """") {
                    onMask(""提示"", ""请填写单选题数量""); return;
                }
                if ($('#singleScore').val()  == """") {
                    onMask(""提示"", ""请填写单选题当个题目分数""); return;
                }
                if ($('#JudgeCount').val()  == """") {
                    onMask(""提示"", ""请填写判断题目数量""); return;
                }
                if ($('#JudgeScore').val() == """") {
                    onMask(""提示"", ""请填写判断题单个题目分数""); return; 
                }
                if ($('#MultipleScore').val() == """") {
                    onMask(""提示"", ""请填写多选题数量""); return;
                }
                if ($('#MultipleCount').val() == """") {
                    onMask(""提示"", ""请填写多选题单个题目分数""); return;
     ");
                WriteLiteral(@"           }
                if ($('#AllowPassScore').val() == """") {
                    onMask(""提示"", ""请填写考试通过分数""); return;
                }

                var  time = $('#AllowMaxtimes').val();
                var  examcount = $('#ExamTimes').val();
                var  pass = $('#AllowPassScore').val();
                
                var judgeCount = parseInt($('#JudgeCount').val() == """"? 0:$('#JudgeCount').val() );
                var judgeScore = parseInt($('#JudgeScore').val() == """"? 0:$('#JudgeScore').val() );
                var multipleCount = parseInt($('#MultipleCount').val() == """"? 0:$('#MultipleCount').val());
                var multipleScore = parseInt($('#MultipleScore').val() == """"? 0:$('#MultipleScore').val());
                var singleCount = parseInt($('#singleCount').val()==""""?0:$('#singleCount').val());
                var singleScore = parseInt($('#singleScore').val() == """" ? 0 : $('#singleScore').val());

                var data = new Strategy();
                ");
                WriteLiteral(@"data.allowExamTime = time;
                data.examTime = examcount;
                data.totalScore = total();
                data.moduleId = $(this).attr('data-moduleId');
                data.moduleName = $('#moduleName').val();
                data.passFloat = pass;
                data.judge.count = judgeCount;
                data.judge.score = judgeScore;
                data.multiple.count = multipleCount;
                data.multiple.score = multipleScore;
                data.single.count = singleCount;
                data.single.score = singleScore;

                console.log(data);

                $.ajax({
                    url: ""Setting/Module"",
                    type: ""post"",
                    dataType: ""json"",
                    data: {
                        moduleId: data.moduleId,
                        data:JSON.stringify(data)
                    },
                    success: function (json, textStatus, jqXhr) {
                        if (json.isO");
                WriteLiteral(@"k) {
                            onMask(json.title, json.message);
                        } else {
                            $('#program').modal('hide');
                            onMask(json.title, json.message);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        onMask(""错误"", errorThrown);
                    }
                });

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
