#pragma checksum "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\LogL\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "261808a2c86f5386d3f5c9577685691917d6ea34"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LogL_Index), @"mvc.1.0.view", @"/Views/LogL/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/LogL/Index.cshtml", typeof(AspNetCore.Views_LogL_Index))]
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
#line 1 "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\LogL\Index.cshtml"
using LabExam.Models.Map;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"261808a2c86f5386d3f5c9577685691917d6ea34", @"/Views/LogL/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_LogL_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LabExam.Models.Entities.Principal>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/laydate.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/laydate.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "-1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "F:\四川师范大学实验室安全考试项目\yun\lab\project\LabExam\LabExam\Views\LogL\Index.cshtml"
  
    ViewData["Title"] = "实验室安全教育在线-登录日志";
    Layout = "~/Views/Shared/_BackEnd_Layout.cshtml";

#line default
#line hidden
            BeginContext(187, 50, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "301297de121b475293d96f88cee7e693", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(237, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(239, 39, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f50b10d672de43da858110927c2b41d0", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(278, 482, true);
            WriteLiteral(@"
<div class=""admin-searach"">
    <!-- 搜索栏 -->
    <div id=""Search-condition"" class="" bc-clr-white margin-5px  padding-15px border-little-grey-all""
         data-min-width=""1250"">
        <div class=""float-layout"">
            <!-- 模块 -->
            <span for=""Terminal"" class=""margin-left-10px font-size-14 text-muted"">终端类型:</span>
            <select name=""Terminal"" id=""Terminal"" data-height=""27"" data-width=""110"" class=""font-size-13 padding-left-5px "">
                ");
            EndContext();
            BeginContext(760, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9bd1ecbb4574483aa5c71b95b21401d", async() => {
                BeginContext(779, 10, true);
                WriteLiteral("--  全部  --");
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
            BeginContext(798, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(816, 29, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "177574035b5a4a9ca2e881efb57a8815", async() => {
                BeginContext(834, 2, true);
                WriteLiteral("手机");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(845, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(863, 29, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "35fd7263dd4049debe9f6a22bd4f76c9", async() => {
                BeginContext(881, 2, true);
                WriteLiteral("平板");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(892, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(910, 29, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f0c10a42d8724b409770b04505282a07", async() => {
                BeginContext(928, 2, true);
                WriteLiteral("电脑");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(939, 3484, true);
            WriteLiteral(@"
            </select>
            <span for=""TimeLeft"" class=""margin-left-10px font-size-14 text-muted"">时间范围:</span>
            <input type=""text"" id=""TimeLeft"" name=""TimeLeft"" data-height=""25"" data-width=""150"" class=""font-size-12 padding-left-5px "">
            <span for=""TimeRight"" class=""font-size-14 text-muted"">~</span>
            <input type=""text""  id=""TimeRight"" name=""TimeRight"" data-height=""25"" data-width=""150"" class=""font-size-12 padding-left-5px "">
            <span for=""KeyName"" class=""margin-left-10px font-size-14 text-muted"">登录账号:</span>
            <input type=""text"" placeholder=""账号"" id=""KeyName"" name=""KeyName"" data-height=""25"" data-width=""150"" class=""font-size-12 padding-left-5px "">

            <button class=""float-right  btn btn-sm btn-primary"" id=""search-items"">
                <span class=""glyphicon glyphicon-search""></span>
                立即搜索
            </button>
        </div>
    </div>
</div>
<div class="" bc-clr-white margin-5px  padding-15px border-little-grey-all");
            WriteLiteral(@""" data-min-width=""1250"">
    <table class=""table table-hover"" id=""student-list"">
        <thead>
            <tr>
                <th>编号</th>
                <th>登录人员</th>
                <th>Ip地址</th>
                <th>终端来源</th>
                <th class="" text-right"">登录时间</th>
            </tr>
        </thead>
        <tbody class=""section-items""></tbody>
    </table>
    <div class="" float-layout bc-clr-white padding-10px "">
        <label class="" InspageCount float-left"">
            共 <span class=""items-count"">0</span> 个记录
        </label>
        <div class="" float-right"">
            <button class="" btn-default btn btn-sm "">
                <span>第</span>
                <span class=""show-page-Index"">
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
                </");
            WriteLiteral(@"span>
            </button>
            <button class=""First btn btn-primary btn-sm""> <span class=""glyphicon glyphicon-backward""></span>  首页</button>
            <button class=""Previous btn btn-primary btn-sm""> <span class=""glyphicon glyphicon-chevron-left""></span> 上一页</button>
            <button class=""Next btn btn-primary btn-sm"">下一页 <span class=""glyphicon glyphicon-chevron-right""></span> </button>
            <button data-lastIndex="""" class=""Last btn btn-primary btn-sm"">尾页 <span class=""glyphicon glyphicon-forward""></span> </button>
            <select id=""pageSkipNext"" data-options=""true"" class="" margin-left-10px"" data-height=""27"" data-width=""45""></select>
            <button data-skip class=""pageSkip btn btn-sm btn-primary"">跳转</button>
        </div>
    </div>
</div>
<script id=""item-template"" type=""x-tmpl-mustache"">
    id = log.LogUserLoginId,
    uid = log.ID,
    addTime = _logger.FormatDateLocal(log.LoginTime),
    ip = log.LoginIp,
    terminal = log.Terminal
    {{#items}}
    <");
            WriteLiteral(@"tr>
    <tr>
        <td>
            <label class="" label label-primary "">
                {{index}}
            </label>
        </td>
        <td>
            {{uid}}
        </td>
        <td>
            {{ip}}
        </td>

        <td>
            {{terminal}}
        </td>
        <td class=""text-right"">
           {{addTime}}
        </td>
    </tr>
    {{/items}}

</script>
");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(4446, 6629, true);
                WriteLiteral(@"
    <script>

        laydate.render({
            elem: '#TimeLeft',
            type: 'datetime',
            value: '1900-01-01 00:00:00'
        });

        laydate.render({
            elem: '#TimeRight',
            type: 'datetime',
            value: '2100-01-01 00:00:00'
        });

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

        function CheckDateTime(str) {
            var reg = /^(\d+)-(\d{1,2})-(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
      ");
                WriteLiteral(@"      var r = str.match(reg);
            if (r == null) return false;
            r[2] = r[2] - 1;
            var d = new Date(r[1], r[2], r[3], r[4], r[5], r[6]);
            if (d.getFullYear() != r[1]) return false;
            if (d.getMonth() != r[2]) return false;
            if (d.getDate() != r[3]) return false;
            if (d.getHours() != r[4]) return false;
            if (d.getMinutes() != r[5]) return false;
            if (d.getSeconds() != r[6]) return false;
            return true;
        }

        function loadPageByIndex(index) {

            if (!CheckDateTime($('input[name=""TimeLeft""]').val().trim())) {
                onMask(""错误信息"", ""第一个日期的格式错误 例子:年/月/日 时:分:秒"");
                return;
            }

            if (!CheckDateTime($('input[name=""TimeRight""]').val().trim())) {
                onMask(""错误信息"", ""第二个日期的格式错误 例子:年/月/日 时:分:秒"");
                return;
            }

            $.ajax({
                url: ""/LogL/Page"",
                type: ""po");
                WriteLiteral(@"st"",
                dataType: ""json"",
                data: {
                    index: index,
                    id: $('input[name=""KeyName""]').val(),
                    terminal: $('select[name=""Terminal""]').val(),
                    left: $('input[name=""TimeLeft""]').val(),
                    right: $('input[name=""TimeRight""]').val()
                },
                success: function (json, textStatus, jqXhr) {
                    if (json.isOk) {
                        if (json.items == null) {
                            $('.section-items').html("""");
                        } else {
                            for (var i = 0; i < json.items.length; i++) {
                                json.items[i].index = (i + 1);
                            }

                            var template = $('#item-template').html();
                            Mustache.parse(template);
                            var result = Mustache.render(template, json);
                            $('.s");
                WriteLiteral(@"ection-items').html(result);
                        }

                        $('.items-count').text(json.lineCount); //总数
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
                    onMas");
                WriteLiteral(@"k(""错误"", `网络接连错误..${errorThrown}`);
                }
            });
        }

        loadPageByIndex(1);

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
                loadPageByIndex($('select[data-options]");
                WriteLiteral(@"').val());
            }
        });

        $('#search-items').click(function (jqEvent) {
            loadPageByIndex(1);
        });

        $('.section-items').on('click',
            "".detail-button"",
            null,
            function (jqEvent) {
                $('#program').modal('show');
                var id = $(this).attr(""data-item-id"");
                $.ajax({
                    url: ""/LogS/Item"",
                    type: ""post"",
                    dataType: ""json"",
                    data: {
                        logId: id
                    },
                    success: function (json) {
                        console.log(json);
                        if (json.isOk) {
                            var template = $('#detail-template').html();
                            Mustache.parse(template);
                            var result = Mustache.render(template, json);
                            $('#application-detail .modal-dialog').html(result);
    ");
                WriteLiteral(@"                        $('#program').modal('hide');
                            $('#application-detail').modal('show');
                        } else {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LabExam.Models.Entities.Principal>> Html { get; private set; }
    }
}
#pragma warning restore 1591
