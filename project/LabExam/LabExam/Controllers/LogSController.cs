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
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabExam.Controllers
{
    public class LogSController : Controller
    {
        private readonly LabContext _context;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly ILoggerService _logger;

        public LogSController(LabContext context, IHttpContextAnalysisService analysis, ILoggerService logger)
        {
            _context = context;
            _analysis = analysis;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_context.Principals.ToList());
        }

        public IActionResult Page([Required] Int32 index,  String id, [Required] int code, [Required] DateTime left, [Required] DateTime right)
        {
            if (ModelState.IsValid)
            {
                StringBuilder builder = new StringBuilder("select * from  LogStudentOperations where LogStudentOperationId > 0");
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (id != null && id.Trim() != "")
                {
                    builder.Append(" and StudentId = @Id");
                    SqlParameter parameter = new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@Id",
                        Value = id.Trim()
                    };
                    parameters.Add(parameter);
                }

                if (code >= 0)
                {
                    builder.Append($" and StuOperationCode = {code}");
                }

                int pageSize = 12; //每一页的题目数量
                // ReSharper disable once CoVariantArrayConversion
                int dataCount = _context.LogStudentOperations.FromSql(builder.ToString(), parameters.ToArray<SqlParameter>()).Count();
                int pageCount = dataCount / pageSize; //有多少页
                int lastCount = dataCount % pageSize; //最后一页的题目数量
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
                var list = _context.LogStudentOperations
                    .FromSql(builder.ToString(), parameters.ToArray())
                    .Where(p => p.AddTime >= left && p.AddTime <= right)
                    .OrderByDescending(l => l.AddTime)
                    .Skip((index - 1) * pageSize)
                    .Take(pageSize)
                    .Select(log => new
                    {
                        id = log.LogStudentOperationId,
                        uId = log.StudentId,
                        addTime = _logger.FormatDateLocal(log.AddTime),
                        ip = log.OperationIp,
                        code = log.StuOperationCode.ToString(),
                        target = log.StuOperationName,
                        status = log.StuOperationStatus == StuOperationStatus.Success ? "操作成功" : "操作失败",
                        content = log.StuOperationContent
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

        public IActionResult Item([Required] Int32 logId)
        {
            if (ModelState.IsValid)
            {
                LogStudentOperation log = _context.LogStudentOperations.Find(logId);
                if (log == null)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "日志记录不存在,或者已经被删除！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = true,
                        title = "错误提示",
                        message = "加载成功",
                        id = log.LogStudentOperationId,
                        uId = log.StudentId,
                        addTime = _logger.FormatDateLocal(log.AddTime),
                        ip = log.OperationIp,
                        code = log.StuOperationCode.ToString(),
                        target = log.StuOperationName,
                        status = log.StuOperationStatus == StuOperationStatus.Success ? "操作成功" : "操作失败",
                        content = log.StuOperationContent
                    });
                }
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