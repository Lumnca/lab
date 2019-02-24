using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
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

    
    }
}
