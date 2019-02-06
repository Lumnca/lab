using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using Microsoft.AspNetCore.Mvc;

namespace LabExam.Controllers
{
    public class ProfessionController : Controller
    {

        private readonly LabContext _context;

        public IActionResult Index()
        {
            var list = _context.Professions.ToList();
            return View(list);
        }
    }
}