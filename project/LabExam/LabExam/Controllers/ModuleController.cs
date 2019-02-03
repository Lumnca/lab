using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LabExam.DataSource;
using LabExam.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LabExam.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class ModuleController : Controller
    {
        private readonly LabContext _context;

        public ModuleController(LabContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Module> modules = _context.Modules.ToList<Module>();
            return View(modules);
        }

        public IActionResult Encode([Required] String data)
        {
            if (ModelState.IsValid)
            {
                EncryptionDataService service = new EncryptionDataService(); ;
                String result = service.EncodeByMd5(data);
                return Content(service.EncodeByMd5(result));
            }
            else
            {
                return Content("参数错误");
            }
        }

        public IActionResult List()
        {
            List<Module> modules = _context.Modules.ToList<Module>();
            return Json(modules);
        }

        [HttpPost]
        public IActionResult Create([Required] String name)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_context.Modules.FirstOrDefault(m => m.Name.Equals(name)) == null)
                    {
                        Module module = new Module();
                        module.AddTime = DateTime.Now;
                        module.Name = name;
                        module.PrincipalId = "20020059";
                        _context.Modules.Add(module);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = true
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            isOk = false,
                            error = "模块名重复,此模块已经存在"
                        });
                    }
                }
                else
                {
                    return RedirectToAction("ParameterError", "Error");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}
