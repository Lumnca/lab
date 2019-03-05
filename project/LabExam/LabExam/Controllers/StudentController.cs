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
using System.Runtime.InteropServices.ComTypes;
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
                    log.StuOperationStatus = StuOperationStatus.Success;
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
            return PartialView(_context.LogStudentOperations.Where(log => log.StudentId == model.UserId).OrderByDescending(l => l.AddTime));
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
                operation.StuOperationStatus = StuOperationStatus.Success;
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
        /// 参加考试入口
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Exam()
        {
            LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);

            vStudentMap vStudent = _context.VStudentMaps.FirstOrDefault(v => v.StudentId == user.UserId);

            if (vStudent == null)
            {
                return RedirectToAction("Wrong", "Error", new { data = "无法查询你的个人信息！请重新登录或查询你的个人信息是否错误！" });
            }
            //考试出题配置 和系统开发情况 ---是否允许往届考试 
            SystemSetting setting = _config.LoadSystemSetting(); 

            if (!setting.ExamModuleSettings.TryGetValue(vStudent.ModuleId, out var moduleExamSetting))
            {
                //学生所在模块必须有 考试出题策略 不然不允许参加考试 或者学生所在学院没有模块所属
                return RedirectToAction("Wrong", "Error", new { data = "[错误-严重级别]:你所在学院未加入考试规划中！或考试规划未设置！ 请联系系统维护人员！" });
            }
            /* 判断是否可以考试 ---是否是往届 */

            if (!setting.LoginSetting.IsOpenExam)
            {
                return RedirectToAction("Wrong", "Error", new { data = "考试通道为关闭状态！" });
            }

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
            ExaminationPaper paper = 
                _context.ExaminationPapers.FirstOrDefault(p => p.StudentId == user.UserId && !p.IsFinish);

            ViewBag.Name = vStudent.StudentName;
            ViewBag.PaperCode = _handle.GetDateTimeFileName();
            ViewBag.ModuleName = vStudent.ModuleName;
            
            if (paper != null)
            {

                if (paper.ExamJudgeChoices == null)
                {
                    paper.ExamJudgeChoices = _context.ExamJudgeChoices.Where(j => j.PaperId == paper.PaperId).OrderBy(j => j.JudgeId).ToList();
                }
                if (paper.ExamMultipleChoices == null)
                {
                    paper.ExamMultipleChoices = _context.ExamMultipleChoices.Where(m => m.PaperId == paper.PaperId).OrderBy(m => m.MultipleId).ToList();
                }
                if (paper.ExamSingleChoices == null)
                {
                    paper.ExamSingleChoices = _context.ExamSingleChoices.Where(s => s.PaperId == paper.PaperId).OrderBy(s => s.SingleId).ToList();
                }

                HttpContext.Session.SetInt32("examiningPaper",paper.PaperId);

                LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.EnterTheExamAgain,$"试卷编号:{paper.PaperId}", "再次进入考试完成未完成试卷 或刷新页面！");
                log.Title = "刷新页面或者完成未完成的试卷";
                log.StuOperationStatus = StuOperationStatus.Success;
                _context.LogStudentOperations.Add(log);
                _context.SaveChanges();


                return View(paper);
            }

            if (_context.Progresses.Any(p => p.StudyTime < p.NeedTime && p.StudentId == vStudent.StudentId))
            {
                return RedirectToAction("Wrong", "Error", new { data = "貌似你有视频没有看完哟！ 请到个人中心的视频学习查看一下,完成学习任务." });
            }



            var examCount = _context.ExaminationPapers
                .Count(e => e.StudentId == user.UserId && e.IsFinish);

            if (vStudent.IsPassExam)
            {
                return RedirectToAction("Wrong", "Error", new { data = "你已经通过考试了！不能再次参加考试." });
            }

            if (vStudent.MaxExamCount <= examCount)
            {
                return RedirectToAction("Wrong", "Error", new { data = "你的考试次数已经用尽了" });
            }

            _context.Database.BeginTransaction();
            try
            {
                /* 参加考试 开始出题 */
                ExaminationPaper newPaper = new ExaminationPaper
                {
                    StudentId = vStudent.StudentId,
                    AddTime = DateTime.Now,
                    IsFinish = false,
                    Score = 0,
                    ExamTime = moduleExamSetting.ExamTime,
                    TotleScore = moduleExamSetting.TotalScore,
                    LeaveExamTime = moduleExamSetting.ExamTime,
                    PassScore = moduleExamSetting.PassFloat
                };

                _context.ExaminationPapers.Add(newPaper);
                _context.SaveChanges();

                List<vRandomJudgeMap> judgeMaps = await _context.VRandomJudgeMaps.Where(v => v.ModuleId == vStudent.ModuleId).OrderBy(v => v.RandomId)
                    .Take(moduleExamSetting.Judge.Count).ToListAsync();
                List<vRandomMultipleMap> multipleMaps = await _context.VRandomMultipleMaps.Where(v => v.ModuleId == vStudent.ModuleId).OrderBy(v => v.RandomId)
                    .Take(moduleExamSetting.Multiple.Count).ToListAsync();
                List<vRandomSingleMap> singleMaps = await _context.VRandomSingleMaps.Where(v => v.ModuleId == vStudent.ModuleId).OrderBy(v => v.RandomId)
                    .Take(moduleExamSetting.Single.Count).ToListAsync();

                foreach (var judge in judgeMaps)
                {
                    _context.ExamJudgeChoices.Add(new ExamJudgeChoices
                    {
                        StudentId = vStudent.StudentId,
                        Score = moduleExamSetting.Judge.Score,
                        PaperId = newPaper.PaperId,
                        RealAnswer = _context.JudgeChoices.Find(judge.JudgeId)?.Answer,
                        JudgeId = judge.JudgeId
                    });
                }

                foreach (var single in singleMaps)
                {
                    _context.ExamSingleChoices.Add(new ExamSingleChoices
                    {
                        StudentId = vStudent.StudentId,
                        Score = moduleExamSetting.Single.Score,
                        PaperId = newPaper.PaperId,
                        SingleId = single.SingleId,
                        RealAnswer = _context.SingleChoices.Find(single.SingleId)?.Answer
                    });
                }

                foreach (var multiple in multipleMaps)
                {
                    _context.ExamMultipleChoices.Add(new ExamMultipleChoices
                    {
                        StudentId = vStudent.StudentId,
                        Score = moduleExamSetting.Multiple.Score,
                        PaperId = newPaper.PaperId,
                        MultipleId = multiple.MultipleId,
                        RealAnswer = _context.MultipleChoices.Find(multiple.MultipleId)?.Answer
                    });
                }
                LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.EnterTheExamAgain, $"试卷编号:{newPaper.PaperId}", "参加考试");
                log.Title = "参加了一次新的考试";
                log.StuOperationStatus = StuOperationStatus.Success;
                _context.LogStudentOperations.Add(log);

                _context.SaveChanges();

                if (newPaper.ExamJudgeChoices == null)
                {
                    newPaper.ExamJudgeChoices = _context.ExamJudgeChoices.Where(j => j.PaperId == newPaper.PaperId).OrderBy(j => j.JudgeId).ToList();
                }
                if (newPaper.ExamMultipleChoices == null)
                {
                    newPaper.ExamMultipleChoices = _context.ExamMultipleChoices.Where(m => m.PaperId == newPaper.PaperId).OrderBy(m => m.MultipleId).ToList();
                }
                if (newPaper.ExamSingleChoices == null)
                {
                    newPaper.ExamSingleChoices = _context.ExamSingleChoices.Where(s => s.PaperId == newPaper.PaperId).OrderBy(s => s.SingleId).ToList();
                }
                _context.Database.CommitTransaction();
                HttpContext.Session.SetInt32("examiningPaper", newPaper.PaperId);
                return View(newPaper);
            }
            catch (Exception e)
            {
                _context.Database.RollbackTransaction();
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public IActionResult LoadJudge([Required] int eJid)
        {
            //请求参数是否合法
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExamJudgeChoices eJudge = _context.ExamJudgeChoices.Find(eJid);
                //看一下 考试记录ID 是否存在
                if (eJudge != null)
                {
                    //查询这个考试记录是否属于这个考生 不是那么记录非法获取题目到日志中
                    if (eJudge.StudentId != user.UserId)
                    {
                        LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.IllegalAttack,
                            "非法攻击", "对系统进行了非法攻击获取题目内容！已拦截");
                        log.Title = "非法请求题目";
                        _context.LogStudentOperations.Add(log);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你请求了不属于你的题目！这并不合法！ 我们将会记录你的请求IP 在线学号！对于所做的非法行为提出警告！"
                        });
                    }
                    //根据考试记录查询 题目信息
                    JudgeChoices choices = _context.JudgeChoices.Find(eJudge.JudgeId);
                    //是否题目信息存在
                    if (choices != null)
                    {
                        //考试信息存在 那么抹掉答案 传递给前端
                        choices.Answer = "";
                        return Json(new
                        {
                            isOk = true,
                            title = "消息",
                            message = "加载成功",
                            examId = eJid,
                            data = choices,
                            stuAnswer = eJudge.StudentAnswer
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你请求的考题已经被删除了！你可以选择退出当前考试,重新参考！"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你请求了错误的或不属于你的考试题目！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误！"
                });
            }
        }

        /// <summary>
        ///  更具考试试卷 的出题记录ID 来请求 题目中的单选题
        /// </summary>
        /// <param name="eSid">单选题出题记录ID</param>
        /// <returns>Json 信息</returns>
        [HttpPost]
        public IActionResult LoadSingle([Required] int eSid)
        {
            //请求参数是否合法
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExamSingleChoices eSingle = _context.ExamSingleChoices.Find(eSid);
                //看一下 考试记录ID 是否存在
                if (eSingle != null) 
                {
                    //查询这个考试记录是否属于这个考生
                    if (eSingle.StudentId != user.UserId)
                    {
                        LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.IllegalAttack,
                            "非法攻击", "对系统进行了非法攻击获取题目内容！已拦截");
                        log.Title = "非法请求题目";
                        _context.LogStudentOperations.Add(log);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你请求了不属于你的题目！这并不合法！ 我们将会记录你的请求IP 在线学号！对于所做的非法行为提出警告！"
                        });
                    }
                    //根据考试记录查询 题目信息
                    SingleChoices choices = _context.SingleChoices.Find(eSingle.SingleId);
                    //是否题目信息存在
                    if (choices != null)
                    {
                        //考试信息存在 那么抹掉答案 传递给前端
                        choices.Answer = "";
                        return Json(new
                        {
                            isOk = true,
                            title = "消息",
                            message = "加载成功",
                            examId = eSid,
                            data = choices,
                            stuAnswer = eSingle.StudentAnswer

                        });
                    }
                    else
                    {
                        //已经被删除
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你请求的考题已经被删除了！你可以选择退出当前考试,重新参考！"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你请求了错误的或不属于你的考试题目！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误！"
                });
            }
        }
     
        /// <summary>
        ///  加载多选题
        /// </summary>
        /// <param name="eMid"></param>
        /// <returns></returns>
        public IActionResult LoadMultiple([Required] int eMid)
        {
            //请求参数是否合法
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExamMultipleChoices eMultiple = _context.ExamMultipleChoices.Find(eMid);
                //看一下 考试记录ID 是否存在
                if (eMultiple != null)
                {
                    //查询这个考试记录是否属于这个考生
                    if (eMultiple.StudentId != user.UserId)
                    {
                        LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.IllegalAttack,
                            "非法攻击", "对系统进行了非法攻击获取题目内容！已拦截");
                        log.Title = "非法请求题目";
                        _context.LogStudentOperations.Add(log);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你请求了不属于你的题目！这并不合法！ 我们将会记录你的请求IP 在线学号！对于所做的非法行为提出警告！"
                        });
                    }
                    //根据考试记录查询 题目信息
                    MultipleChoices choices = _context.MultipleChoices.Find(eMultiple.MultipleId);
                    //是否题目信息存在
                    if (choices != null)
                    {
                        //考试信息存在 那么抹掉答案 传递给前端
                        choices.Answer = "";
                        return Json(new
                        {
                            isOk = true,
                            title = "消息",
                            message = "加载成功",
                            examId = eMid,
                            data = choices,
                            stuAnswer = eMultiple.StudentAnswer
                        });
                    }
                    else
                    {
                        //已经被删除
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你请求的考题已经被删除了！你可以选择退出当前考试,重新参考！"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你请求了错误的或不属于你的考试题目！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误！"
                });
            }
        }

        public IActionResult Review([Required,MaxLength(300)] String review,[Required] int pId) 
        {
            if(ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExaminationPaper paper = _context.ExaminationPapers.Find(pId);
                if (paper != null)
                {
                    if (paper.StudentId != user.UserId)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你无法回答其他人的题目！"
                        });
                    }

                    paper.Review = review.Trim();
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "提示",
                        message = "回答成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "试卷为空！ 无法记录答案"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误！回答不要超过两百字！感谢"
                });
            }
        }

        public IActionResult SubmitSingle([Required] int examId,[Required] String answer)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExamSingleChoices choices = _context.ExamSingleChoices.Find(examId);
                if (choices!=null)
                {
                    if (choices.StudentId == user.UserId)
                    {
                        if (_context.ExaminationPapers.Any(p =>  !p.IsFinish && p.PaperId == choices.PaperId))
                        {
                            Char[] checkAnswer = answer.ToUpper().Trim().ToCharArray();//重新排序
                            choices.StudentAnswer = String.Join("", checkAnswer);
                            _context.SaveChanges();
                            return Json(new
                            {
                                isOk = true,
                                title = "提示",
                                message = "回答成功！"
                            });
                        }
                        else
                        {
                            return Json(new
                            {
                                isOk = false,
                                title = "错误提示",
                                message = "未找到此考题所属的试卷！或此试卷已经被提交！"
                            });
                        }
                    }
                    else
                    {
                        LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.IllegalAttack,
                            "非法攻击", "回答了并非属于你的考试题目");
                        log.Title = "非法回答题目";
                        _context.LogStudentOperations.Add(log);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你回答了并非属于你的考试题目, 我们将会记录你的请求IP 在线学号！对于所做的非法行为提出警告！"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你请求的考题已经被删除了！你可以选择退出当前考试,重新参考！"
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

        public IActionResult SubmitMultiple([Required] int examId, [Required] String answer)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExamMultipleChoices choices = _context.ExamMultipleChoices.Find(examId);
                if (choices != null)
                {
                    if (choices.StudentId == user.UserId)
                    {
                        if (_context.ExaminationPapers.Any(p => !p.IsFinish && p.PaperId == choices.PaperId))
                        {
                            Char[] checkAnswer = answer.ToUpper().Trim().ToCharArray();//重新排序
                            choices.StudentAnswer = String.Join("", checkAnswer);
                            _context.SaveChanges();
                            return Json(new
                            {
                                isOk = true,
                                title = "提示",
                                message = "回答成功！"
                            });
                        }
                        else
                        {
                            return Json(new
                            {
                                isOk = false,
                                title = "错误提示",
                                message = "未找到此考题所属的试卷！或此试卷已经被提交！"
                            });
                        }
                    }
                    else
                    {
                        LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.IllegalAttack,
                            "非法攻击", "回答了并非属于你的考试题目");
                        log.Title = "非法回答题目";
                        _context.LogStudentOperations.Add(log);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你回答了并非属于你的考试题目, 我们将会记录你的请求IP 在线学号！对于所做的非法行为提出警告！"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你请求的考题已经被删除了！你可以选择退出当前考试,重新参考！"
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

        public IActionResult SubmitJudge([Required] int examId, [Required] String answer)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExamJudgeChoices choices = _context.ExamJudgeChoices.Find(examId);
                if (choices != null)
                {
                    if (choices.StudentId == user.UserId)
                    {
                        if (_context.ExaminationPapers.Any(p => !p.IsFinish && p.PaperId == choices.PaperId))
                        {
                            Char[] checkAnswer = answer.ToUpper().Trim().ToCharArray();//重新排序
                            choices.StudentAnswer = String.Join("", checkAnswer);
                            _context.SaveChanges();
                            return Json(new
                            {
                                isOk = true,
                                title = "提示",
                                message = "回答成功！"
                            });
                        }
                        else
                        {
                            return Json(new
                            {
                                isOk = false,
                                title = "错误提示",
                                message = "未找到此考题所属的试卷！或此试卷已经被提交！"
                            });
                        }
                    }
                    else
                    {
                        LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.IllegalAttack,
                            "非法攻击", "回答了并非属于你的考试题目");
                        log.Title = "非法回答题目";
                        _context.LogStudentOperations.Add(log);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你回答了并非属于你的考试题目, 我们将会记录你的请求IP 在线学号！对于所做的非法行为提出警告！"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你请求的考题已经被删除了！你可以选择退出当前考试,重新参考！"
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
        
        public IActionResult Timer([Required] int pId,[FromServices] IDataBaseService dbService)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExaminationPaper paper = _context.ExaminationPapers.Find(pId);
                if (paper == null || paper.StudentId != user.UserId)
                {
                    /* 从 Session 里面取得 */
                    LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.DisruptingExamination, "无法判断正常试卷ID","企图误导考试结束时间!");
                    log.Title = "干扰考试,企图作弊";

                    /* 从 Session 里面取得 */
                    int? paperId = HttpContext.Session.GetInt32("examiningPaper");

                    if (paperId.HasValue)
                    {
                        paper = _context.ExaminationPapers.Find(paperId.Value);
                        if (paper != null)
                        {
                            if (!paper.IsFinish && paper.StudentId == user.UserId)
                            {
                                if (paper.LeaveExamTime <= 1.0)
                                {
                                    dbService.FinishPaper(paper, _context);
                                    HttpContext.Session.Remove("examiningPaper");
                                    LogStudentOperation operation =
                                        _logger.GetDefaultLogStudentOperation(StuOperationCode.SubmitExamBySystem,
                                            $"提交试卷:${paper.PaperId}", "考试时间到了！系统帮你提交了考试");
                                    operation.StuOperationStatus = StuOperationStatus.Success;
                                    operation.Title = "系统提交了试卷";
                                    _context.LogStudentOperations.Add(operation);
                                }
                                else
                                {
                                    paper.LeaveExamTime--;
                                }
                            }
                        }
                    }
                    _context.LogStudentOperations.Add(log);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = false,
                        title = "提醒！",
                        isPaperIdError = true,
                        message = "当前试卷并不属于你！你企图非法误导考试结束时间！我们将记录你的非法行为！并启动 Session 活动 正常化你的考试时间！"
                    });
                }

                if (!paper.IsFinish)
                {
                    if (paper.LeaveExamTime <= 1.0)
                    {
                        dbService.FinishPaper(paper, _context);
                        HttpContext.Session.Remove("examiningPaper");
                        dbService.FinishPaper(paper, _context);
                        HttpContext.Session.Remove("examiningPaper");
                        LogStudentOperation operation =
                            _logger.GetDefaultLogStudentOperation(StuOperationCode.SubmitExamBySystem,
                                $"提交试卷:${paper.PaperId}", "考试时间到了！系统帮你提交了考试");
                        operation.StuOperationStatus = StuOperationStatus.Success;
                        operation.Title = "系统提交了试卷";
                        _context.LogStudentOperations.Add(operation);
                    }
                    else
                    {
                        paper.LeaveExamTime--;
                    }

                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "成功",
                        message = "计时成功！",
                        isFinish = paper.IsFinish,
                        leftTime = paper.LeaveExamTime,
                        isPaperIdError = false
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "考试已经结束！"
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

        public IActionResult Result([Required] int pId)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExaminationPaper paper = _context.ExaminationPapers.Find(pId);
                if (paper == null)
                {
                    return RedirectToAction("ParameterError", "Error");
                }
                else
                {
                    if (paper.StudentId != user.UserId)
                    {
                        RedirectToAction("Wrong", "Error", new { data = "你不能查看别人的试卷！" });
                    }

                    vStatisticJudgeMap judge =
                        _context.VStatisticJudgeMaps.FirstOrDefault(j => j.PaperId == paper.PaperId);
                    vStatisticMultipleMap multiple =
                        _context.VStatisticMultipleMaps.FirstOrDefault(m => m.PaperId == paper.PaperId);
                    vStatisticSingleMap single =
                        _context.VStatisticSingleMaps.FirstOrDefault(m => m.PaperId == paper.PaperId);

                    ViewBag.Name = _context.Student.Find(paper.StudentId)?.Name;

                    ViewData["judge"] = judge;
                    ViewData["multiple"] = multiple;
                    ViewData["single"] = single;
                    return View(paper);
                }
            }
            else
            {
                return RedirectToAction("ParameterError", "Error");
            }
        }

        public IActionResult SubmitPaper([Required] int pId, [FromServices] IDataBaseService dbService)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExaminationPaper paper = _context.ExaminationPapers.Find(pId);
                if (paper != null)
                {
                    if (paper.StudentId != user.UserId)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你不能提交别人的试卷！"
                        });
                    }

                    if (paper.IsFinish)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "试卷已经完成！无法提交。"
                        });
                    }

                    dbService.FinishPaper(paper,_context);
                    LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.SubmitExam,
                        $"试卷ID{paper.PaperId}:{paper.PaperId}", "你提前交了试卷,结束了考试");
                    log.Title = "结束考试";
                    log.StuOperationStatus = StuOperationStatus.Success;
                    _context.LogStudentOperations.Add(log);
                    _context.SaveChanges();
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "提示",
                        message = "提交成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "试卷为空！ "
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误！"
                });
            }
        }

        public IActionResult Exit([Required] int pId, [FromServices] IDataBaseService dbService)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                ExaminationPaper paper = _context.ExaminationPapers.Find(pId);
                if (paper != null)
                {
                    if (paper.StudentId != user.UserId)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你无法删除其他人的试卷！"
                        });
                    }

                    if (paper.IsFinish)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "试卷已经完成！无法删除。"
                        });
                    }

                    dbService.DeletePaper(paper, _context);
                    LogStudentOperation log = _logger.GetDefaultLogStudentOperation(StuOperationCode.GiveUpTheExam,
                        $"删除试卷ID:{paper.PaperId}", "你放弃了你考试！删除了考试信息");
                    log.Title = "放弃考试";
                    log.StuOperationStatus = StuOperationStatus.Success;
                    _context.LogStudentOperations.Add(log);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "提示",
                        message = "操作成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "试卷为空！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误！"
                });
            }
        }
    }
}
