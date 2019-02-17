using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabExam.Controllers
{
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
                    Cource cource = new Cource();
                    cource.Name = name.Trim();
                    cource.AddTime = DateTime.Now;
                    cource.CourceStatus = CourceStatus.Normal;
                    cource.Credit = mark;
                    cource.Introduction = description.Trim();
                    cource.ModuleId = moduleId;
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
                List<string> sb = new List<string>();
                List<string> Keys = ModelState.Keys.ToList();
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述添加到sb中
                    foreach (var error in errors)
                    {
                        sb.Add(error.ErrorMessage);
                    }
                }
                return Json(new
                {
                    isOk = false,
                    error = sb,
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
                if (index > pageCount)
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
                List<string> sb = new List<string>();
                //获取所有错误的Key
                List<string> Keys = ModelState.Keys.ToList();
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述添加到sb中
                    foreach (var error in errors)
                    {
                        sb.Add(error.ErrorMessage);
                    }
                }
                return Json(new
                {
                    isOk = false,
                    error = sb,
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
                Cource item = _context.Cources.Include("Module").FirstOrDefault(j => j.CourceId == itemId);
                if (item != null)
                {
                    return Json(new
                    {
                        isOk = true,
                        item = item,
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
                List<string> sb = new List<string>();
                List<string> Keys = ModelState.Keys.ToList();
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述添加到sb中
                    foreach (var error in errors)
                    {
                        sb.Add(error.ErrorMessage);
                    }
                }
                return Json(new
                {
                    isOk = false,
                    error = sb,
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        
    }
}