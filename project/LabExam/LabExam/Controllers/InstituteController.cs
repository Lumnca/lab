using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LabExam.DataSource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Entities;
using LabExam.Models.EntitiyViews;
using Microsoft.EntityFrameworkCore;

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class InstituteController : Controller
    {
        private readonly LabContext _context;

        public InstituteController(LabContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int pageSize = 10;
            int dataCount = _context.Institute.Count();
            int pageCount = dataCount / pageSize;
            int lastCount = dataCount % pageSize;
            if (lastCount > 0)
            {
                pageCount++;
            }
            List<vInstituteMap> list = _context.VInstituteMaps.OrderBy(ins => ins.InstituteId).Take(10).ToList();
            ViewBag.lineCount = _context.Institute.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.InstituteWithOut = _context.VInstituteWithoutModuleMaps.Count();
            ViewBag.StudentCount = _context.Student.Count();
            ViewBag.StudentCountOut = _context.Student.Where(one => one.IsPassExam == false).Count();

            return View(list);
        }

        public IActionResult List()
        {
            var list = _context.VInstituteMaps.ToList();
            return Json(list);
        }

        public async Task<IActionResult> Create([Required] String Name,[Required] int ModuleId)
        {
            if (ModelState.IsValid)
            {
                if (_context.Modules.Any(m => m.ModuleId == ModuleId))
                {
                    if (_context.Institute.Any(ins => ins.Name == Name))
                    {
                        return Json(new
                        {
                            isOk = false,
                            error = "新建学院名称重复,已经存在此学院"
                        });
                    }
                    else
                    {
                        Institute institute = new Institute();
                        institute.Name = Name;
                        _context.Institute.Add(institute);
                        await _context.SaveChangesAsync().ContinueWith(t =>
                        {
                            int result = t.Result;
                            if (result == 1)
                            {
                                InstituteToModule instituteToModule = new InstituteToModule();
                                instituteToModule.ModuleId = ModuleId;
                                instituteToModule.InstituteId = institute.InstituteId;
                                _context.Add(instituteToModule);
                                _context.SaveChangesAsync();
                            }
                        });
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
                        error = "不存在此模块！学院不可属于此模块"
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

        public IActionResult Page([Required] int pageIndex,[Required] int moduleId,[Required] int instituteId)
        {
            if (ModelState.IsValid && pageIndex > 0)
            {
                String sql = "select * from InstituteView where InstituteId > 0";
                if (moduleId > 0)
                {
                    sql += $" and ModuleId = {moduleId}";
                }

                if (instituteId > 0)
                {
                    sql += $" and InstituteId = {instituteId}";
                }

                int pageSize = 10;
                int dataCount = _context.VInstituteMaps.FromSql(sql: sql).Count();
                int pageCount = dataCount / pageSize;
                int lastCount = dataCount % pageSize;
                if (lastCount > 0)
                {
                    pageCount++;
                }

                if (pageIndex > pageCount)
                {
                    return Json(new
                    {
                        isOk = true,
                        lineCount = 0,
                        pageCount = 1,//总共是多少页
                        pageNowIndex = 1, //当前是第几页
                    });
                }

                var listResultMaps = _context.VInstituteMaps.FromSql(sql: sql).OrderBy(ins => ins.InstituteId)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();


                return Json(new
                {
                    isOk = true,
                    lineCount = dataCount,
                    pageCount = pageCount,//总共是多少页
                    pageNowIndex = pageIndex, //当前是第几页
                    institutes = listResultMaps
                });
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

        public async Task<IActionResult> Update([Required] String newName, [Required] int instituteId)
        {
            if (ModelState.IsValid && instituteId > 0)
            {
                Institute ins = _context.Institute.FirstOrDefault(one => one.InstituteId == instituteId);
                if (ins != null)
                {
                    ins.Name = newName;
                    await _context.SaveChangesAsync();
                    return Json(new
                    {
                        isOk = true,
                        info ="修改成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "参数错误 !并不存在此学院"
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

        [HttpPost]
        public async Task<IActionResult> Delete([Required] int instituteId)
        {
            if (ModelState.IsValid)
            {
                Institute ins = _context.Institute.FirstOrDefault(one => one.InstituteId == instituteId);
                if (ins != null)
                {
                    _context.Institute.Remove(ins);
                    await _context.SaveChangesAsync();
                    return Json(new
                    {
                        isOk = true,
                        info = "删除成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "学院不存在,或者已经被删除"
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

        [Route("/Institute/WithoutModule")]
        public IActionResult WithoutModule()
        {
            int Index = 1;
            var list = _context.VInstituteWithoutModuleMaps.Select(val => new
            {
                name = val.Name,
                id = val.InstituteId,
                index = Index
            });
            return Json(list);
        }

        public IActionResult EchartData()
        {
            try
            {
                List<vInstituteStudentNotPassCountMap> Institutes = _context.VInstituteStudentNotPassCountMaps.ToList();

                List<String> Names = new List<String>();
                List<Int32> Counts = new List<Int32>();

                foreach (var ins in Institutes)
                {
                    Names.Add(ins.Name);
                    Counts.Add(ins.StuCount);
                }

                return Json(new
                {
                    list = Institutes,
                    InsName = Names,
                    StuCount = Counts
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}