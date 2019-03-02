using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models;
using LabExam.Models.Entities;
using LabExam.Models.EntitiyViews;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.JsonModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IEmailService _email;
        private readonly LabContext _context;
        private readonly ILoggerService _logger;
        private readonly ILoadConfigFileService _config;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly IEncryptionDataService _encryption;
        private readonly IFileHandleService _handle;

        public StudentController(IEmailService email, LabContext context, ILoggerService logger, ILoadConfigFileService config, IHttpContextAnalysisService analysis, IEncryptionDataService encryption, IFileHandleService handle)
        {
            _email = email;
            _context = context;
            _logger = logger;
            _config = config;
            _analysis = analysis;
            _encryption = encryption;
            _handle = handle;
        }

        // GET: /<controller>/ 学生主页
        public async Task<IActionResult> Index()
        {
             try{

                LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
                Student student = _context.Student.Find(model.UserId);
                
                //错误情况： 学生已经被删除 但是浏览器Cookie 中仍然保存着学生账号！
                if (student == null)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    RedirectToAction("Error", "Error",new
                    {
                        data = "错误信息！ 你的登录账号已经被删除不存在！请重新登录账号"
                    });
                }

                ViewBag.ModuleName = _context.VStudentMaps.FirstOrDefault(vm => vm.StudentId == student.StudentId)?.ModuleName;
                return View(new StudentViewModel(student, _context));
             }
             catch (Exception e)
             {
                Console.WriteLine(e);
                throw;
             }
        }

        [HttpGet]
        public IActionResult Application()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Application([Required] String reason)
        {
            if (ModelState.IsValid)
            {
                Student student = _context.Student.Find(_analysis.GetLoginUserModel(HttpContext).UserId);

                if (student == null)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误信息！",
                        message = "登录超时! 请重新登录."
                    });
                } 

                if (String.IsNullOrEmpty(student.Email))
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你尚未绑定邮箱,请到个人中心绑定邮箱后申请重考！"
                    });
                }

                if (_context.ApplicationForReExaminations.Any(val => val.StudentId == student.StudentId && val.ApplicationStatus == ApplicationStatus.Submit))
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "请等待你之前的申请审核完毕后再填写！"
                    });
                }

                InstituteToModule im = _context.InstituteToModules.FirstOrDefault( item => item.InstituteId == student.InstituteId);

                if (im == null)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误消息",
                        message = "你的学院尚未规划至考试模块,请联系系统人员！"
                    });
                }

                ApplicationForReExamination application = new ApplicationForReExamination
                {
                    StudentId = student.StudentId,
                    ModuleId = im.ModuleId,
                    InstituteId = student.InstituteId,
                    ApplicationStatus = ApplicationStatus.Submit,
                    Email = student.Email,
                    Reason = reason
                };
                 _context.ApplicationForReExaminations.Add(application);
                 _context.SaveChanges();

                return Json(new
                {
                    isOk = true,
                    title = "信息提示",
                    message = "添加成功！ 请耐心等待审核结果！"
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

        public IActionResult Log()
        {
            LoginUserModel userModel = _analysis.GetLoginUserModel(HttpContext);
            return PartialView(_context.VLogStudentMaps.Where(l => l.ID == userModel.UserId).OrderByDescending(l=>l.DoTime).ToList());
        }

        public IActionResult Course()
        {
            LoginUserModel userModel = _analysis.GetLoginUserModel(HttpContext);
            return PartialView(_context.VLearningMaps.Where(l => l.StudentId == userModel.UserId).ToList());
        }

        public IActionResult Video([Required] Int32 lId)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);

                Learing learing = _context.Learings.FirstOrDefault(l => l.LearingId == lId); 
                if (learing != null){

                    vLearningMap learningMap = _context.VLearningMaps
                            .FirstOrDefault(v => v.LearingId == lId);
                    ViewData["learning"] = learningMap;
                    List<Progress> list = _context.Progresses
                            .Where(p => p.StudentId == user.UserId)
                            .Where(p => p.Resource.CourceId == learing.CourceId)
                            .Include("Resource")
                            .OrderBy(p =>p.ProgressId)
                            .ToList();

                    List<Resource> resources = _context
                            .Resources
                            .Where(r => r.CourceId == learing.CourceId)
                            .Where(r => r.ResourceType == ResourceType.Link)
                            .ToList();
                    ViewData["links"] = resources;

                    LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.StudyCourse, $"学习视频编码:{learing.CourceId}", $"打开或刷新了课程{learningMap?.Name}页面");

                    _context.LogStudentOperations.Add(log);
                    _context.SaveChanges();

                    return View(list);
                }else{
                    return RedirectToAction("Wrong", "Error",new {data = "参数错误! 并无此学习记录"});
                }
            }
            else
            {
                return RedirectToAction("ParameterError", "Error");
            }
        }

        public IActionResult Paper()
        {
            LoginUserModel userModel = _analysis.GetLoginUserModel(HttpContext);
            return PartialView(_context.ExaminationPapers.Where(ep => ep.StudentId == userModel.UserId).ToList());
        }

        public IActionResult ReApplications()
        {
            LoginUserModel userModel = _analysis.GetLoginUserModel(HttpContext);
            return PartialView(_context.ApplicationForReExaminations.Include("Student").Where(ep => ep.StudentId == userModel.UserId).ToList());
        }

        public IActionResult Item([Required] int apId)
        {
            if (ModelState.IsValid)
            {
                vReExamApplicationMap application = _context.VReExamApplicationMaps.FirstOrDefault(v => v.ApplicationExamId == apId);

                if (application != null)
                {
                    LoginUserModel userModel = _analysis.GetLoginUserModel(HttpContext);

                    if (application.StudentId != userModel.UserId)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你无法查看其他人的申请信息！"
                        });
                    }

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
                        message = "申请记录不存在,或者已经被删除!"
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

        public IActionResult Person()
        {
            LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
            Student student = _context.Student.Find(model.UserId);

            return View(new StudentViewModel(student, _context));
        }

        public IActionResult Operations()
        {
            LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
            return PartialView(_context.LogStudentOperations.Where(log => log.StudentId == model.UserId));
        }

        public IActionResult Progress()
        {
            LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
            return PartialView(_context.Progresses.Include("Resource").Where(pro => pro.StudentId == model.UserId));
        }

        //删除未审核申请
        [HttpPost]
        public IActionResult DeleteApp([Required] int apId)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
                ApplicationForReExamination application = _context.ApplicationForReExaminations.Find(apId);

                LogStudentOperation operation = _logger.GetDefaultLogStudentOperation(
                    StuOperationCode.DeleteReExamApplication, $"查询编码:{application?.ApplicationExamId}", $"删除重考申请 重考申请时间{application?.AddTime}");
                operation.Title = "删除重考申请";
                if (application != null)
                {
                    if (application.ApplicationStatus != ApplicationStatus.Submit)
                    {
                        _logger.Logger(operation);
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "申请已经被审核学生无权限无法删除！"
                        });
                    }

                    if (application.StudentId != model.UserId)
                    {
                        _logger.Logger(operation);
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你无法删除其他人的申请信息!"
                        });
                    }

                    operation.StuOperationStatus = StuOperationStatus.Success;
                    _context.ApplicationForReExaminations.Remove(application);
                    _context.LogStudentOperations.Add(operation);
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
                    _logger.Logger(operation);
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "申请记录不存在,或者已经被删除"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        //修改学生邮件
        [HttpPost]
        public IActionResult Email([Required,EmailAddress] String email)
        {
            if (ModelState.IsValid)
            {
                
                LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
                Student student = _context.Student.FirstOrDefault(s => s.StudentId == model.UserId);
                LogStudentOperation operation =
                    _logger.GetDefaultLogStudentOperation(StuOperationCode.ChangeEmail, $"查询编码:{student?.StudentId}",$"修改邮箱为:{email.Trim()}");
                operation.Title = "修改邮箱账号";
                if (student != null)
                {
                    student.Email = email.Trim();
                    operation.StuOperationStatus = StuOperationStatus.Success;
                    _context.LogStudentOperations.Add(operation);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "消息提示",
                        message = "邮件修改成功！"
                    });
                }
                else
                {
                    _logger.Logger(operation);
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "学生不存在或者已经被删除！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数,或者邮件格式错误"
                });
            }
        }

        //修改学生手机信息
        [HttpPost]
        public IActionResult Phone([Required,Phone] String phone)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
                Student student = _context.Student.Find(model.UserId);
                LogStudentOperation operation =
                    _logger.GetDefaultLogStudentOperation(StuOperationCode.ChangePhone, $"查询编码:{student?.StudentId}", $"修改手机为:{phone.Trim()}");
                operation.Title = "修改手机号码";
                if (student != null)
                {
                    operation.StuOperationStatus = StuOperationStatus.Success;
                    student.Phone = phone.Trim();
                    _context.LogStudentOperations.Add(operation);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "消息提示",
                        message = "邮件修改成功！"
                    });
                }
                else
                {
                    _logger.Logger(operation);
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "学生不存在或者已经被删除！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数,或者手机格式错误"
                });
            }
        }

        [HttpPost]
        public IActionResult Finish([Required] int learningId)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
                Learing learning = _context.Learings.Find(learningId);
                if (learning != null)
                {
                    if (learning.StudentId != model.UserId)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你无法操控别人的学习记录！"
                        });
                    }

                    learning.IsFinish = true;
                    LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.FinishCourseBySelf,
                        $"完成学习记录编码:{learningId}", "手动完成课程学习");
                    _context.LogStudentOperations.Add(log);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "提示",
                        message = "已经完成了！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "学习记录不存在！"
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
        public IActionResult Study([Required] int progressId)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
                Progress progress = _context.Progresses.Find(progressId);
                if (progress != null)
                {
                    if (progress.StudentId != model.UserId)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你无法操控别人的学习进度！"
                        });
                    }

                    if (progress.NeedTime <= progress.StudyTime)
                    {
                        progress.StudyTime = progress.NeedTime;

                        vProgressMap pMap = _context.VProgressMaps.FirstOrDefault(vp => vp.ProgressId == progressId);
                        if (pMap != null)
                        {
                            Learing learning = _context.Learings.FirstOrDefault(l => l.CourceId == pMap.CourceId && l.StudentId == model.UserId);
                            if (learning != null)
                            {
                                if (!learning.IsFinish)
                                {
                                    List<Progress> progresses = _context.Progresses.Where(p =>
                                            p.Resource.CourceId == learning.CourceId && p.StudentId == learning.StudentId)
                                        .ToList();
                                    Boolean isFinishAll = true;
                                    if (progresses.Count <= 1)
                                    {

                                    }
                                    else
                                    {
                                        foreach (var v in progresses)
                                        {
                                            if (v.StudyTime < v.NeedTime)
                                            {
                                                isFinishAll = false;
                                                break;
                                            }
                                        }
                                    }
                                    learning.IsFinish = isFinishAll;
                                }
                            }
                        }
                    }
                    else
                    {
                        progress.StudyTime += 2;
                    }
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "提示",
                        message = "完成记录！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "学习记录不存在！"
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
        public IActionResult Take()
        {
            LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
            vStudentMap vStudent = _context.VStudentMaps.FirstOrDefault(v => v.StudentId == model.UserId);
            if (vStudent != null)
            {
                SystemSetting setting = _config.LoadSystemSetting();
                if(!setting.ExamModuleSettings.TryGetValue(vStudent.ModuleId, out var moduleExamSetting))
                {
                    return RedirectToAction("Wrong", "Error", new { data = "无法查询你所在的规划模块,请重新登录或查询你的学院是否加入到考试规划模块中！" });
                }
                else
                {
                    ViewBag.ExamCount = _context.ExaminationPapers.Count(e => e.StudentId == model.UserId && e.IsFinish);
                    ViewBag.PassScore =  moduleExamSetting.PassFloat;
                    return View(vStudent);
                }
            }
            else
            {
                return RedirectToAction("Wrong", "Error", new {data = "无法查询你的个人信息！请重新登录或查询你的学院是否加入到考试规划模块中！"});
            }
        }
        /// <summary>
        /// 参加考试入口-
        /// </summary>
        /// <returns></returns>
        public IActionResult Exam()
        {
            LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);

            vStudentMap vStudent = _context.VStudentMaps.FirstOrDefault(v => v.StudentId == user.UserId);

            if (vStudent == null)
            {
                return RedirectToAction("Wrong", "Error", new { data = "无法查询你的个人信息！请重新登录或查询你的个人信息是否错误！" });
            }
            SystemSetting setting = _config.LoadSystemSetting(); /* 考试出题配置 和系统开发情况 ---是否允许往届考试 */

            if (!setting.ExamModuleSettings.TryGetValue(vStudent.ModuleId, out var moduleExamSetting))
            {
                //学生所在模块必须有 考试出题策略 不然不允许参加考试 或者学生所在学院没有模块所属
                return RedirectToAction("Wrong", "Error", new { data = "[错误-严重级别]:你所在学院未加入考试规划中！或考试规划未设置！ 请联系系统维护人员！" });
            }
            /* 先判断是否可以考试 ---是否是往届 */
            if (vStudent.Grade <= DateTime.Now.Year)
            {
                if (!setting.LoginSetting.AllowPastJoinExam)
                {
                    return RedirectToAction("Wrong", "Error", new { data = "未开放往届考试！" });
                }
            }
            Dictionary<Int32, ExamOpenSetting> openSettings = _config.LoadModuleExamOpenSetting(); /* 是否开发每个模块的考试 */
            //没有 模块开发配置 或者 没有开发模块
            if (!openSettings.TryGetValue(vStudent.ModuleId,out var moduleOpenSetting) || !moduleOpenSetting.IsOpen)
            {
                return RedirectToAction("Wrong", "Error", new { data = $"未开放你学院当前所在模块的考试通道！你所在学院{vStudent.InstituteName}所属模块:{vStudent.ModuleName} " });
            }
            /* 判断是否具有未完成的考试 让他继续考试 */
            ExaminationPaper paper = _context.ExaminationPapers.Include("ExamSingleChoices").Include("ExamMultipleChoices").Include("ExamJudgeChoices")
                .FirstOrDefault(p => p.StudentId == user.UserId && !p.IsFinish);

            ViewBag.Name = vStudent.StudentName;
            ViewBag.PaperCode = _handle.GetDateTimeFileName();
            ViewBag.ModuleName = vStudent.ModuleName;
            
            if (paper != null)
            {
                return View(paper);
            }

            var examCount = _context.ExaminationPapers.Count(e => e.StudentId == user.UserId && e.IsFinish);

            if (vStudent.IsPassExam)
            {
                return RedirectToAction("Wrong", "Error", new { data = "你已经通过考试了！不能再次参加考试." });
            }

            if (vStudent.MaxExamCount <= examCount)
            {
                return RedirectToAction("Wrong", "Error", new { data = "你的考试次数已经用尽了" });
            }
            /* 参加考试 开始出题 */
            

            


            return View();
        }
    }
}
