using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LabExam.DataSource;
using LabExam.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using LabExam.IServices;
using LabExam.Models;
using LabExam.Models.JsonModel;
using LabExam.Models.Map;
using LabExam.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    /// <summary>
    /// 管理模块信息
    /// ----- 完成日志记录 -----
    /// </summary>
    [Authorize(Roles = "Principal")]
    public class ModuleController : Controller
    {
        private readonly LabContext _context;
        private readonly ILoadConfigFileService _config;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly ILoggerService _logger;
        public ModuleController(LabContext context, ILoadConfigFileService config, IHttpContextAnalysisService analysis, ILoggerService logger)
        {
            _context = context;
            _config = config;
            _analysis = analysis;
            _logger = logger;
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
        /// <summary>
        /// 开启日志
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([Required] String name)
        {
            try
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

                    LogPricipalOperation operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.AddModule, $"添加模块 模块名称{name.Trim()}",$"添加新的的模块:{name.Trim()}");

                    if (_context.Modules.FirstOrDefault(m => m.Name.Equals(name)) == null)
                    {
                        operation.PrincpalOperationStatus = PrincpalOperationStatus.Success;

                        Module module = new Module {AddTime = DateTime.Now, Name = name.Trim(), PrincipalId = _analysis.GetLoginUserModel(HttpContext).UserId};

                        _context.LogPricipalOperations.Add(operation);
                        _context.Modules.Add(module);

                        await _context.SaveChangesAsync().ContinueWith(r =>
                        {
                            /* 考试出题配置 */
                            SystemSetting setting = _config.LoadSystemSetting();
                            ModuleExamSetting mes = SystemSetting.GetDefault();
                            mes.ModuleId = module.ModuleId;
                            mes.ModuleName = module.Name;
                            setting.ExamModuleSettings.Add(module.ModuleId,mes);
                            _config.ReWriteSystemSetting(setting);

                            /* 考试开放配置 */
                            Dictionary<int, ExamOpenSetting> es = _config.LoadModuleExamOpenSetting();
                            ExamOpenSetting examSetting = new ExamOpenSetting();
                            examSetting.ModuleId = module.ModuleId;
                            examSetting.IsOpen = false;
                            examSetting.ModuleName = module.Name;
                            es.Add(module.ModuleId,examSetting);
                            _config.ReWriteModuleExamOpenSetting(es);
                        });

                        return Json(new
                        {
                            isOk = true
                        });
                    }
                    else
                    {
                        await _logger.LoggerAsync(operation);
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

        /// <summary>
        /// 完成日志记录
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public IActionResult Delete([Required] int moduleId)
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

                if (_context.Cources.Any(cor => cor.ModuleId == moduleId))
                {
                    return Json(new
                    {
                        isOk = false,
                        Error = "此模块下具有课程信息！ 请先删除此模块下辖的所有课程后再来删除模块！"
                    });
                }

                if (_context.MultipleChoices.Any(mul => mul.ModuleId == moduleId))
                {
                    return Json(new
                    {
                        isOk = false,
                        Error = "此模块下具有多选题信息！ 请先删除此模块下辖的所有多选题后再来删除模块！"
                    });
                }

                if (_context.MultipleChoices.Any(mul => mul.ModuleId == moduleId))
                {
                    return Json(new
                    {
                        isOk = false,
                        Error = "此模块下具有多选题信息！ 请先删除此模块下辖的所有多选题后再来删除模块！"
                    });
                }

                if (_context.SingleChoices.Any(sig => sig.ModuleId == moduleId))
                {
                    return Json(new
                    {
                        isOk = false,
                        Error = "此模块下具有单选题信息！ 请先删除此模块下辖的所有单选题后再来删除模块！"
                    });
                }

                if (_context.JudgeChoices.Any(jud => jud.ModuleId == moduleId))
                {
                    return Json(new
                    {
                        isOk = false,
                        Error = "此模块下具有判断题信息！ 请先删除此模块下辖的所有判断题后再来删除模块！"
                    });
                }

                //判断是否具有学院属于它
                if (!_context.InstituteToModules.Any(im => im.ModuleId == moduleId))
                {
                    Module module = _context.Modules.FirstOrDefault(m => m.ModuleId == moduleId);
                    if (module != null)
                    {
                        /* 重写考试配置 */
                        SystemSetting setting = _config.LoadSystemSetting();
                        setting.ExamModuleSettings.Remove(moduleId);
                        _config.ReWriteSystemSetting(setting);

                        /* 重写开发配置*/
                        Dictionary<int, ExamOpenSetting> es = _config.LoadModuleExamOpenSetting();
                        es.Remove(module.ModuleId);
                        _config.ReWriteModuleExamOpenSetting(es);


                        _context.Remove(module); //删除这个模块
                        LogPricipalOperation log =
                            _logger.GetDefaultLogPricipalOperation(
                                PrincpalOperationCode.ModuleDelete,
                                $"查询编码:{moduleId} ",
                                $"删除模块：{module.Name}");

                        log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                        _context.LogPricipalOperations.Add(log);

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
                        Error = "此模块下具有学院信息！ 请先删除此模块下辖的所有学院后再来删除模块！"
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
        /// 完成日志记录方法
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public IActionResult ReName([Required] int moduleId, [Required] String newName)
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

                Module module = _context.Modules.Find(moduleId);
                if (module != null)
                {
                    LogPricipalOperation log =
                        _logger.GetDefaultLogPricipalOperation(
                            PrincpalOperationCode.ModuleReName,
                            $"查询编码:{moduleId} ",
                            $"重命名模块 {module.Name} 名称为 {newName}");

                    module.Name = newName;
                    log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                    _context.LogPricipalOperations.Add(log);
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
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemInfoManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        error = "你并无信息管理操作权限"
                    });
                }

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

        /// <summary>
        ///  完成日志记录方法 DeleteInstitute
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="instituteId"></param>
        /// <returns></returns>
        public IActionResult DeleteInstitute([Required] int moduleId, [Required] int instituteId)
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
                LogPricipalOperation log = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.DeleteInstituteFromModule, " 学院id: {instituteId} 模块id {moduleId}", $"将一个学院从此模块中排除出去 学院id: {instituteId} 模块id {moduleId}");
                
                InstituteToModule im = _context.InstituteToModules.FirstOrDefault(val =>
                    val.InstituteId == instituteId && val.ModuleId == moduleId);

                if (im != null)
                {
                    log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                    _context.InstituteToModules.Remove(im);
                    _context.LogPricipalOperations.Add(log);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true
                    });
                }
                else
                {
                    _logger.Logger(log);
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

        /// <summary>
        ///  完成日志记录此方法: AddInstitute
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="instituteId"></param>
        /// <returns></returns>
        public IActionResult AddInstitute([Required] int moduleId,[Required] int instituteId)
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

                if (_context.Modules.Any(val => val.ModuleId == moduleId) && _context.Institute.Any(ins => ins.InstituteId == instituteId))
                {
                    LogPricipalOperation log = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.ChangeInstituteToModule, " 学院id: {instituteId} 模块id {moduleId}", $"将一个学院归属要这个模块 学院id: {instituteId} 模块id {moduleId}");
                    
                    if (_context.InstituteToModules.Any(one =>
                        one.InstituteId == instituteId))
                    {
                        _logger.Logger(log);
                        return Json(new
                        {
                            isOk = false,
                            error = "学院有归属了！ 一个学院不能属于两个模块"
                        });
                    }
                    else
                    {
                        InstituteToModule im = new InstituteToModule {InstituteId = instituteId, ModuleId = moduleId};
                        log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                        _context.InstituteToModules.Add(im);
                        _context.LogPricipalOperations.Add(log);
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
