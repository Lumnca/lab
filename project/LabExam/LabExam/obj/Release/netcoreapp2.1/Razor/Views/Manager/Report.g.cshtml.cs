#pragma checksum "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Manager\Report.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "01de351b94d7cc9a9db3ce8a93f6d5fe15ea537a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manager_Report), @"mvc.1.0.view", @"/Views/Manager/Report.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manager/Report.cshtml", typeof(AspNetCore.Views_Manager_Report))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01de351b94d7cc9a9db3ce8a93f6d5fe15ea537a", @"/Views/Manager/Report.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25a346eec04c34e7426a0411470cd3c767046258", @"/Views/_ViewImports.cshtml")]
    public class Views_Manager_Report : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/echarts/echarts.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/echarts/walden.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "F:\四川师范大学实验室安全考试项目\lab\project\LabExam\LabExam\Views\Manager\Report.cshtml"
  
    ViewData["Title"] = "实验室安全教育在线-学生统计报表";
    Layout = "~/Views/Shared/_BackEnd_Layout.cshtml";

#line default
#line hidden
            BeginContext(109, 52, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6eddf527dcf547498ba97b9347156724", async() => {
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
            BeginContext(161, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(163, 47, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a5562957183b4a3b8d06dd2deb030909", async() => {
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
            BeginContext(210, 1750, true);
            WriteLiteral(@"
<div class="" margin-5px bc-clr-white  padding-10px border-little-grey-all "">
    <div id=""tablist-hover"">
        <ul class=""nav nav-tabs"" role=""tablist"">
            <li role=""presentation"" class=""active"">
                <a href=""#firstView"" aria-controls=""home"" role=""tab"" data-toggle=""tab"">
                    统计视图
                </a>
            </li>
            <li role=""presentation"">
                <a href=""#addInstitute"" aria-controls=""profile"" role=""tab""
                   data-toggle=""tab"">学院人数统计</a>
            </li>

        </ul>
        <div class=""tab-content margin-top-10px"">
            <div role=""tabpanel"" class=""tab-pane active"" id=""firstView"">
                <div style="" height:320px;"" id=""LoginStatus"">

                </div>
                <div class="" flex-layout"">
                    <div class="" flex-item-4 bc-clr-white  "" id=""ChartExamInfo"">
                        <div id=""chart-student-info"" style="" height:320px;"">

                        </div>

  ");
            WriteLiteral(@"                  </div>
                </div>

                <div class="" flex-layout"">

                    <div class=""flex-item-3  bc-clr-white margin-5px padding-10px"">
                        <div id=""WebSiteCallOption"" style="" width:100%; height:300px"">

                        </div>
                    </div>

                </div>

            </div>
            <div role=""tabpanel"" class=""tab-pane"" id=""addInstitute"">
                <div class=""flex-item-5  bc-clr-white margin-5px padding-10px"">
                    <div id=""ModelStatus"" style="" width:1200px; height:950px"">

                    </div>
                </div>
            </div>

        </div>
    </div>
</div>

");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(1983, 11567, true);
                WriteLiteral(@"
    <script>
        var theme = ""walden"";

        function Format(date) {
            var year = date.getFullYear();
            var $month = date.getMonth() + 1;
            var month = $month > 9 ? $month.toString() : '0' + $month;
            var $date = date.getDate();
            var day = $date > 9 ? $date.toString() : ""0"" + $date;
            return `${year}/${month}/${day} 0:00`;
        }

        function getLastDay(date, last) {
            var $date = new Date(date.getTime());
            $date.setDate($date.getDate() - last);
            return $date;
        }

        function getLastMonth(date, length) {
            var dateArray = new Array();

            for (let i = 0; i < length; i++) {
                var $date = getLastDay(date, i);
                var format = Format($date);
                dateArray.push(format);
            }
            return dateArray;
        }

        $('#LoginStatus').width($('#firstView').width());

        console.log($('#L");
                WriteLiteral(@"oginStatus').width());

        var LoginChart = echarts.init(document.getElementById('LoginStatus'), theme);
        LoginChart.showLoading();
        $.ajax({
            url: ""/EChart/Login"",
            type: ""get"",
            dataType: ""json"",
            success: function (json, textStatus, jqXhr) {
                console.log(json);
                var timeData = getLastMonth(new Date(), json.length);

                console.log(timeData);

                var values = [];

                for (var index in timeData) {
                    var key = timeData[index];
                    if (json.data.hasOwnProperty(key)) {
                        values.unshift(json.data[key]);
                    } else {
                        values.unshift(0);
                    }
                }
                console.log(values);
                

                var loginStatusOption = {
                    title: {
                        x: 'center',
                        t");
                WriteLiteral(@"ext: ""系统登录情况"",
                        subtext: ""[学生和管理员的登录情况]"",
                        textStyle: {
                            fontSize: 16
                        }
                    },
                    grid: {
                        show: false,
                        borderColor: ""red"",
                        borderWidth: 2,
                        left: 50,
                        bottom: 60,
                        right: 40
                    },
                    dataZoom: {
                        type: 'slider',
                        realtime: true,
                        start: 0,
                        end: 100,
                        xAxisIndex: 0,
                        backgroundColor: 'rgba(38,38,38,0.1)'
                    },
                    xAxis: {
                        name: ""时间"",
                        show: true,
                        type: ""category"",
                        data: timeData
                    },
                   ");
                WriteLiteral(@" yAxis: {
                        name: ""人数"",
                        min: 0,
                    },
                    legend: {
                        x: ""right""
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            animation: false
                        }
                    },
                    series: [
                        {
                            name: '登陆人数',
                            type: 'line',
                            symbolSize: 8,
                            hoverAnimation: false,
                            data: values,
                            areaStyle: {}
                        }
                    ]
                }

                        
                LoginChart.hideLoading();
                LoginChart.setOption(loginStatusOption,true);
            },
            error: function (jqXHR, textStatus, errorThrown) {
        ");
                WriteLiteral(@"        onMask(""错误"", errorThrown);
            }
        });

        var app = {}
        app.currentIndex = -1;

        var myChart = echarts.init(document.getElementById('chart-student-info'), theme);

        var model = echarts.init(document.getElementById('WebSiteCallOption'), theme);

        myChart.showLoading();
        model.showLoading();

        $.ajax({
            url: ""/EChart/Distribute"",
            type: ""get"",
            dataType: ""json"",
            success: function (json, textStatus, jqXhr) {
                console.log(json);
                var optionExam = {
                    title: {
                        text: '系统成员分布',
                        subtext: ""System member distribution"",
                        x: 'left',
                        textStyle: {
                            fontSize: 16
                        }
                    },
                    legend: {
                        type: 'scroll',
                        orient: 've");
                WriteLiteral(@"rtical',
                        right: 10,
                        top: 45,
                        bottom: 20
                    },
                    toolbox: {
                        orient: ""horizontal"",
                        itemSize: ""16"",
                        itemGap: 10,
                        feature: {
                            saveAsImage: {
                                name: ""系统成员分布""
                            },
                            restore: {

                            },
                        }
                    },
                    tooltip: {
                        trigger: 'item',
                        formatter: ""{a} <br/>{b} : {c}人 ({d}%)""
                    },
                    series: [
                        {
                            name: '情况',
                            type: 'pie',
                            radius: '55%',
                            center: ['50%', '60%'],
                            data: json.d");
                WriteLiteral(@"ata,
                            itemStyle: {
                                emphasis: {
                                    shadowBlur: 10,
                                    shadowOffsetX: 0,
                                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                                }
                            }
                        }
                    ]
                };

                setInterval(function () {
                    var dataLen = optionExam.series[0].data.length;
                    // 取消之前高亮的图形
                    myChart.dispatchAction({
                        type: 'downplay',
                        seriesIndex: 0,
                        dataIndex: app.currentIndex
                    });
                    app.currentIndex = (app.currentIndex + 1) % dataLen;
                    // 高亮当前图形
                    myChart.dispatchAction({
                        type: 'highlight',
                        seriesIndex: 0,
                        ");
                WriteLiteral(@"dataIndex: app.currentIndex
                    });
                    // 显示 tooltip
                    myChart.dispatchAction({
                        type: 'showTip',
                        seriesIndex: 0,
                        dataIndex: app.currentIndex
                    });
                }, 2500);

                myChart.hideLoading();
                myChart.setOption(optionExam, true);

                var callOption = {
                    title: {
                        text: '用户访问方式',
                        x: 'center',
                        subtext: ""用户访问终端""
                    },
                    tooltip: {
                        trigger: 'item',
                        formatter: ""{a} <br/>{b} : {c} ({d}%)""
                    },
                    legend: {
                        orient: 'vertical',
                        x: 'left'
                    },
                    toolbox: {
                        show: true,
                        f");
                WriteLiteral(@"eature: {
                            mark: { show: true },
                            dataView: { show: true, readOnly: false },
                            magicType: {
                                show: true,
                                type: ['pie', 'funnel'],
                                option: {
                                    funnel: {
                                        x: '25%',
                                        width: '50%',
                                        funnelAlign: 'left',
                                        max: 1548
                                    }
                                }
                            },
                            restore: { show: true },
                            saveAsImage: { show: true }
                        }
                    },
                    calculable: true,
                    series: [
                        {
                            name: '访问来源',
                            ");
                WriteLiteral(@"type: 'pie',
                            radius: '55%',
                            center: ['50%', '60%'],
                            data: json.terminals
                        }
                    ]
                };
                
                model.hideLoading();
                model.setOption(callOption);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                onMask(""错误"", errorThrown);
            }
        });

        var modelStatus = echarts.init(document.getElementById('ModelStatus'), theme);
        $.ajax({
            url: ""/EChart/Institute"",
            type: ""get"",
            dataType: ""json"",
            success: function (json, textStatus, jqXhr) {
               
                var optionModel = {
                    title: {
                        text: ""各学院学生数量"",
                        subtext: ""Student Distribution"",
            
                        textStyle: {
                            fontSize: 16");
                WriteLiteral(@"
                        }
                    },
                    tooltip: {
                        trigger: 'axis'

                    },
                    legend: {},
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    xAxis: [
                        {
                            type: 'value'
                        }
                    ],
                    yAxis:
                    {
                        type: 'category',
                        data: json.names,
                        axisTick: {
                            alignWithLabel: true
                        }
                    }
                    ,
                    series:
                    {
                        name: '拥有学生数量',
                        type: 'bar',
                        barWidth: '60%',
       ");
                WriteLiteral(@"                 data: json.vals
                    }

                };
                modelStatus.setOption(optionModel);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                onMask(""错误"", errorThrown);
            }
        });


    </script>
");
                EndContext();
            }
            );
            BeginContext(13553, 2, true);
            WriteLiteral("\r\n");
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
