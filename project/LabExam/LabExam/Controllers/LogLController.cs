using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabExam.Controllers
{
    public class LogLController : Controller
    {
        private readonly LabContext _context;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly ILoggerService _logger;
        public LogLController(LabContext context, IHttpContextAnalysisService analysis, ILoggerService logger)
        {
            _context = context;
            _analysis = analysis;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Page([Required] Int32 index,  String id, [Required] int terminal, [Required] DateTime left, [Required] DateTime right)
        {
            if (ModelState.IsValid)
            {
                StringBuilder builder = new StringBuilder("select * from  LogUserLogin where LogUserLoginId > 0");
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (id != null && id.Trim() != "" )
                {
                    builder.Append(" and ID = @Id");
                    SqlParameter parameter = new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@Id",
                        Value = id.Trim()
                    };

                    parameters.Add(parameter);
                }

                if (terminal >= 0)
                {
                    builder.Append($" and Terminal = {terminal}");
                }
                int pageSize = 12; //每一页的题目数量
                // ReSharper disable once CoVariantArrayConversion
                int dataCount = _context.LogUserLogin.FromSql(builder.ToString(), parameters.ToArray<SqlParameter>()).Count();
                int pageCount = dataCount / pageSize;  //有多少页
                int lastCount = dataCount % pageSize;  //最后一页的题目数量

                if (lastCount > 0)
                {
                    pageCount++;
                }

                if (index > pageCount || index <= 0)
                {
                    return Json(new
                    {
                        isOk = true,
                        lineCount = 0,
                        pageCount = 1, //总共是多少页
                        pageNowIndex = 1, //当前是第几页
                    });
                }

                // ReSharper disable once CoVariantArrayConversion
                var list = _context.LogUserLogin
                    .FromSql(builder.ToString() , parameters.ToArray())
                    .Where(p => p.LoginTime >= left && p.LoginTime <= right)
                    .OrderByDescending(l => l.LoginTime)
                    .Skip((index - 1) * pageSize)
                    .Take(pageSize)
                    .Select(log => new
                    {
                        id = log.LogUserLoginId,
                        uid = log.ID,
                        addTime = _logger.FormatDateLocal(log.LoginTime),
                        ip = log.LoginIp,
                        terminal = log.Terminal == Terminal.Pc?"电脑终端":(log.Terminal == Terminal.Phone? "手机终端":"平板终端")
                    })
                    .ToList();

                return Json(new
                {
                    isOk = true,
                    lineCount = dataCount,
                    PageCount = pageCount, //总共是多少页
                    pageNowIndex = index, //当前是第几页
                    items = list,
                    size = pageSize
                });
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

    }
}