using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabExam.Controllers
{
    public class ProfessionController : Controller
    {

        private readonly LabContext _context;

        public ProfessionController(LabContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int pageSize = 10;
            int dataCount = _context.Professions.Count();
            int pageCount = dataCount / pageSize;
            int lastCount = dataCount % pageSize;
            if (lastCount > 0)
            {
                pageCount++;
            }
            
            ViewBag.lineCount = dataCount; //总共有多少个专业
            ViewBag.pageCount = pageCount; //多少页

            var list = _context.VProfessionMaps.OrderBy(pro => pro.ProfessionId)
                .Take(pageSize).ToList();
            return View(list);
        }

        public IActionResult List()
        {
            var list = _context.VProfessionMaps.Select(pro => new
            {
                id = pro.ProfessionId,
                name = pro.Name,
                instituteName = pro.InstituteName,
                instituteId = pro.InstituteId,
                type = pro.ProfessionType == ProfessionType.PostGraduate? "研究生专业" : "本科生专业"
            });
            return Json(new
            {
                professions = list
            });
        }

        public IActionResult Update([Required] int professionId, [Required] String name, [Required] int instituteId, ProfessionType type)
        {
            if (ModelState.IsValid && name.Length > 0)
            {
                if (!_context.Institute.Any(ins => ins.InstituteId == instituteId))
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "不存在此学院！"
                    });
                }

                Profession pro = _context.Professions.FirstOrDefault(p => p.ProfessionId == professionId);
                if (pro != null)
                {
                    pro.InstituteId = instituteId;
                    pro.Name = name;
                    pro.ProfessionType = type;
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        info = "修改成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "专业已经不存在,或者已被删除！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = "参数错误! 传递了错误的参数！"
                });
            }
        }

        public IActionResult Page([Required] int pageIndex, [Required] String name, [Required] int instituteId)
        {
            if (ModelState.IsValid && pageIndex > 0 )
            {
                String sql = "select * from ProfessionView where ProfessionId > 0";
                if (name != "")
                {
                    sql += $" and InstituteName like '%{name}%'";
                }

                if (instituteId > 0)
                {
                    sql += $" and InstituteId = {instituteId}";
                }

                int pageSize = 10;
                int dataCount = _context.VProfessionMaps.FromSql(sql: sql).Count();
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

                var listResultMaps = _context.VProfessionMaps.FromSql(sql: sql).OrderBy(pro => pro.ProfessionId)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(pro => new
                    {
                        id = pro.ProfessionId,
                        name = pro.Name,
                        instituteName = pro.InstituteName,
                        instituteId = pro.InstituteId,
                        type = pro.ProfessionType == ProfessionType.PostGraduate ? "研究生专业" : "本科生专业"
                    }).ToList();


                return Json(new
                {
                    isOk = true,
                    lineCount = dataCount,
                    pageCount = pageCount,//总共是多少页
                    pageNowIndex = pageIndex, //当前是第几页
                    Professions = listResultMaps
                });

            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        public IActionResult Delete([Required] int professionId)
        {
            if (ModelState.IsValid)
            {
                Profession pro = _context.Professions.FirstOrDefault(p => p.ProfessionId == professionId);
                if (pro != null)
                {
                    _context.Professions.Remove(pro);
                    _context.SaveChanges();
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
                        error = "专业已经不存在,或者已被删除！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = "参数错误! 传递了错误的参数！"
                });
            }
        }

        public IActionResult Create([Required] String name,[Required] int instituteId,ProfessionType type) 
        {
            if (ModelState.IsValid)
            {
                if (_context.Institute.Any(val => val.InstituteId == instituteId))
                {
                    if (_context.Professions.Any(pro => pro.Name.Equals(name)))
                    {
                        return Json(new
                        {
                            isOk = false,
                            error = "此专业已经存在,名称重复"
                        });
                    }
                    else
                    {
                        Profession profession = new Profession();
                        profession.Name = name;
                        profession.InstituteId = instituteId;
                        profession.ProfessionType = type;
                        _context.Professions.Add(profession);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = true,
                            error = "添加成功"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "所属学院不存在"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = "参数错误! 传递了错误的参数！"
                });
            }
        }

    }
}