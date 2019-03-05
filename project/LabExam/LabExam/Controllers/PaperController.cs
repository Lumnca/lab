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
using LabExam.Models;
using LabExam.Models.Entities;
using LabExam.Models.EntitiyViews;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    public class PaperController : Controller
    {
        private readonly LabContext _context;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly ILoggerService _logger;

        public PaperController(LabContext context, IHttpContextAnalysisService analysis, ILoggerService logger)
        {
            _context = context;
            _analysis = analysis;
            _logger = logger;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Page([Required] int index, [Required] int isFinish,[Required] int paperId,String studentId,float leftScore = 0,float rightScore = float.MaxValue)
        {
            if (ModelState.IsValid)
            {
                StringBuilder builder = new StringBuilder("select * from  [ExaminationPapers] where [PaperId] > 0");

                List<SqlParameter> parameters = new List<SqlParameter>();

                if (studentId != null && studentId.Trim() != "")
                {
                    builder.Append(" and StudentId = @StudentId");

                    parameters.Add(new SqlParameter { ParameterName = "@StudentId", Value = studentId.Trim(), SqlDbType = SqlDbType.NVarChar });
                }

                if (paperId > 0)
                {
                    builder.Append($" and [PaperId] = {paperId}");
                }

                if (isFinish > -1)
                {
                    if(isFinish == 0)
                    {
                        builder.Append(" and [IsFinish]  = 0");
                    }
                    else
                    {
                        builder.Append(" and [IsFinish] = 1");
                    }
                }
                builder.Append($" and Score > {leftScore} and  Score < {rightScore}");

                int pageSize = 12; //每一页的题目数量
                // ReSharper disable once CoVariantArrayConversion
                int dataCount = _context.ExaminationPapers.FromSql(builder.ToString(), parameters.ToArray<SqlParameter>()).Count();
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
                var list = _context.ExaminationPapers
                .FromSql(builder.ToString(), parameters.ToArray<SqlParameter>())
                .OrderByDescending(item => item.PassScore)
                .ThenBy(ap => ap.Score)
                .ThenBy(ap => ap.TotleScore)
                .Skip((index - 1) * pageSize)
                .Take(pageSize).Select(v => new
                {
                    studentId = v.StudentId,
                    examTime = v.ExamTime,
                    addTime = _logger.FormatDateLocal(v.AddTime),
                    leaveExamTime = v.LeaveExamTime,
                    totleScore = v.TotleScore,
                    score = v.Score,
                    isFinish = v.IsFinish ? "已完成":"未完成",
                    passScore = v.PassScore,
                    paperId = v.PaperId
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
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        public IActionResult Detail([Required] int pId)
        {
            if (ModelState.IsValid)
            {
                ExaminationPaper paper = _context.ExaminationPapers.Find(pId);
                if (paper == null)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "请求的试卷不存在！或者已经被删除了！"
                    });
                }
                else
                {
                    vStatisticJudgeMap judge =
                        _context.VStatisticJudgeMaps.FirstOrDefault(j => j.PaperId == paper.PaperId);
                    vStatisticMultipleMap multiple =
                        _context.VStatisticMultipleMaps.FirstOrDefault(m => m.PaperId == paper.PaperId);
                    vStatisticSingleMap single =
                        _context.VStatisticSingleMaps.FirstOrDefault(m => m.PaperId == paper.PaperId);

                    return Json(new
                    {
                        isOk = true,
                        title ="提示",
                        message ="加载成功",
                        judge = judge,
                        multiple = multiple,
                        single = single,
                        review = paper.Review
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误"
                });
            }
        }
    }
}
