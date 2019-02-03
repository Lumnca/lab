using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LabExam.IServices;
using Microsoft.AspNetCore.Mvc;
using LabExam.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LabExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpRequstAnalysisService _analysis;

        public HomeController(IHttpRequstAnalysisService analysis)
        {
            _analysis = analysis;
        }

        [Route("/")]
        [Route("/Index")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Mobile/Home/Index")]
        public IActionResult MobileIndex()
        {
            return View(@"~/Views/Home/Index.Mobile.cshtml");
        }

        [HttpGet]
        public IActionResult Header()
        {
            var headerDictionary = HttpContext.Request.Headers;
            String val = "";
            foreach (var Pair in headerDictionary)
            {
                val  += $"{Pair.Key}: {Pair.Value} \n";
            }
            return Content(val);
        }

        public IActionResult Announcement()
        {
            return View();
        }

        [Route("/Mobile/Home/Announcement")]
        public IActionResult MobileAnnouncement()
        {
            return View(@"~/Views/Home/Announcement.Mobile.cshtml");
        }

        public IActionResult Help()
        {
            return View();
        }


        [Route("/Mobile/Home/Help")]
        public IActionResult MobileHelp()
        {
            return View(@"~/Views/Home/Help.Mobile.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
