using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.JsonModel;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using LabExam.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class PrincipalController : Controller
    {
        private readonly LabContext _context;
        private readonly IEncryptionDataService _encryption;
        private readonly IHostingEnvironment _hosting;
        private readonly IHttpContextAnalysisService _analysis;
        public PrincipalController(LabContext context, IEncryptionDataService encryption, IHostingEnvironment hosting, IHttpContextAnalysisService analysis)
        {
            _context = context;
            _encryption = encryption;
            _hosting = hosting;
            _analysis = analysis;
        }

        public IActionResult Index()
        {
            LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
            Principal principal = _context.Principals.Find(user.UserId);

            if (principal != null)
            {
                ViewBag.reExam =
                    _context.ApplicationForReExaminations.Count(a => a.ApplicationStatus == ApplicationStatus.Submit);
                ViewBag.join =
                    _context.ApplicationJoinTheExaminations.Count(a => a.ApplicationStatus == ApplicationStatus.Submit);

                ViewBag.moduleCount = _context.Modules.Count();
                ViewBag.instituteCount = _context.Institute.Count();
                ViewBag.professionCount = _context.Professions.Count();

                ViewBag.adminCount = _context.Principals.Count();
                ViewBag.underCount = _context.Student.Count(s => s.StudentType == StudentType.UnderGraduate);
                ViewBag.postCount = _context.Student.Count(s => s.StudentType == StudentType.PostGraduate);
                ViewBag.courseCount = _context.Cources.Count();
                ViewBag.resourceCount = _context.Resources.Count();
                return View(principal);
            }
            else
            {
                RedirectToAction("Wrong", "Error", new {data = "请从新登录系统!"});
            }

            return View();
        }

        public IActionResult Admin()
        {
            List<Principal> principals = _context.Principals.OrderBy(item => item.PrincipalId).Take(10).ToList();
            int pageSize = 10;
            int dataCount = _context.Principals.Count();
            int pageCount = dataCount / pageSize;
            int lastCount = dataCount % pageSize;
            if (lastCount > 0)
            {
                pageCount++;
            }

            ViewBag.lineCount = dataCount;
            ViewBag.pageCount = pageCount;
            return View(principals);
        }

        public IActionResult Page([Required] int index, String name, String jobId, String pid)
        {
            if (ModelState.IsValid && index > 0)
            {
                String sql = "select * from [Principal] where  Len([PrincipalId]) > 0";

                if (pid != null && pid.Trim() != "")
                {
                    sql += $" and PrincipalId like '%{pid.Trim().Replace(";", ".")}%'";
                }

                if (name != null && name.Trim() != "")
                {
                    sql += $" and Name like '%{name.Trim().Replace(";", ".")}%'";
                }

                if (jobId != null && jobId.Trim() != "")
                {
                    sql += $" and JobNumber like '%{jobId.Trim().Replace(";", ".")}%'";
                }

                int pageSize = 10;
                int dataCount = _context.Principals.FromSql(sql).Count();
                int pageCount = dataCount / pageSize;
                int lastCount = dataCount % pageSize;
                if (lastCount > 0)
                {
                    pageCount++;
                }

                if (index > pageCount || index <= 0)
                {
                    return Json(new
                    {
                        isOk = true,
                        lineCount = 0,
                        pageCount = 1, //总共是多少页
                        pageNowIndex = 1, //当前是第几页
                    });
                }

                var items = _context.Principals.FromSql(sql).OrderBy(item => item.PrincipalId)
                    .Skip((index - 1) * pageSize).Take(pageSize).Select(val => new
                    {
                        id = val.PrincipalId,
                        jobId = val.JobNumber,
                        name = val.Name,
                        status = val.PrincipalStatus == PrincipalStatus.Normal ? "正常" : val.PrincipalStatus == PrincipalStatus.Ban ? "禁止" : "超级管理员",
                        phone = val.Phone
                    }).ToList();

                return Json(new
                {
                    isOk = true,
                    lineCount = dataCount,
                    PageCount = pageCount, //总共是多少页
                    pageNowIndex = index, //当前是第几页
                    Items = items,
                    size = pageSize
                });
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = $"参数错误,{ModelState.ErrorCount} {ModelState.Keys.FirstOrDefault().ToString()} 传递了不符合规定的参数"
                });
            }
        }

        public IActionResult Create([Required] String id, [Required] String jobId, [Required] String name,
            [Required] String phone, [Required] String pwd)
        {
            if (ModelState.IsValid)
            {
                PrincipalConfig principalConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!principalConfig.Power.SystemManager)
                {
                    if (!_context.Principals.Any(p => p.PrincipalId == principalConfig.PrincipalId && p.PrincipalStatus == PrincipalStatus.Super))
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "你并无系统管理操作权限"
                        });
                    }
                }
                if (_context.Student.Any(stu => stu.StudentId == id))
                {
                    return Json(new
                    {
                        isOk = false,
                        message = $"编号:{id}是已经存在的学号！无法使用！"
                    });
                }

                if (id.Length == 12 || id.Length == 10)
                {
                    return Json(new
                    {
                        isOk = false,
                        message = $"编号:{id}属于学号范围！长度不能为10位或者12位 无法使用！"
                    });
                }

                if (_context.Principals.Any(admin => admin.PrincipalId == id || admin.JobNumber == jobId))
                {
                    return Json(new
                    {
                        isOk = false,
                        message = $"编号:{id}或者工号{jobId} 已经使用！"
                    });
                }
                else
                {
                    Principal principal = new Principal
                    {
                        PrincipalId = id,
                        JobNumber = jobId,
                        Name = name,
                        Phone = phone,
                        PrincipalStatus = PrincipalStatus.Normal,
                        PrincipalConfig = $"{id}.json"
                    };
                    String password = _encryption.EncodeByRsa(_encryption.EncodeByMd5(_encryption.EncodeByMd5(pwd)));
                    principal.Password = password;
                    _context.Principals.Add(principal);

                    int result = _context.SaveChanges();

                    if (result == 1)
                    {
                        //配置权限
                        PrincipalConfig config = new PrincipalConfig
                        {
                            SettingTime = DateTime.Now, PrincipalId = id, Power = new Power()
                        };

                        using (var stream = new FileStream(
                            Path.GetFullPath($@"{_hosting.ContentRootPath}/JsonConfig/{id}.json"), FileMode.Create,
                            FileAccess.Write, FileShare.None))
                        {
                            using (var writer = new StreamWriter(stream))
                            {
                                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings());
                                String jsonResult = JsonConvert.SerializeObject(config, Formatting.Indented);
                                writer.Write(jsonResult);
                            }
                        }

                        return Json(new
                        {
                            isOk = true,
                            message = "信息插入成功"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "信息插入失败"
                        });
                    }
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = $"参数错误！输入了不合规范的参数。 "
                });
            }
        }

        public IActionResult Delete([Required] String pId)
        {
            if (ModelState.IsValid)
            {
                PrincipalConfig principalConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!principalConfig.Power.SystemManager)
                {
                    if (!_context.Principals.Any(p => p.PrincipalId == principalConfig.PrincipalId && p.PrincipalStatus == PrincipalStatus.Super))
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "你并无系统管理操作权限"
                        });
                    }
                }
                Principal principal = _context.Principals.Find(pId);
                if (principal != null)
                {
                    _context.Remove(principal);
                    _context.SaveChanges();

                    String path = Path.GetFullPath($@"{_hosting.ContentRootPath}/JsonConfig/{pId}.json");
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    return Json(new
                    {
                        isOk = true,
                        message = "删除成功！ "
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = true,
                        message = "管理员不存在或者已被删除！ "
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = $"参数错误！输入了不合规范的参数。 "
                });
            }
        }

        public IActionResult Update([Required] String pId, String jobId, [Required] String name,
            [Required] String phone,[Required] PrincipalStatus status)
        {
            if (ModelState.IsValid)
            {
                PrincipalConfig principalConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!principalConfig.Power.SystemManager)
                {
                    if (!_context.Principals.Any(p => p.PrincipalId == principalConfig.PrincipalId && p.PrincipalStatus == PrincipalStatus.Super))
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "你并无系统管理操作权限"
                        });
                    }
                }
                Principal principal = _context.Principals.FirstOrDefault(val => val.PrincipalId == pId);
                if (principal != null)
                {
                    principal.JobNumber = jobId;
                    principal.Name = name;
                    principal.Phone = phone;
                    principal.PrincipalStatus = status;
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        message = "修改成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        message = "管理员不存在或者已被删除！ "
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = $"参数错误！输入了不合规范的参数。 "
                });
            }
        }

        [HttpGet]
        public IActionResult Power([Required] String pId)
        {
            if (ModelState.IsValid)
            {
                Principal principal = _context.Principals.Find(pId);
                if (principal != null)
                {
                    if (System.IO.File.Exists(Path.GetFullPath($@"{_hosting.ContentRootPath}/JsonConfig/{pId}.json")))
                    {
                        using (var stream = new FileStream(
                            Path.GetFullPath($@"{_hosting.ContentRootPath}/JsonConfig/{pId}.json"),
                            FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                String json = reader.ReadToEnd();

                                try
                                {
                                    PrincipalConfig config = JsonConvert.DeserializeObject<PrincipalConfig>(json);


                                    return Json(new
                                    {
                                        isOk = true,
                                        setting = config,
                                        name = principal.Name,
                                        status = principal.PrincipalStatus == PrincipalStatus.Normal,
                                        message = "管理员不存在或者已被删除！ "
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
                    else
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "管理员配置文件丢失！ 无法使用！ "
                        });
                    }

                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        message = "管理员不存在或者已被删除！ "
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = $"参数错误！输入了不合规范的参数。 "
                });
            }
        }

        public IActionResult Reset([Required] String pId)
        {
            if (ModelState.IsValid)
            {
                PrincipalConfig principalConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!principalConfig.Power.SystemManager)
                {
                    if (!_context.Principals.Any(p => p.PrincipalId == principalConfig.PrincipalId && p.PrincipalStatus == PrincipalStatus.Super))
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "你并无系统管理操作权限"
                        });
                    }
                }
                Principal principal = _context.Principals.Find(pId);
                if (principal != null)
                {
                    principal.Password =
                        _encryption.EncodeByRsa(_encryption.EncodeByMd5(_encryption.EncodeByMd5("123456")));
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        message = "重置完毕！ "
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        message = "管理员不存在或者已被删除！ "
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = $"参数错误！输入了不合规范的参数。 "
                });
            }
        }

        public IActionResult Status([Required] String pId, [Required] Boolean status)
        {
            if (ModelState.IsValid)
            {
                PrincipalConfig principalConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!principalConfig.Power.SystemManager)
                {
                    if (!_context.Principals.Any(p => p.PrincipalId == principalConfig.PrincipalId && p.PrincipalStatus == PrincipalStatus.Super))
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "你并无系统管理操作权限"
                        });
                    }
                }
                Principal principal = _context.Principals.Find(pId);
                if (principal != null)
                {
                    principal.PrincipalStatus = status ? PrincipalStatus.Normal : PrincipalStatus.Ban;
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = false,
                        message = $"操作成功！ "
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        message = $"管理员不存在或者已经删除！ "
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = $"参数错误！输入了不合规范的参数。导致改变用户状态失败 "
                });
            }
        }

        [HttpPost]
        public IActionResult Setting([Required] String pId,[Required] String powerString)
        {
            if (ModelState.IsValid)
            {
                PrincipalConfig principalConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!principalConfig.Power.SystemManager)
                {
                    if (!_context.Principals.Any(p => p.PrincipalId == principalConfig.PrincipalId && p.PrincipalStatus == PrincipalStatus.Super))
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "你并无系统管理操作权限"
                        });
                    }
                }
                Principal principal = _context.Principals.Find(pId);
                if (principal != null)
                {
                    //写入配置文件 到文件中
                    try
                    {
                        Power p = JsonConvert.DeserializeObject<Power>(powerString);
                        PrincipalConfig config = new PrincipalConfig
                        {
                            PrincipalId = pId, Power = p, SettingTime = DateTime.Now
                        };

                        using (var stream = new FileStream(
                            Path.GetFullPath($@"{_hosting.ContentRootPath}/JsonConfig/{pId}.json"), FileMode.Create,
                            FileAccess.Write, FileShare.ReadWrite))
                        {
                            using (var writer = new StreamWriter(stream))
                            {
                                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings());
                                String jsonResult = JsonConvert.SerializeObject(config, Formatting.Indented);
                                writer.Write(jsonResult);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        message = $"操作成功！ "
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        message = $"管理员不存在或者已经删除！ "
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = $"参数错误！输入了不合规范的参数。导致改变用户状态失败 "
                });
            }
        }

        public JsonResult Person([Required] String pId)
        {
            if (ModelState.IsValid)
            {
                PrincipalConfig principalConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!principalConfig.Power.SystemManager)
                {
                    if (!_context.Principals.Any(p => p.PrincipalId == principalConfig.PrincipalId && p.PrincipalStatus == PrincipalStatus.Super))
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "你并无系统管理操作权限"
                        });
                    }
                }
                var val = _context.Principals.FirstOrDefault(p=>p.PrincipalId == pId);
                if (val != null)
                {
                    return Json(new
                    {
                        isOk = false,
                        message = $"加载成功",
                        id = val.PrincipalId,
                        jobId = val.JobNumber,
                        name = val.Name,
                        status = val.PrincipalStatus == PrincipalStatus.Normal ? "正常" : val.PrincipalStatus == PrincipalStatus.Ban ? "禁止" : "超级管理员", 
                        phone = val.Phone
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        message = $"用户不存在或者已经被删除！。 "
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = $"参数错误！输入了不合规范的参数。 "
                });
            }
        }

        public IActionResult Password([Required,MinLength(6)] String password,[Required] String lastPassword)
        {
            if (ModelState.IsValid)
            {
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                Principal principal = _context.Principals.Find(user.UserId);
                
                if (principal != null)
                {
                    String dbLastPassword = _encryption.DecryptByRsa(principal.Password);
                    String lastPasswordMd5 = _encryption.EncodeByMd5Times(lastPassword,2);
                    if (lastPasswordMd5 == dbLastPassword)
                    {
                        principal.Password = _encryption.EncodeByRsa(_encryption.EncodeByMd5Times(password, 2));
                        _context.SaveChanges();
                        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return Json(new
                        {
                            isOk = true,
                            title = "提示",
                            message = $"修改成功"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            isOk = false,
                            title ="错误",
                            message = $"旧密码错误！"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误",
                        message = $"用户不存在或者已经被删除！。 "
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误",
                    message = $"参数错误！输入了不合规范的参数。 "
                });
            }
        }
    }
}


        
