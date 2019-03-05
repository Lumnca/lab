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
using LabExam.Models.EntitiyViews;
using LabExam.Models.JsonModel;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class ReExamController : Controller
    {
        private readonly IEmailService _email;
        private readonly LabContext _context;
        private readonly ILoggerService _logger;
        private readonly ILoadConfigFileService _config;
        private readonly IHttpContextAnalysisService _analysis;

        public ReExamController(IEmailService email, LabContext context, ILoggerService logger, ILoadConfigFileService config, IHttpContextAnalysisService analysis)
        {
            _email = email;
            _context = context;
            _logger = logger;
            _config = config;
            _analysis = analysis;
        }

        public IActionResult Index()
        {
            return View(_context.Institute.ToList());
        }

        public IActionResult Page([Required] int index, [Required] int iId, [Required] int type, [Required] int pId,[Required] int status, [Required] int grade, String keyName)
        {
            if(ModelState.IsValid)
            {
                StringBuilder builder = new StringBuilder("select * from  ReExamApplicationView where ApplicationExamId > 0");
                if (iId > 0)
                {
                    builder.Append($" and InstituteId = {iId}");
                }

                if (type >= 0)
                {
                    builder.Append($" and StudentType = {type}");
                }

                if (pId >= 0)
                {
                    builder.Append($" and ProfessionId = {pId}");
                }

                if (status >= 0)
                {
                    builder.Append($" and ApplicationStatus = {status}");
                }

                if (grade >= 0)
                {
                    builder.Append($" and Grade = {grade}");
                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                if (keyName != null && keyName.Trim() != "")
                {
                    builder.Append($" and Name like '%@keyName%'");

                    SqlParameter parameter = new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@keyName",
                        Value = keyName
                    };
                    parameters.Add(parameter);

                    builder.Append($" or StudentId like '@id'");

                    SqlParameter parameterId = new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@id",
                        Value = keyName
                    };
                    parameters.Add(parameterId);
                }

                int pageSize = 14; //每一页的题目数量
                // ReSharper disable once CoVariantArrayConversion
                int dataCount = _context.VReExamApplicationMaps.FromSql(builder.ToString(), parameters.ToArray<SqlParameter>()).Count();
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
                var list = _context.VReExamApplicationMaps
                .FromSql(builder.ToString(), parameters.ToArray<SqlParameter>())
                .OrderBy(item => item.ApplicationStatus)
                .ThenBy(ap => ap.InstituteId)
                .ThenBy(ap => ap.AddTime)
                .Skip((index - 1) * pageSize)
                .Take(pageSize).Select(v => new
                {
                    addTime = v.AddTime.ToLocalTime(),
                    type = v.StudentType == StudentType.UnderGraduate? "本科生":"研究生",
                    sex = v.Sex? "男":"女",
                    detail = v,
                    isInspect = v.ApplicationStatus != ApplicationStatus.Submit,
                    status = v.ApplicationStatus == ApplicationStatus.Submit ? "尚未审核" : v.ApplicationStatus == ApplicationStatus.Fail ? "未通过审核" : "已通过审核"
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

        public IActionResult Pass([Required] int apId)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无学生管理操作权限"
                    });
                }
                LogPricipalOperation operation =
                    _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.InspectReExamApplication, $"查询编码:{apId}", $"审核学生重考申请 通过审核 操作类:{nameof(ApplicationForReExamination)}");

                ApplicationForReExamination application = _context.ApplicationForReExaminations.FirstOrDefault(app => app.ApplicationExamId == apId && app.ApplicationStatus == ApplicationStatus.Submit);

                if (application != null)
                {
                    operation.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                    application.ApplicationStatus = ApplicationStatus.Pass;
                    Student student = _context.Student.Find(application.StudentId);
                    if (student == null)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "学生不存在,或者已经删除"
                        });
                    }

                    if (student.IsPassExam)
                    {
                        application.ApplicationStatus = ApplicationStatus.Fail;
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "此人已经通过考试,自动判定审核失败"
                        });
                    }
                    else
                    {
                        application.ApplicationStatus = ApplicationStatus.Pass;
                        student.MaxExamCount += 2;
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = true,
                            title = "消息提示",
                            message = "审核成功"
                        });
                    }
                }
                else
                {
                    operation.PrincpalOperationStatus = PrincpalOperationStatus.Fail;
                    _logger.Logger(operation);
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "申请不存在,或者已经被审核或删除"
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

        public IActionResult Fail([Required] int apId)
        {

            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无学生管理操作权限"
                    });
                }

                LogPricipalOperation operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.InspectJoinApplication, $"查询编码:{apId}", $"审核学生重考申请 不通过审核 操作类: {nameof(ApplicationForReExamination)}");
                ApplicationForReExamination application = _context.ApplicationForReExaminations.Find(apId);

                if (application != null)
                {
                    application.ApplicationStatus = ApplicationStatus.Fail;
                    _context.LogPricipalOperations.Add(operation);
                    _context.SaveChanges();

                    _email.SendReEmail(application.Email, application.StudentId, application.Student?.Name,application.AddTime,false);
                    return Json(new
                    {
                        isOk = true,
                        title = "消息提示",
                        message = "审核完毕！"
                    });
                }
                else
                {
                    _logger.Logger(operation);
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "学生申请记录不存在,或者已经被删除！"
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

        public IActionResult Item([Required] int apId)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无学生管理操作权限"
                    });
                }
                
                vReExamApplicationMap application = _context.VReExamApplicationMaps.FirstOrDefault(v => v.ApplicationExamId == apId);
                if (application != null)
                {
                    float learingSum = _context.Progresses.Where(p => p.StudentId == application.StudentId).Sum(pl => pl.StudyTime);
                    int time = _context.ExaminationPapers.Count(pa => pa.StudentId == application.StudentId);
                    float maxScore = application.MaxExamScore;
                    return Json(new
                    {
                        isOk = true,
                        item = new
                        {
                            name = application.Name,
                            grade = application.Grade,
                            id = application.StudentId,
                            sex = application.Sex ? "女" : "男",
                            type = application.StudentType == StudentType.UnderGraduate ? "本科生" : "研究生",
                            insName = _context.Institute.Find(application.InstituteId)?.Name,
                            proName = _context.Professions.Find(application.ProfessionId)?.Name,
                            email = application.Email,
                            reason = application.Reason,
                            score = maxScore,
                            examTime = time,
                            learing = learingSum
                        },
                        title = "信息提示",
                        message = "加载成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "学生申请记录不存在,或者已经被删除!或者学生已经被删除了"
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

        [HttpPost]
        public IActionResult Delete([Required] int apId)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无学生管理操作权限"
                    });
                }

                ApplicationForReExamination application = _context.ApplicationForReExaminations.Find(apId);
                if (application != null)
                {
                    _context.ApplicationForReExaminations.Remove(application);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "信息提示",
                        message = "删除成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "学生申请记录不存在,或者已经被删除"
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

        [HttpPost]
        public IActionResult Not([Required] int iId, [Required] int type, [Required] int pId, [Required] int status, [Required] int grade, String keyName)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无学生管理操作权限"
                    });
                }

                StringBuilder builder =
                    new StringBuilder("select * from  ReExamApplicationView where ApplicationExamId > 0");
                if (iId > 0)
                {
                    builder.Append($" and InstituteId = {iId}");
                }

                if (type >= 0)
                {
                    builder.Append($" and StudentType = {type}");
                }

                if (pId >= 0)
                {
                    builder.Append($" and ProfessionId = {pId}");
                }

                if (status >= 0)
                {
                    builder.Append($" and ApplicationStatus = {status}");
                }

                if (grade >= 0)
                {
                    builder.Append($" and Grade = {grade}");
                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                if (keyName != null && keyName.Trim() != "")
                {
                    builder.Append($" and Name like '%@keyName%'");

                    SqlParameter parameter = new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@keyName",
                        Value = keyName
                    };
                    parameters.Add(parameter);

                    builder.Append($" or StudentId like '@id'");

                    SqlParameter parameterId = new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@id",
                        Value = keyName
                    };
                    parameters.Add(parameterId);
                }
                // ReSharper disable once CoVariantArrayConversion
                List<vReExamApplicationMap> list = _context.VReExamApplicationMaps
                    .FromSql<vReExamApplicationMap>(builder.ToString(), parameters.ToArray<SqlParameter>()).Where(app => app.ApplicationStatus == ApplicationStatus.Submit).ToList();

                if (list.Count == 0)
                {
                    return Json(new
                    {
                        isOk = true,
                        title = "信息提示",
                        message = "并无未审核的考试申请"
                    });
                }

                foreach (var item in list)
                {
                    LogPricipalOperation operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.InspectAllReExamApplicationPass, $"查询编码:{item.ApplicationExamId}", "审核所有学生加入考试申请 不允许通过 操作类: ApplicationJoinTheExamination");
                    _context.LogPricipalOperations.Add(operation);
                    _email.SendReEmail(item.Email, item.StudentId, item.Name, item.AddTime, false);
                    _context.Database.ExecuteSqlCommand($"UPDATE [dbo].[ApplicationForReExaminations] SET [ApplicationStatus] = {(int) ApplicationStatus.Fail} Where  [ApplicationExamId] = {item.ApplicationExamId}");
                }
                _context.SaveChanges();
                return Json(new
                {
                    isOk = true,
                    title = "消息提示",
                    message = "审核成功！"
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

        [HttpPost]
        public IActionResult All([Required] int iId, [Required] int type, [Required] int pId, [Required] int status, [Required] int grade, String keyName)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无学生管理操作权限"
                    });
                }

                StringBuilder builder = new StringBuilder("select * from  ReExamApplicationView where ApplicationExamId > 0");
                if (iId > 0)
                {
                    builder.Append($" and InstituteId = {iId}");
                }

                if (type >= 0)
                {
                    builder.Append($" and StudentType = {type}");
                }

                if (pId >= 0)
                {
                    builder.Append($" and ProfessionId = {pId}");
                }

                if (status >= 0)
                {
                    builder.Append($" and ApplicationStatus = {status}");
                }

                if (grade >= 0)
                {
                    builder.Append($" and Grade = {grade}");
                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                if (keyName != null && keyName.Trim() != "")
                {
                    builder.Append($" and Name like '%@keyName%'");

                    SqlParameter parameter = new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@keyName",
                        Value = keyName
                    };
                    parameters.Add(parameter);

                    builder.Append($" or StudentId like '@id'");

                    SqlParameter parameterId = new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@id",
                        Value = keyName
                    };
                    parameters.Add(parameterId);
                }
                // ReSharper disable once CoVariantArrayConversion
                List<vReExamApplicationMap> list = _context.VReExamApplicationMaps
                    .FromSql(builder.ToString(), parameters.ToArray<SqlParameter>()).Where(app => app.ApplicationStatus == ApplicationStatus.Submit).ToList();

                if (list.Count == 0)
                {
                    return Json(new
                    {
                        isOk = true,
                        title = "信息提示",
                        message = "并无未审核的考试申请"
                    });
                }

                SystemSetting setting = _config.LoadSystemSetting();//记载配置文件

                foreach (var item in list)
                {
                    LogPricipalOperation operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.InspectAllReExamApplicationFail, $"查询编码:{item.ApplicationExamId}", $"审核所有学生重考申请 不通过 操作类: {nameof(ApplicationForReExamination)}");
                    _context.Database.ExecuteSqlCommand($"UPDATE [dbo].[ApplicationForReExaminations] SET [ApplicationStatus] = {(int)ApplicationStatus.Fail} Where  [ApplicationExamId] = {item.ApplicationExamId}");
                    _email.SendReEmail(item.Email, item.StudentId, item.Name, item.AddTime, true, "");
                    _context.LogPricipalOperations.Add(operation);
                }
                _context.SaveChanges();
                return Json(new
                {
                    isOk = true,
                    title = "消息提示",
                    message = "审核成功！"
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