using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LabExam.Controllers
{
    public class StatisticsController : Controller
    {
        [HttpGet]
        public IActionResult Institute()
        {
            return View();
        }
    }
}