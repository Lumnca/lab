using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.JsonModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    public class SettingController : Controller
    {
        private readonly IHostingEnvironment _hosting;
        private readonly LabContext _context;

        public SettingController(LabContext context, IHostingEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
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
                                ModuleExamSetting moduleSetting = null;
                                Boolean isOk = setting.ExamModuleSettings.TryGetValue(moduleId,out moduleSetting);

                                if (isOk == true)
                                {
                                    return Json(new
                                    {
                                        moduleSetting = moduleSetting,
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

                        var stream = new FileStream(Path.GetFullPath($@"{_hosting.ContentRootPath}/SettingConfig.json"),FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                        StreamReader reader = new StreamReader(stream);
                        SystemSetting systemSetting =
                            JsonConvert.DeserializeObject<SystemSetting>(reader.ReadToEnd());

                        systemSetting.ExamModuleSettings[setting.ModuleId] = setting;

                        reader.Close();
                        reader.Dispose();
                        stream.Close();
                        stream.Dispose();

                        System.IO.File.WriteAllText(
                            Path.GetFullPath($@"{_hosting.ContentRootPath}/SettingConfig.json"),
                            JsonConvert.SerializeObject(systemSetting,Formatting.Indented));

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
    }
}
