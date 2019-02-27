using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using LabExam.Models.EntitiyViews;
using Microsoft.EntityFrameworkCore;

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

        public StudentController(IEmailService email, LabContext context, ILoggerService logger, ILoadConfigFileService config, IHttpContextAnalysisService analysis, IEncryptionDataService encryption)
        {
            _email = email;
            _context = context;
            _logger = logger;
            _config = config;
            _analysis = analysis;
            _encryption = encryption;
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
            return PartialView(_context.VLogStudentMaps.Where(l => l.ID == userModel.UserId).ToList());
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
                if (_context.Learings.Any(l => l.LearingId == lId))
                {
                    ViewData["learning"] = _context.VLearningMaps.FirstOrDefault(v => v.LearingId == lId);
                    List<Progress> list = _context.Progresses.Where(p => p.StudentId == user.UserId).Include("Resource").ToList();
                    return View(list);
                }
                else
                {
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

        [HttpPost]
        public IActionResult DeleteApp([Required] int apId)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
                ApplicationForReExamination application = _context.ApplicationForReExaminations.Find(apId);
                if (application != null)
                {
                    if (application.ApplicationStatus != ApplicationStatus.Submit)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "申请已经被审核学生无权限无法删除！"
                        });
                    }

                    if (application.StudentId != model.UserId)
                    {
                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "你无法删除其他人的申请信息!"
                        });
                    }
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

        [HttpPost]
        public IActionResult Email([Required,EmailAddress] String email)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
                Student student = _context.Student.Find(model.UserId);
                if (student != null)
                {
                    student.Email = email.Trim();
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

        [HttpPost]
        public IActionResult Phone([Required,Phone] String phone)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel model = _analysis.GetLoginUserModel(HttpContext);
                Student student = _context.Student.Find(model.UserId);
                if (student != null)
                {
                    student.Phone = phone.Trim();
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
    }
}
