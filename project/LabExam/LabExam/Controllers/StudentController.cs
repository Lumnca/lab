using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.JsonModel;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    public class StudentController : Controller
    {
        private readonly IEncryptionDataService _ncryption;
        private readonly LabContext _context;
        private readonly IHttpContextAccessor _accessor;

        public StudentController(IEncryptionDataService ncryption, LabContext context, IHttpContextAccessor accessor)
        {
            _ncryption = ncryption;
            _context = context;
            _accessor = accessor;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create([Bind(include: "StudentId,IDNumber,InstituteId,Name,Profession,BirthDate,Sex,StudentType,Grade,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (_context.Student.Any(val => val.StudentId == student.StudentId))
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误",
                        message = "此学号的学生已经存在！"
                    });
                }
                else
                {
                    student.IsPassExam = false;
                    student.MaxExamCount = 2;
                    student.MaxExamScore = 0;


                    return Json(new
                    {
                        isOk = true,
                        title = "温馨提示",
                        message = "添加成功！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误",
                    message = "参数错误!,传入了错误的信息！"
                });
            }
        }


        public IActionResult Setting()
        {
            SystemSetting setting = new SystemSetting();
            Dictionary<int,ModuleExamSetting> dictionary = new Dictionary<int, ModuleExamSetting>();

            ModuleExamSetting moduleOne = new ModuleExamSetting
            {
                ModuleId = 1,
                ExamTime = 100,
                ModuleName = "其他理工类",
                Multiple = new Multiple() {Count = 20, Score = 2},
                Single = new LabExam.Models.JsonModel.Single() {Count = 30, Score = 1},
                Judge = new Judge() {Count = 20, Score = 1},
                PassFloat = 60,
                TotalScore = 100,
                AllowExamTime = 3
            };
            ModuleExamSetting moduleTwo = new ModuleExamSetting
            {
                ModuleId = 2,
                ExamTime = 100,
                ModuleName = "化学生物类",
                Multiple = new Multiple() {Count = 20, Score = 2},
                Single = new LabExam.Models.JsonModel.Single() {Count = 30, Score = 1},
                Judge = new Judge() {Count = 20, Score = 1},
                PassFloat = 60,
                TotalScore = 100,
                AllowExamTime = 3
            };
            dictionary.Add(moduleOne.ModuleId, moduleOne);
            dictionary.Add(moduleTwo.ModuleId, moduleTwo);

            setting.ExamModuleSettings = dictionary;

            setting.Version = "2019.2.00.00.12";
            setting.LoginSetting = new LoginSetting()
            {
                AllowPastJoinExam = true,
                IsOpenExam = true,
                PrincipalLogin = true,
                StudentLogim = true
            };

            List<MaintenanceStaff> staff = new List<MaintenanceStaff>
            {
                new MaintenanceStaff() {Name = "蒋星", Phone = "15982690985", QQ = "1427035242"},
                new MaintenanceStaff() {Name = "刘闽川", Phone = "17721953180", QQ = "1944731504"}
            };


            setting.Staffs = staff;
            return Json(setting);
        }

    }
}
