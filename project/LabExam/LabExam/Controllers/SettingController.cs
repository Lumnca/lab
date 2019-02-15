using LabExam.DataSource;
using LabExam.Models.Entities;
using LabExam.Models.JsonModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using LabExam.IServices;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class SettingController : Controller
    {
        private readonly IHostingEnvironment _hosting;
        private readonly LabContext _context;
        private readonly ILoadConfigFileService _config;
        private readonly IHttpContextAnalysisService _analysis;
        public SettingController(LabContext context, IHostingEnvironment hosting, ILoadConfigFileService config, IHttpContextAnalysisService analysis)
        {
            _context = context;
            _hosting = hosting;
            _config = config;
            _analysis = analysis;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Module> modules = _context.Modules.ToList();
            return View(modules);
        }

        public IActionResult LoadSetting([Required] int moduleId)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemSettingManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无系统设置操作权限"
                    });
                }
                Module module = _context.Modules.FirstOrDefault(m => m.ModuleId == moduleId);
                if (module == null)
                {
                    return Json(new
                    {
                        isOk = false,
                        title ="错误提示",
                        message = "模块不存在或者已经删除"
                    });
                }
                if (System.IO.File.Exists(Path.GetFullPath($@"{_hosting.ContentRootPath}/SettingConfig.json")))
                {
                    try
                    {
                        using (var stream = new FileStream(Path.GetFullPath($@"{_hosting.ContentRootPath}/SettingConfig.json"),
                            FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                String json = reader.ReadToEnd();
                                SystemSetting setting = JsonConvert.DeserializeObject<SystemSetting>(json);
                                Boolean isOk = setting.ExamModuleSettings.TryGetValue(moduleId,out var _moduleSetting);

                                if (isOk == true)
                                {
                                    return Json(new
                                    {
                                        moduleSetting = _moduleSetting,
                                        isOk = true,
                                        title = "提示",
                                        message = "加载成功！"
                                    });
                                }
                                else
                                {
                                    //如果没有模块设置将会加载默认设置
                                    ModuleExamSetting msetting = SystemSetting.GetDefault();
                                    msetting.ModuleId = module.ModuleId;
                                    msetting.ModuleName = module.Name;

                                    return Json(new
                                    {
                                        moduleSetting = msetting,
                                        isOk = true,
                                        title = "提示",
                                        message = "加载成功！"
                                    });
                                }
                            };
                        };
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "严重错误提示！",
                        message = "系统配置文件丢失...请联系系统维护人员！及时处理..."
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误,模块信息插入失败"
                });
            }
        }

        public IActionResult Module([Required] int moduleId, [Required] String data)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemSettingManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无系统设置操作权限"
                    });
                }
                Module module = _context.Modules.FirstOrDefault(m => m.ModuleId == moduleId);

                if (module == null)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示！",
                        message = "模块不存在或者已经被删除..."
                    });
                }
                else
                {
                    try
                    {
                        ModuleExamSetting setting = JsonConvert.DeserializeObject<ModuleExamSetting>(data);
                        setting.ModuleName = module.Name;
                        SystemSetting systemSetting = _config.LoadSystemSetting();
                        systemSetting.ExamModuleSettings[setting.ModuleId] = setting;
                        _config.ReWriteSystemSetting(systemSetting);

                        return Json(new
                        {
                            isOk = true,
                            title = "提示！",
                            message = "更新成功！"
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误,输入了非法参数"
                });
            }
        }

        public IActionResult Sys()
        {
            SystemSetting setting = _config.LoadSystemSetting();
            setting.Staffs = setting.Staffs.OrderBy(s => s.Name).ToList(); //按照名称排序

            ViewData["ModuleSetting"] = _config.LoadModuleExamOpenSetting();
            return View(setting);
        }

        public IActionResult Reload()
        {
            try
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemSettingManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示！",
                        message = "你并无系统设置操作权限"
                    });
                }
                SystemSetting systemSetting = _config.LoadSystemSetting();
                List<int> keys = new List<int>();

                foreach (var pair in systemSetting.ExamModuleSettings)
                {
                    int moduleKey = pair.Key;
                    if (!_context.Modules.Any(m => m.ModuleId == moduleKey))
                    {
                        keys.Add(moduleKey);
                    }
                }
                foreach (var key in keys)
                {
                    systemSetting.ExamModuleSettings.Remove(key);
                }

                _config.ReWriteSystemSetting(systemSetting);

                return Json(new
                {
                    isOk = true,
                    title = "提示！",
                    message = "配置文件检查完毕！非常感谢你对系统的维护所做的贡献！"
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IActionResult ExamSetting()
        {
            if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemSettingManager)
            {
                return Json(new
                {
                    isOk = false,
                    title ="错误提示",
                    message = "你并无系统设置操作权限"
                });
            }

            Dictionary<int,ExamOpenSetting> openSetting = new Dictionary<int, ExamOpenSetting>();
           foreach (var module in _context.Modules.ToList())
           {
               ExamOpenSetting setting = new ExamOpenSetting()
               {
                   IsOpen = true,ModuleId = module.ModuleId,ModuleName = module.Name
               };
               openSetting.Add(module.ModuleId, setting);
           }
           _config.ReWriteModuleExamOpenSetting(openSetting);
           return Json(new
           {
               load = true
           });
        }

        public IActionResult SysLogin([Required] String data)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemSettingManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示！",
                        message = "你并无系统设置操作权限"
                    });
                }
                try
                {
                    LoginSetting loginSetting = JsonConvert.DeserializeObject<LoginSetting>(data);
                    SystemSetting setting = _config.LoadSystemSetting();
                    setting.LoginSetting = loginSetting;
                    _config.ReWriteSystemSetting(setting);
                    return Json(new
                    {
                        isOk = true,
                        title = "提示",
                        message = "保存成功"
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误,输入了非法参数"
                });
            }
        }

        public IActionResult Exam([Required] String data)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemSettingManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无系统设置操作权限"
                    });
                }
                try
                {
                    List<ExamOpenSetting> list = JsonConvert.DeserializeObject<List<ExamOpenSetting>>(data);
                    Dictionary<int, ExamOpenSetting>  dicSetting = new Dictionary<int, ExamOpenSetting>();
                    foreach (var val in list)
                    {
                        dicSetting.Add(val.ModuleId,val);                        
                    }
                    _config.ReWriteModuleExamOpenSetting(dicSetting);                    

                    return Json(new
                    {
                        isOk = true,
                        title = "提示",
                        message = "修改成功"
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误,输入了非法参数"
                });
            }
        }

        public IActionResult Staff([Required] String data)
        {

            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.SystemSettingManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无系统设置操作权限"
                    });
                }
                try
                {
                    List<MaintenanceStaff> staffs = JsonConvert.DeserializeObject<List<MaintenanceStaff>>(data);
                    SystemSetting set = _config.LoadSystemSetting();
                    set.Staffs = staffs;
                    _config.ReWriteSystemSetting(set);
                    return Json(new
                    {
                        isOk = true,
                        title = "提示",
                        message = "修改成功"
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "参数错误,输入了非法参数"
                });
            }
        }


    }

}

