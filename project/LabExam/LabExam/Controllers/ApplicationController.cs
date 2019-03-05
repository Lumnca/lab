using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.JsonModel;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class ApplicationController : Controller
    {
        private readonly IEmailService _email;
        private readonly LabContext _context;
        private readonly ILoggerService _logger;
        private readonly ILoadConfigFileService _config;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly IEncryptionDataService _encryption;

        public ApplicationController(IEmailService email, LabContext context, ILoggerService logger,
            IHostingEnvironment hosting, IHttpContextAnalysisService analysis, IEncryptionDataService encryption, ILoadConfigFileService config)
        {
            _email = email;
            _context = context;
            _logger = logger;
            _analysis = analysis;
            _encryption = encryption;
            _config = config;
        }

        public IActionResult Index()
        {
            return View(_context.Institute.ToList());
        }

        public IActionResult Page([Required] int index, [Required] int iId, [Required] int type, [Required] int pId,[Required] int status,[Required] int grade ,String keyName)
        {
            if (ModelState.IsValid)
            {
                StringBuilder builder =
                    new StringBuilder("select * from  ApplicationJoinTheExaminations where ApplicationJoinId > 0");
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
                        SqlDbType = SqlDbType.NVarChar, ParameterName = "@keyName", Value = keyName
                    };
                    parameters.Add(parameter);

                    builder.Append($" or StudentId like '@id'");

                    SqlParameter parameterId = new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar, ParameterName = "@id", Value = keyName
                    };
                    parameters.Add(parameterId);
                }

                int pageSize = 12; //每一页的题目数量
                // ReSharper disable once CoVariantArrayConversion
                int dataCount = _context.ApplicationJoinTheExaminations.FromSql(builder.ToString(), parameters.ToArray<SqlParameter>()).Count();
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

                //做了两个内连接  查询出学院 和专业
                // ReSharper disable once CoVariantArrayConversion
                var list = _context.ApplicationJoinTheExaminations
                    .FromSql(builder.ToString(), parameters.ToArray<SqlParameter>())
                    .OrderBy(item => item.ApplicationStatus)
                    .ThenBy(ap => ap.InstituteId)
                    .ThenBy(ap => ap.AddTime)
                    .Join(_context.Institute, p => p.InstituteId, i => i.InstituteId, (application, institute) => new
                    {
                        app = application,
                        insName = institute.Name
                    })
                    .Join(_context.Professions, ip => ip.app.ProfessionId, p => p.ProfessionId, (ip, p) => new
                    {
                        applicationForm = ip.app, //ApplicationJoinTheExaminations
                        instName = ip.insName, //学院名称
                        profesName = p.Name //专业名称
                    })
                    .Skip((index - 1) * pageSize)
                    .Take(pageSize)
                    .Select(v => new
                    {
                        type = v.applicationForm.StudentType == StudentType.UnderGraduate?"本科生":"研究生",
                        sex = v.applicationForm.Sex?"男":"女",
                        application = v.applicationForm,
                        insName = v.instName,
                        proName =v.profesName,
                        addTime = _logger.FormatDateLocal(v.applicationForm.AddTime),
                        isInspect = v.applicationForm.ApplicationStatus != ApplicationStatus.Submit,
                        status = v.applicationForm.ApplicationStatus == ApplicationStatus.Submit? "尚未审核":v.applicationForm.ApplicationStatus == ApplicationStatus.Fail? "未通过审核":"已通过审核"
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

        /// <summary>
        /// 日志记录完成
        /// </summary>
        /// <param name="apId"></param>
        /// <returns></returns>
        [HttpPost]
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
                    _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.InspectJoinApplication, $"{apId}", $"审核学生加入考试申请 通过审核");

                ApplicationJoinTheExamination applicationJoin = _context.ApplicationJoinTheExaminations.Find(apId);

                if (applicationJoin != null)
                {
                    //是否已经存在了
                    if (_context.Student.Any(s=> s.StudentId == applicationJoin.StudentId))
                    {
                        _email.SendJoinEmail(applicationJoin.Email,applicationJoin.StudentId,applicationJoin.Name,applicationJoin.AddTime,false,"你已经在考试范围内！");
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "审核结果: 此学生已经在考试范围内! 审核此申请失败."
                        });
                    }
                    else
                    {
                        Student student = (Student) applicationJoin;
                        // //身份证后六位就是密码
                        student.Password = _encryption.EncodeByMd5(_encryption.EncodeByMd5(student.IDNumber.Substring(student.IDNumber.Length - 6, 6)));

                        SystemSetting setting = _config.LoadSystemSetting();
                        //如果这个学院有对应的模块 然后找到这个模块的 考试设置类
                        var  insModule = _context.InstituteToModules.FirstOrDefault(im => im.InstituteId == student.InstituteId);
                        if (insModule != null)
                        {
                            //如果这个模块具有加载类
                            Boolean isHave = setting.ExamModuleSettings.TryGetValue(insModule.ModuleId, out var meSetting);
                            student.MaxExamCount = isHave? meSetting.AllowExamTime:2;
                        }
                        else
                        {
                            //如果学院灭有属于哪个模块
                            student.MaxExamCount = 2;
                        }

                        operation.PrincpalOperationStatus = PrincpalOperationStatus.Success; //日志记录 成功
                        applicationJoin.ApplicationStatus = ApplicationStatus.Pass;

                        _context.LogPricipalOperations.Add(operation);
                        _context.Student.Add(student);
                        _context.SaveChanges();

                        _email.SendJoinEmail(applicationJoin.Email, applicationJoin.StudentId, applicationJoin.Name, applicationJoin.AddTime, true, "");
                        return Json(new
                        {
                            isOk = true,
                            title = "信息提示",
                            message = "审核完成！"
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
                        message = "申请不存在,或者已经被删除"
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

        /// <summary>
        /// 日志记录完成
        /// </summary>
        /// <param name="apId"></param>
        /// <returns></returns>
        [HttpPost]
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
                LogPricipalOperation operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.InspectJoinApplication, $"{apId}", "审核学生加入考试申请 不通过审核 ");

                ApplicationJoinTheExamination applicationJoin = _context.ApplicationJoinTheExaminations.Find(apId);
                if (applicationJoin != null)
                {
                    applicationJoin.ApplicationStatus = ApplicationStatus.Fail;
                    _email.SendJoinEmail(applicationJoin.Email, applicationJoin.StudentId, applicationJoin.Name, applicationJoin.AddTime, false, "申请原因说明不详细！或者个人信息有误！");
                    _context.LogPricipalOperations.Add(operation);
                    _context.SaveChanges();
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
        [HttpPost]
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

                ApplicationJoinTheExamination applicationJoin = _context.ApplicationJoinTheExaminations.Find(apId);
                if (applicationJoin != null)
                {
                    
                    return Json(new
                    {
                        isOk = true,
                        item = new
                        {
                            name = applicationJoin.Name,
                            grade = applicationJoin.Grade,
                            id = applicationJoin.StudentId, 
                            sex = applicationJoin.Sex?"女":"男",
                            type = applicationJoin.StudentType == StudentType.UnderGraduate?"本科生":"研究生",
                            insName = _context.Institute.Find(applicationJoin.InstituteId)?.Name,
                            proName = _context.Professions.Find(applicationJoin.ProfessionId)?.Name,
                            email = applicationJoin.Email,
                            reason = applicationJoin.Reason
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

                ApplicationJoinTheExamination applicationJoin = _context.ApplicationJoinTheExaminations.Find(apId);
                if (applicationJoin != null)
                {
                    _context.ApplicationJoinTheExaminations.Remove(applicationJoin);
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
                    new StringBuilder("select * from  ApplicationJoinTheExaminations where ApplicationJoinId > 0");
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
                List<ApplicationJoinTheExamination> list = _context.ApplicationJoinTheExaminations
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

                foreach (var item in list)
                {
                    LogPricipalOperation operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.InspectAllJoinApplicationFail,
                        $"{item.ApplicationJoinId}", "审核所有学生加入考试申请 不允许通过");
                    item.ApplicationStatus = ApplicationStatus.Fail;
                    _context.LogPricipalOperations.Add(operation);
                    _email.SendJoinEmail(item.Email, item.StudentId, item.Name, item.AddTime, false, "申请原因说明不详细！或者个人信息有误！");
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

                StringBuilder builder =
                    new StringBuilder("select * from  ApplicationJoinTheExaminations where ApplicationJoinId > 0");
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
                List<ApplicationJoinTheExamination> list = _context.ApplicationJoinTheExaminations
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
                    LogPricipalOperation operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.InspectAllJoinApplicationPass,
                        $"{item.ApplicationJoinId}", "审核所有学生加入考试申请 不允许通过 ");
                    item.ApplicationStatus = ApplicationStatus.Pass;
                    _context.LogPricipalOperations.Add(operation);
                    Student student = (Student)item;
                    //身份证后六位就是密码
                    student.Password = _encryption.EncodeByMd5(_encryption.EncodeByMd5(student.IDNumber.Substring(student.IDNumber.Length - 6, 6)));
                    var insModule = _context.InstituteToModules.FirstOrDefault(im => im.InstituteId == student.InstituteId);
                    if (insModule != null)
                    {
                        //如果这个模块具有加载类
                        Boolean isHave = setting.ExamModuleSettings.TryGetValue(insModule.ModuleId, out var meSetting);
                        student.MaxExamCount = isHave ? meSetting.AllowExamTime : 2;
                    }
                    else
                    {
                        //如果学院没有属于哪个模块 默认为 2
                        student.MaxExamCount = 2;
                    }

                    _context.Student.Add(student);
                    //邮件通知
                    _email.SendJoinEmail(item.Email, item.StudentId, item.Name, item.AddTime, true, "");
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
