using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LabExam.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ParameterError()
        {
            return View();
        }

        public IActionResult Wrong(String data)
        {
            ViewBag.ErrorInfo = data;
            return View();
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            ViewBag.Code = code;
            return View();
        }
    }
}