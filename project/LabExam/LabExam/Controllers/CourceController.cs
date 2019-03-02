using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class CourceController : Controller
    {
        private readonly LabContext _context;
        private readonly IEncryptionDataService _encryption;
        private readonly IHostingEnvironment _hosting;
        private readonly IHttpContextAnalysisService _analysis;

        public CourceController(LabContext context, IEncryptionDataService encryption, IHostingEnvironment hosting, IHttpContextAnalysisService analysis)
        {
            _context = context;
            _encryption = encryption;
            _hosting = hosting;
            _analysis = analysis;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Module> modules = _context.Modules.ToList();
            return View(modules);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Required] String name,[Required] int moduleId, [Required] float mark,[Required] String description)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.CourcesManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无课程管理操作权限"
                    });
                }

                if (!_context.Modules.Any(m=>m.ModuleId == moduleId))
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "模块不存在,或者已经被删除"
                    });
                }

                if (_context.Cources.Any(c => c.Name == name))
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "此课程已经存在了"
                    });
                }
                else
                {
                    Cource cource = new Cource
                    {
                        Name = name.Trim(),
                        AddTime = DateTime.Now,
                        CourceStatus = CourceStatus.Normal,
                        Credit = mark,
                        Introduction = description.Trim(),
                        ModuleId = moduleId
                    };
                    _context.Cources.Add(cource);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = false,
                        title = "消息提示",
                        message = "添加成功！"
                    });
                }

            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        public IActionResult Page([Required] int index, [Required] Int32 mId,[Required] int status ,String content)
        {
            if (ModelState.IsValid && index > 0)
            {
                String sql = "select * from Cources where CourceId > 0";
                if (mId != -1)
                {
                    sql += $" and ModuleId = {mId}";
                }

                if (content != null && content.Trim() != "")
                {
                    sql += $" and name like '%{content.Trim().Replace(";", ".")}%'";
                }

                if (status != -1)
                {
                    sql += $" and CourceStatus = {status}";
                }

                int pageSize = 10; //每一页的题目数量
                int dataCount = _context.Cources.FromSql(sql).Count();
                int pageCount = dataCount / pageSize; //有多少页
                int lastCount = dataCount % pageSize;//最后一页的题目数量
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
                var list = _context.Cources.FromSql(sql).OrderBy(item => item.CourceStatus).Include("Module")
                    .Skip((index - 1) * pageSize).Take(pageSize).Select(c => new
                    {
                        courceId = c.CourceId,
                        name = c.Name,
                        addTime = c.AddTime,
                        introduction = c.Introduction,
                        credit = c.Credit,
                        moduleId = c.ModuleId,
                        module = c.Module,
                        courceStatus = c.CourceStatus == CourceStatus.Normal? "未使用":"使用中"
                    }).ToList();
                return Json(new
                {
                    isOk = true,
                    lineCount = dataCount,
                    PageCount = pageCount, //总共是多少页
                    pageNowIndex = index, //当前是第几页
                    Items = list
                });
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    message = $"参数错误,传递了不符合规定的参数"
                });
            }
        }

        public IActionResult Item([Required] int itemId)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.CourcesManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无课程管理操作权限"
                    });
                }

                #region 功能实现区域
                var _item = _context.Cources.Include("Module").FirstOrDefault(j => j.CourceId == itemId);
                if (_item != null)
                {
                    return Json(new
                    {
                        isOk = true,
                        item = new
                        {
                            name = _item.Name,
                            module = _item.Module,
                            status = _item.CourceStatus == CourceStatus.Normal ? "未使用" : "使用中",
                            addTime = _item.AddTime,
                            introduction = _item.Introduction,
                            cId = _item.CourceId
                        },
                        title = "消息提示",
                        message = "加载成功"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = true,
                        title = "错误提示",
                        message = "题目不存在！或者已经被删除了！"
                    });
                }
                #endregion
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        public async Task<IActionResult> Stop([Required] int cId)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.CourcesManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无课程管理操作权限"
                    });
                }

                Cource cource = _context.Cources.Find(cId);
                if (cource != null)
                {
                    cource.CourceStatus = CourceStatus.Normal;
                    await _context.SaveChangesAsync();

                    return Json(new
                    {
                        isOk = true,
                        title = "消息提示",
                        message = "修改成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "资源不存在，或者已经被删除了"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        public async Task<IActionResult> Use([Required] int cId)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.CourcesManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无课程管理操作权限"
                    });
                }

                Cource cource = _context.Cources.Find(cId);
                if (cource != null)
                {
                    cource.CourceStatus = CourceStatus.Using;
                    await _context.SaveChangesAsync();

                    return Json(new
                    {
                        isOk = true,
                        title = "消息提示",
                        message = "修改成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "资源不存在，或者已经被删除了"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        /// <summary>
        /// 资源文件地址 有关需要改变文件目录的时候需要改变的地方
        /// </summary>
        /// <param name="cId"></param>
        /// <returns></returns>
        public IActionResult Delete([Required] int cId)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.CourcesManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无课程管理操作权限"
                    });
                }

                Cource c = _context.Cources.Find(cId);

                if (c != null)
                {
                    //先把资源文件删除掉
                    List<Resource> resources = _context.Resources.Where(r => r.CourceId == cId && r.ResourceType == ResourceType.Vedio).ToList();
                    foreach (var res in resources)
                    {
                        String path = Path.GetFullPath($@"{_hosting.WebRootPath}/video/{res.ResourceUrl}");
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    _context.RemoveRange(_context.Resources.Where(r => r.CourceId == cId));
                    _context.Cources.Remove(c);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = false,
                        title = "消息提示",
                        message = "删除成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "课程不存在,或者已经被删除"
                    });
                }


            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        public IActionResult Update([Required] int cId, [Required] String name)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.CourcesManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无课程管理操作权限"
                    });
                }
                if (_context.Cources.Any(cr => cr.Name == name))
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "此课程名称已经存在！或者你没有改变课程名称！"
                    });
                }

                Cource c = _context.Cources.Find(cId);
                if (c != null)
                {
                    c.Name = name.Trim();
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "消息提示",
                        message = "修改成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误信息",
                        message = "课程不存在或者已经被删除了"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }

        }

        public IActionResult All([Required] Int32 mId, [Required] int status, String content)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.CourcesManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无课程管理操作权限"
                    });
                }

                String sql = "select * from Cources where CourceId > 0";
                if (mId != -1)
                {
                    sql += $" and ModuleId = {mId}";
                }

                if (content != null && content.Trim() != "")
                {
                    sql += $" and name like '%{content.Trim().Replace(";", ".")}%'";
                }

                if (status != -1)
                {
                    sql += $" and CourceStatus = {status}";
                }

                List<Cource> list = _context.Cources.FromSql(sql).Where(c => c.CourceStatus == CourceStatus.Normal).ToList();
                foreach (var item in list)
                {
                    item.CourceStatus = CourceStatus.Using;
                }
                _context.SaveChanges();
                return Json(new
                {
                    isOk = true,
                    title = "信息提示",
                    message = "操作成功！"
                });
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        public IActionResult Not([Required] Int32 mId, [Required] int status, String content)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.QuestionBankManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无题库管理操作权限"
                    });
                }

                String sql = "select * from Cources where CourceId > 0";
                if (mId != -1)
                {
                    sql += $" and ModuleId = {mId}";
                }

                if (content != null && content.Trim() != "")
                {
                    sql += $" and name like '%{content.Trim().Replace(";", ".")}%'";
                }

                if (status != -1)
                {
                    sql += $" and CourceStatus = {status}";
                }

                List<Cource> list = _context.Cources.FromSql(sql).Where(c => c.CourceStatus == CourceStatus.Using).ToList();
                foreach (var item in list)
                {
                    item.CourceStatus = CourceStatus.Normal;
                }
                _context.SaveChanges();
                return Json(new
                {
                    isOk = true,
                    title = "信息提示",
                    message = "操作成功！"
                });
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

    }
}