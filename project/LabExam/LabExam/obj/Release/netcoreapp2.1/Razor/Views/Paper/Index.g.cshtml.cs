#pragma checksum "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Paper\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5f52410fc5cb4300eed0bfbe9c15296a8fc6afef"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Paper_Index), @"mvc.1.0.view", @"/Views/Paper/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Paper/Index.cshtml", typeof(AspNetCore.Views_Paper_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f52410fc5cb4300eed0bfbe9c15296a8fc6afef", @"/Views/Paper/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_Paper_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "-1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/mustache/mustache.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Paper\Index.cshtml"
  
    ViewData["Title"] = "实验室安全教育在线-考试试卷";
    Layout = "~/Views/Shared/_BackEnd_Layout.cshtml";

#line default
#line hidden
            BeginContext(105, 424, true);
            WriteLiteral(@"

<div class=""admin-searach"">
    <div id=""Search-condition"" class="" bc-clr-white margin-5px  padding-15px border-little-grey-all""
         data-min-width=""1250"">
        <div class=""float-layout"">
            <span for=""isFinish"" class=""font-size-14 text-muted"">是否完成：</span>
            <select id=""isFinish"" name=""isFinish"" data-height=""26"" data-width=""180"" class=""font-size-12 padding-left-5px "">
                ");
            EndContext();
            BeginContext(529, 36, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d994f1710b694ec587c0d7bf0ce98ab5", async() => {
                BeginContext(548, 8, true);
                WriteLiteral("-- 全部 --");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(565, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(583, 36, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c17ff16d34fa49e986c5c163539b267a", async() => {
                BeginContext(601, 9, true);
                WriteLiteral("-- 未完成 --");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(619, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(637, 36, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bf47cb8ccf554a31881dd57ed0272717", async() => {
                BeginContext(655, 9, true);
                WriteLiteral("-- 已完成 --");
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
            BeginContext(673, 6810, true);
            WriteLiteral(@"
            </select>
            <span for=""studentId"" class=""margin-left-10px font-size-14 text-muted"">学生学号：</span>
            <input value="""" type=""text"" id=""studentId"" name=""studentId"" data-height=""22"" data-width=""120"" class=""font-size-12 padding-left-5px "">
            <span for=""PaperId"" class=""margin-left-20px font-size-14 text-muted"">试卷编号：</span>
            <input value="""" type=""text"" id=""PaperId"" name=""PaperId"" data-height=""22""
                   data-width=""100"" class=""font-size-12 padding-left-5px "">
            <span for=""scoreLeft"" class=""margin-left-10px font-size-14 text-muted"">分数范围：</span>
            <input type=""number"" min=""0"" id=""scoreLeft"" name=""scoreLeft"" data-width=""75"" />
            <span for=""scoreRight"" class=""font-size-14 text-muted""> ~ </span>
            <input type=""number"" min=""0"" id=""scoreRight"" name=""scoreRight"" data-width=""70"" />
            <button id=""searchInstitute"" class=""float-right  btn btn-sm btn-primary"">
                <span class=""glyphicon glyphic");
            WriteLiteral(@"on-search""></span>
                立即搜索
            </button>
        </div>
    </div>

</div>
<div class="" bc-clr-white margin-5px  padding-15px border-little-grey-all"" data-min-width=""1250"">
    <table class=""table table-hover"" id=""student-list"">
        <thead>
            <tr>
                <th>编号</th>
                <th>学号</th>
                <th>试卷编号</th>
                <th>通过分数</th>
                <th>考试时间</th>
                <th>剩余考试时间</th>
                <th>试卷总分</th>
                <th>参考时间</th>
                <th>分数</th>
                <th>是否完成</th>
                <th class=""text-right"">操作</th>
            </tr>
        </thead>
        <tbody class=""section-items""></tbody>
    </table>
    <div class="" float-layout "">
        <label class="" float-left"">
            共 <span class=""items-count"">0</span> 张试卷
        </label>
        <div class="" float-right"">
            <a href=""#"" class="" btn-default btn btn-sm "">
                <span>第</span>
        ");
            WriteLiteral(@"        <span class=""show-page-Index"">
                    1
                </span>
                <span>
                    /
                </span>
                <span class=""show-page-Count"">
                    12
                </span>
                <span>
                    页
                </span>
            </a>
            <button class=""First btn btn-primary btn-sm"">
                <span class=""glyphicon glyphicon-backward""></span>
                首页
            </button>
            <button class=""Previous btn btn-primary btn-sm"">
                <span class=""glyphicon glyphicon-chevron-left""></span>
                上一页
            </button>
            <button class=""Next btn btn-primary btn-sm"">
                下一页 <span class=""glyphicon glyphicon-chevron-right""></span>
            </button>
            <button class=""Last btn btn-primary btn-sm"" data-lastIndex=""1"">
                尾页 <span class=""glyphicon glyphicon-forward""></span>
            </button>
 ");
            WriteLiteral(@"           <select data-options=""true"" class="" margin-left-10px text-center"" data-height=""27"" data-width=""45""></select>
            <button data-skip=""true"" class=""btn btn-sm btn-primary"">跳转</button>
        </div>
    </div>
</div>
<script id=""item-template"" type=""x-tmpl-mustache"">
    {{#items}}
    <tr>
        <td>
            <label class=""label label-primary"">{{index}}</label>
        </td>
        <td>
            {{studentId}}
        </td>
        <td>
            {{paperId}}
        </td>
        <td>
            {{passScore}}
        </td>
        <td>
            {{examTime}}分钟
        </td>
        <td>
            {{leaveExamTime}}分钟
        </td>
        <td>
            {{totleScore}}
        </td>
        <td>
            {{addTime}}
        </td>
        <td>
            <label class=""label label-danger"">{{score}}分</label>
        </td>
        <td>
            {{isFinish}}
        </td>
        <td class=""text-right"">
            <button class=""btn btn");
            WriteLiteral(@"-sm btn-primary detail-button"" data-item-id=""{{paperId}}""><span class=""glyphicon glyphicon-zoom-in""></span>详情</button>
        </td>
    </tr>
    {{/items}}
</script>
<script id=""detail-template"" type=""x-tmpl-mustache"">
    <table class="" table table-hover"">
        <thead>
            <tr>
                <th>
                    题目类型
                </th>
                <th>
                    题目数量
                </th>
                <th>
                    正确数量
                </th>
                <th>
                    每题分数
                </th>
                <th>
                    总得分
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>单选题</td>
                <td>{{judge.allCount}}</td>
                <td>{{judge.rightCount}}</td>
                <td>{{judge.score}}</td>
                <td>{{judge.totalScore}} 分</td>
            </tr>
            <tr>
                <td>多选题</td>
                ");
            WriteLiteral(@"<td>{{multiple.allCount}}</td>
                <td>{{multiple.rightCount}}</td>
                <td>{{multiple.score}}</td>
                <td>{{multiple.totalScore}} 分</td>
            </tr>
            <tr>
                <td>判断题</td>
                <td>{{single.allCount}}</td>
                <td>{{single.rightCount}}</td>
                <td>{{single.score}}</td>
                <td>{{single.totalScore}} 分</td>
            </tr>
            <tr>
                <td>简答题</td>
                <td>1</td>
                <td>无正确判断</td>
                <td>0</td>
                <td>0 分</td>
            </tr>

        </tbody>
    </table>
    <div class="" margin-top-10px padding-10px"">
        <div class=""font-weight-600"">
            <span class="" glyphicon glyphicon-ok-sign ""></span> 学生评论:
            {{review}}
        </div>
    </div>
</script>
<div class=""modal fade"" id=""Detail"" tabindex=""-1"" role=""dialog"">
    <div class=""modal-dialog"" role=""document"">
        <div clas");
            WriteLiteral(@"s=""modal-content"">
            <div class=""modal-header"">
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>
                <h4 class=""modal-title font-weight-600 padding-top-10px text-primary"">
                    <span class="" glyphicon glyphicon-cog ""></span>
                    试卷详情
                </h4>
            </div>
            <div class=""modal-body""></div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">立即关闭</button>
            </div>
        </div>
    </div>
</div>
");
            EndContext();
            BeginContext(7483, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "818dac81a92941d79f246d5ca6a41c27", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(7537, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(7558, 5457, true);
                WriteLiteral(@"
    <script>
        function stateManager() {
            var pageIndex = parseInt($('.show-page-Index').text().trim()); //当前页
            var pageCount = parseInt($('.show-page-Count').text().trim()); //总共多少页
            if (pageIndex >= pageCount) {
                $('.Next').prop(""disabled"", true);
            } else {
                $('.Next').prop(""disabled"", false);
            }
            if (pageIndex == 1) {
                $('.Previous').prop(""disabled"", true);
            } else {
                $('.Previous').prop(""disabled"", false);
            }
        }

        loadPageByIndex(1);
        function loadPageByIndex(index) {
            $.ajax({
                url: ""/Paper/Page"",
                type: ""post"",
                dataType: ""json"",
                data: {
                    index: index,
                    isFinish: $('select[name=""isFinish""]').val(),
                    studentId: $('#studentId').val(),
                    PaperId: $('#PaperId').va");
                WriteLiteral(@"l() == """" ? -1 : $('#PaperId').val(),
                    leftScore: $('#scoreLeft').val() == """" ? 0 : $('#scoreLeft').val(),
                    rightScore: $('#scoreRight').val() == """" ? 5112116 : $('#scoreRight').val()
                },
                success: function (json, textStatus, jqXhr) {
                    console.log(json);
                    if (json.isOk) {
                        if (json.items == null) {
                            $('.section-items').html("""");
                        }
                        else {
                            var inCre = json.size * (index - 1);
                            for (var i = 0; i < json.items.length; i++) {
                                json.items[i].index = (i + 1 + inCre);
                            }

                            var template = $('#item-template').html();
                            Mustache.parse(template);
                            var result = Mustache.render(template, json);
                     ");
                WriteLiteral(@"       $('.section-items').html(result);
                        }


                        $('.items-count').text(json.lineCount);
                        $('.show-page-Count').text(`${json.pageCount}`); //分页总数
                        $('.show-page-Index').text(`${json.pageNowIndex}`); //当前页
                        $('button[data-lastIndex]').attr(""data-lastIndex"", json.pageCount); //最后一页 的index


                        $('select[data-options] > option').remove();

                        for (let index_ = 0; index_ < json.pageCount; index_++) {
                            $('select[data-options]').append(`<option value=""${index_ + 1}"">${index_ + 1}</option>`);
                        }

                        $('select[data-options]').val(index);

                        stateManager();
                    } else {
                        onMask(""错误"", json.message);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
        ");
                WriteLiteral(@"            onMask(""错误"", ""网络接连错误.."");
                }
            });
        }

        $('.Next').click(function (jqEvent) {
            loadPageByIndex(parseInt($('.show-page-Index').text().trim()) + 1);
        });

        $('.First').click(function (jqEvent) {
            loadPageByIndex(1);
        });

        $('.Previous').click(function (jqEvent) {
            loadPageByIndex(parseInt($('.show-page-Index').text().trim()) - 1);
        });

        $('.Last').click(function (jqEvent) {
            loadPageByIndex($('button[data-lastIndex]').attr(""data-lastIndex"").trim());
        });

        $('button[data-skip]').click(function (jqEvent) {
            var pageIndex = parseInt($('.show-page-Index').text().trim());
            var skip = parseInt($('select[data-options]').val().trim());
            if (skip === pageIndex) {
                onMask(""提示信息"", ""跳转页面为当前页面"");
            } else {
                loadPageByIndex($('select[data-options]').val());
            }
 ");
                WriteLiteral(@"       });

        $('#searchInstitute').click(function (jqEvent) {
            loadPageByIndex(1);
        });

        $('.section-items').on('click',
            "".detail-button"",
            null,
            function (jqEvent) {

                var id = $(this).attr(""data-item-id"");
                $.ajax({
                    url: ""/Paper/Detail"",
                    type: ""post"",
                    dataType: ""json"",
                    data: {
                        pId: id
                    },
                    success: function (json) {
                        console.log(json);
                        if (json.isOk) {
                            var template = $('#detail-template').html();
                            Mustache.parse(template);
                            var result = Mustache.render(template, json);
                            $('#Detail .modal-body').html(result);

                            $('#Detail').modal('show');

                        }");
                WriteLiteral(@" else {
                            onMask(json.title, json.message);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        onMask(""错误"", `网络接连错误.. ${errorThrown}`);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
