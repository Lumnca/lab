using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using LabExam.DataSource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.EntitiyViews;
using LabExam.Models.Map;
using Microsoft.EntityFrameworkCore;

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class InstituteController : Controller
    {
        private readonly LabContext _context;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly ILoggerService _logger;

        public InstituteController(LabContext context, IHttpContextAnalysisService analysis, ILoggerService logger)
        {
            _context = context;
            _analysis = analysis;
            _logger = logger;
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

        [AllowAnonymous]
        public IActionResult List()
        {
            return Json(_context.VInstituteMaps.ToList());
        }

        public IActionResult ListByModule([Required] int mId)
        {
            if (mId <= 0)
            {
                return Json(_context.VInstituteMaps.ToList());
            }
            else
            {
                return Json(_context.VInstituteMaps.Where(m => m.ModuleId == mId).ToList());
            }
        }

        /// <summary>
        /// 创建一个新的学院
        /// </summary>
        /// <param name="name"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Create([Required] String name,[Required] int moduleId)
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

                if (_context.Modules.Any(m => m.ModuleId == moduleId))
                {
                    if (_context.Institute.Any(ins => ins.Name == name))
                    {
                        return Json(new
                        {
                            isOk = false,
                            error = "新建学院名称重复,已经存在此学院"
                        });
                    }
                    else
                    {
                        LogPricipalOperation log =
                            _logger.GetDefaultLogPricipalOperation(
                                PrincpalOperationCode.InstituteAdd,
                                $"添加新的学院",
                                $"添加新的学院名称 {name}");

                        log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                        _context.LogPricipalOperations.Add(log);
                        Institute institute = new Institute {Name = name};
                        _context.Institute.Add(institute);
                        await _context.SaveChangesAsync().ContinueWith(t =>
                        {
                            int result = t.Result;
                            if (result == 1)
                            {
                                InstituteToModule instituteToModule = new InstituteToModule();
                                instituteToModule.ModuleId = moduleId;
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
                int dataCount = _context.VInstituteMaps.FromSql(sql).Count();
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

                var listResultMaps = _context.VInstituteMaps.FromSql(sql).OrderBy(ins => ins.InstituteId)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();


                return Json(new
                {
                    isOk = true,
                    lineCount = dataCount,
                    pageCount = pageCount,//总共是多少页
                    pageNowIndex = pageIndex, //当前是第几页
                    institutes = listResultMaps,
                    size = pageSize
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

        /// <summary>
        /// 完成日志记录
        /// </summary>
        /// <param name="newName">模块新名称</param>
        /// <param name="instituteId">修改的模块的编码</param>
        /// <returns></returns>
        public async Task<IActionResult> Update([Required] String newName, [Required] int instituteId)
        {
            if (ModelState.IsValid && instituteId > 0)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemInfoManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "你并无信息管理操作权限"
                    });
                }
                Institute ins = _context.Institute.FirstOrDefault(one => one.InstituteId == instituteId);
                if (ins != null)
                {
                    LogPricipalOperation log =
                        _logger.GetDefaultLogPricipalOperation(
                            PrincpalOperationCode.InstituteUpdate,
                            $"查询编码:{ins.InstituteId} ",
                            $"修改学院信息：{ins.Name} 名称改为 {newName}");

                    ins.Name = newName;
                    log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                    _context.LogPricipalOperations.Add(log);
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

        /// <summary>
        ///  完成日志记录
        /// </summary>
        /// <param name="instituteId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete([Required] int instituteId)
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

                Institute ins = _context.Institute.FirstOrDefault(one => one.InstituteId == instituteId);

                LogPricipalOperation log =
                    _logger.GetDefaultLogPricipalOperation(
                        PrincpalOperationCode.ModuleDelete,
                        $"查询编码:{ins.InstituteId} ",
                        $"删除学院：{ins.Name}");


                if (ins != null)
                {
                    if (_context.Professions.Any(pro => pro.InstituteId == instituteId))
                    {
                        _context.LogPricipalOperations.Add(log); //删除失败
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = false,
                            error = "此学院下具有专业信息！ 请先删除此学院下辖的所有专业后再来删除学院！"
                        });
                    }
                    else
                    {
                        log.PrincpalOperationStatus = PrincpalOperationStatus.Success; //删除成功
                        _context.LogPricipalOperations.Add(log);
                        _context.Institute.Remove(ins);
                        await _context.SaveChangesAsync();
                        return Json(new
                        {
                            isOk = true,
                            info = "删除成功！"
                        });
                    }
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