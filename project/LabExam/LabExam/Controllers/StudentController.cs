using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
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
        private IHttpContextAccessor _accessor;

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
    }
}
