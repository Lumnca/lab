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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class StatisticsController : Controller
    {
        private readonly LabContext _context;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly ILoggerService _logger;

        public StatisticsController(LabContext context, IHttpContextAnalysisService analysis, ILoggerService logger)
        {
            _context = context;
            _analysis = analysis;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Institute()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Page([Required] int index, [Required] int instituteId, [Required] int grade,[Required] int orderOne)
        {
            if (ModelState.IsValid)
            {
                StringBuilder builder = new StringBuilder("select * from  [ExamResultView] where [InstituteId] > 0");

                List<SqlParameter> parameters = new List<SqlParameter>();

                if (instituteId > 0)
                {
                    builder.Append($" and [InstituteId] = {instituteId}");
                }

                if (grade > -1)
                {
                    builder.Append($" and [Grade] = {grade}");
                }


                int pageSize = 14; //每一页的题目数量
                // ReSharper disable once CoVariantArrayConversion
                int dataCount = _context.VExamResultMaps.FromSql(builder.ToString(), parameters.ToArray<SqlParameter>()).Count();
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

                if (orderOne == 0)
                {
                    // ReSharper disable once CoVariantArrayConversion
                    var list = _context.VExamResultMaps
                        .FromSql(builder.ToString(), parameters.ToArray<SqlParameter>())
                        .OrderByDescending(v => v.TotalPassRate)
                        .Skip((index - 1) * pageSize)
                        .Take(pageSize).Select(v => new
                        {
                            grade = v.Grade,
                            name = v.Name,
                            total = v.Total,
                            passTotal = v.PassTotal,
                            postCount = v.PostCount,
                            underCount = v.UnderCount,
                            postPassCount = v.PostPassCount,
                            postNotPassCount = v.PostNotPassCount,
                            underPassCount = v.UnderPassCount,
                            underNotPassCount = v.UnderNotPassCount,
                            totalPassRate = v.TotalPassRate,
                            postPassRate = v.PostPassRate,
                            underPossRate = v.UnderPassRate
                        }).ToList();

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
                else if (orderOne > 0)
                {
                    // ReSharper disable once CoVariantArrayConversion
                    var list = _context.VExamResultMaps
                        .FromSql(builder.ToString(), parameters.ToArray<SqlParameter>())
                        .OrderByDescending(v => v.PostPassRate)
                        .Skip((index - 1) * pageSize)
                        .Take(pageSize).Select(v => new
                        {
                            grade = v.Grade,
                            name = v.Name,
                            total = v.Total,
                            passTotal = v.PassTotal,
                            postCount = v.PostCount,
                            underCount = v.UnderCount,
                            postPassCount = v.PostPassCount,
                            postNotPassCount = v.PostNotPassCount,
                            underPassCount = v.UnderPassCount,
                            underNotPassCount = v.UnderNotPassCount,
                            totalPassRate = v.TotalPassRate,
                            postPassRate = v.PostPassRate,
                            underPossRate = v.UnderPassRate
                        }).ToList();

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
                    // ReSharper disable once CoVariantArrayConversion
                    var list = _context.VExamResultMaps
                        .FromSql(builder.ToString(), parameters.ToArray<SqlParameter>())
                        .OrderByDescending(v => v.UnderPassRate)
                        .Skip((index - 1) * pageSize)
                        .Take(pageSize).Select(v => new
                        {
                            grade = v.Grade,
                            name = v.Name,
                            total = v.Total,
                            passTotal = v.PassTotal,
                            postCount = v.PostCount,
                            underCount = v.UnderCount,
                            postPassCount = v.PostPassCount,
                            postNotPassCount = v.PostNotPassCount,
                            underPassCount = v.UnderPassCount,
                            underNotPassCount = v.UnderNotPassCount,
                            totalPassRate = v.TotalPassRate,
                            postPassRate = v.PostPassRate,
                            underPossRate = v.UnderPassRate
                        }).ToList();

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

        [HttpGet]
        public IActionResult Grade()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GPage([Required] int index,  [Required] int grade, [Required] int orderOne)
        {
            if (ModelState.IsValid)
            {
                StringBuilder builder = new StringBuilder("select * from  [ExamGradeResultView] where [Grade] > 0");

                List<SqlParameter> parameters = new List<SqlParameter>();

                if (grade > -1)
                {
                    builder.Append($" and [Grade] = {grade}");
                }


                int pageSize = 14; //每一页的题目数量
                // ReSharper disable once CoVariantArrayConversion
                int dataCount = _context.VExamGradeResultMaps.FromSql(builder.ToString(), parameters.ToArray<SqlParameter>()).Count();
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

                if (orderOne == 0)
                {
                    // ReSharper disable once CoVariantArrayConversion
                    var list = _context.VExamGradeResultMaps
                        .FromSql(builder.ToString(), parameters.ToArray<SqlParameter>())
                        .OrderByDescending(v => v.PassTotleRate)
                        .Skip((index - 1) * pageSize)
                        .Take(pageSize).ToList();

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
                else if (orderOne > 0)
                {
                    // ReSharper disable once CoVariantArrayConversion
                    var list = _context.VExamGradeResultMaps
                        .FromSql(builder.ToString(), parameters.ToArray<SqlParameter>())
                        .OrderByDescending(v => v.PostPassRate)
                        .Skip((index - 1) * pageSize)
                        .Take(pageSize).ToList();

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
                    // ReSharper disable once CoVariantArrayConversion
                    var list = _context.VExamGradeResultMaps
                        .FromSql(builder.ToString(), parameters.ToArray<SqlParameter>())
                        .OrderByDescending(v => v.UnderPassRate)
                        .Skip((index - 1) * pageSize)
                        .Take(pageSize).ToList();

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

        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }
    }
}