using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabExam.Controllers
{

    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Json(new
            {
                IsLogin = true,
                UserId = "2016110418",
                Password = "AB46845ASD8945D34AS8D45A1WD8465AS1D5AS4DA8S5D",
                Name = "蒋星"
            });
        }
    }
}