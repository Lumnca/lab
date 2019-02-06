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

        public IActionResult Delete([Required] int moduleId)
        {
            if (ModelState.IsValid)
            {
                if (!_context.InstituteToModules.Any(im => im.ModuleId == moduleId))
                {
                    Module module = _context.Modules.FirstOrDefault(m => m.ModuleId == moduleId);
                    if (module != null)
                    {
                        _context.Remove(module); //删除这个模块
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
                            Error = "没有这个模块"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        Error = "没有这个模块"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = "参数错误"
                });
            }
        }

        public IActionResult ReName([Required] int moduleId, [Required] String newName)
        {
            if (ModelState.IsValid)
            {
                Module module = _context.Modules.Find(moduleId);
                if (module != null)
                {
                    module.Name = newName;
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
                        error = "没有这个模块"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = "参数错误"
                });
            }
        } 

        public IActionResult ModuleInfoById([Required] int moduleId)
        {
            if (ModelState.IsValid)
            {
                Module m = _context.Modules.FirstOrDefault(one => one.ModuleId == moduleId);
                if (m != null)
                {
                    var list = _context.VInstituteToModuleMaps.Where(v => v.ModuleId == moduleId).ToList();

                    return Json(new
                    {
                        isOk = true,
                        module = m,
                        data = list
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "不存在此模块,或此模块已经被删除了"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = "参数错误"
                });
            }
        }

        public IActionResult DeleteInstitute([Required] int moduleId, [Required] int instituteId)
        {
            if (ModelState.IsValid)
            {
                InstituteToModule im = _context.InstituteToModules.FirstOrDefault(val =>
                    val.InstituteId == instituteId && val.ModuleId == moduleId);

                if (im != null)
                {
                    _context.InstituteToModules.Remove(im);
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
                        error = "没有此条记录,记录着此学院属于此模块"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = "参数错误"
                });
            }
            
        }

        public IActionResult AddInstitute([Required] int moduleId,[Required] int instituteId)
        {
            if (ModelState.IsValid)
            {
                if (_context.Modules.Any(val => val.ModuleId == moduleId) && _context.Institute.Any(ins => ins.InstituteId == instituteId))
                {
                    if (_context.InstituteToModules.Any(one =>
                        one.InstituteId == instituteId))
                    {
                        return Json(new
                        {
                            isOk = false,
                            error = "学院有归属了！ 一个学院不能属于两个模块"
                        });
                    }
                    else
                    {
                        InstituteToModule im = new InstituteToModule();
                        im.InstituteId = instituteId;
                        im.ModuleId = moduleId;
                        _context.InstituteToModules.Add(im);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = true,
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "模块或者学院不存在！ 你不要搞我涩！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = "参数错误"
                });
            }
        }
    }
}
