using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class ProfessionController : Controller
    {

        private readonly LabContext _context;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly ILoggerService _logger;

        public ProfessionController(LabContext context, IHttpContextAnalysisService analysis, ILoggerService logger)
        {
            _context = context;
            _analysis = analysis;
            _logger = logger;
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

        [AllowAnonymous]
        public IActionResult ListById([Required] int pid, ProfessionType type)
        {
            if (ModelState.IsValid)
            {
                var list = _context.VProfessionMaps.Where(p => p.InstituteId == pid && p.ProfessionType == type).Select(pro => new
                {
                    id = pro.ProfessionId,
                    name = pro.Name,
                    instituteName = pro.InstituteName,
                    instituteId = pro.InstituteId,
                    type = pro.ProfessionType == ProfessionType.PostGraduate ? "研究生专业" : "本科生专业"
                });
                return Json(new
                {
                    professions = list
                });
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

        [AllowAnonymous]
        public IActionResult ListOnlyById([Required] int pid)
        {
            if (ModelState.IsValid)
            {
                if (pid == -1)
                {
                    var plistAll = _context.VProfessionMaps.Select(pro => new
                    {
                        id = pro.ProfessionId,
                        name = $"{pro.Name}[" + (pro.ProfessionType == ProfessionType.PostGraduate ? "研究生]" : "本科]"),
                        instituteName = pro.InstituteName,
                        instituteId = pro.InstituteId,
                        type = pro.ProfessionType == ProfessionType.PostGraduate ? "研究生专业" : "本科生专业"
                    });
                    return Json(new
                    {
                        professions = plistAll
                    });
                }

                var list = _context.VProfessionMaps.Where(p => p.InstituteId == pid).Select(pro => new
                {
                    id = pro.ProfessionId,
                    name = $"{pro.Name}[" + (pro.ProfessionType == ProfessionType.PostGraduate? "研究生]" : "本科]"),
                    instituteName = pro.InstituteName,
                    instituteId = pro.InstituteId,
                    type = pro.ProfessionType == ProfessionType.PostGraduate ? "研究生专业" : "本科生专业"
                });
                return Json(new
                {
                    professions = list
                });
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

        /// <summary>
        /// 完成日志记录
        /// </summary>
        /// <param name="professionId"></param>
        /// <param name="newName"></param>
        /// <param name="instituteId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IActionResult Update([Required] Int32 professionId, [Required] String newName, [Required] Int32 instituteId,[Required] ProfessionType type)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemInfoManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "你并无信息管理操作权限"
                    });
                }
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
                    LogPricipalOperation log = _logger.GetDefaultLogPricipalOperation(
                        PrincpalOperationCode.ProfessionUpdate, $"查询编号:{pro.InstituteId}",
                        $"修改专业信息被修改后的专业名{newName} 修改前{pro.Name} 或修改专业类型");

                    log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                    pro.InstituteId = instituteId;
                    pro.Name = newName;
                    pro.ProfessionType = type;
                    _context.LogPricipalOperations.Add(log);
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
                    error = $"参数错误! 传递了错误的参数！"
                });
            }
        }

        public IActionResult Page([Required] int pageIndex, String name, [Required] int instituteId)
        {
            if (ModelState.IsValid && pageIndex > 0 )
            {
                String sql = "select * from ProfessionView where ProfessionId > 0";

                if (name != null && name.Trim() != "")
                {
                    sql += $" and Name like '%{name.Trim().Replace(";",".")}%'";
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

                if (pageIndex > pageCount || pageIndex <= 0)
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
                    professions = listResultMaps,
                    size = pageSize
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
       
        /// <summary>
        /// 完成日志记录
        /// </summary>
        /// <param name="professionId"></param>
        /// <returns></returns>
        public IActionResult Delete([Required] int professionId)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemInfoManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "你并无信息管理操作权限"
                    });
                }
                if (_context.Student.Any(stu => stu.ProfessionId == professionId))
                {
                    return Json(new
                    {
                        isOk = false,
                        Error = "尚有学生属于此专业,无法删除此专业！"
                    });
                }

                Profession pro = _context.Professions.FirstOrDefault(p => p.ProfessionId == professionId);
                if (pro != null)
                {
                    LogPricipalOperation log = _logger.GetDefaultLogPricipalOperation(
                        PrincpalOperationCode.ProfessionDelete, $"查询编号:{pro.InstituteId}",
                        $"删除专业{pro.Name}");

                    log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                    _context.Professions.Remove(pro);
                    _context.LogPricipalOperations.Add(log);
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

        /// <summary>
        /// 完成日志记录 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instituteId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IActionResult Create([Required] String name,[Required] int instituteId,ProfessionType type) 
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemInfoManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "你并无信息管理操作权限"
                    });
                }
                if (_context.Institute.Any(val => val.InstituteId == instituteId))
                {
                    if (_context.Professions.Any(pro => pro.Name.Equals(name) && pro.ProfessionType == type )) //同一类型下的
                    {
                        return Json(new
                        {
                            isOk = false,
                            error = "此专业已经存在,名称重复"
                        });
                    }
                    else
                    {
                        LogPricipalOperation log =
                            _logger.GetDefaultLogPricipalOperation(
                                PrincpalOperationCode.ProfessionAdd,
                                $"添加一个新的专业",
                                $"新专业名称: {name}");
                        log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                        _context.LogPricipalOperations.Add(log);

                        Profession profession = new Profession();
                        profession.Name = name;
                        profession.InstituteId = instituteId;
                        profession.ProfessionType = type;
                        _context.Professions.Add(profession);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = true,
                            info = "添加成功"
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