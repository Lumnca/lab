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
    public class ResourceController : Controller
    {

        private readonly LabContext _context;
        private readonly IEncryptionDataService _encryption;
        private readonly IHostingEnvironment _hosting;
        private readonly IHttpContextAnalysisService _analysis;

        public ResourceController(LabContext context, IEncryptionDataService encryption, IHostingEnvironment hosting, IHttpContextAnalysisService analysis)
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

        public IActionResult Page([Required] int index, [Required] Int32 cId, [Required] int status,[Required] int type, String content)
        {
            if (ModelState.IsValid && index > 0)
            {
                String sql = "select * from Resources where ResourcesId > 0";
                if (cId != -1)
                {
                    sql += $" and CourceId = {cId}";
                }

                if (content != null && content.Trim() != "")
                {
                    sql += $" and Name like '%{content.Trim().Replace(";", ".")}%'";
                }

                if (status != -1)
                {
                    sql += $" and ResourceStatus = {status}";
                }

                if (type != -1)
                {
                    sql += $" and ResourceType = {type}";
                }

                int pageSize = 10; //每一页的题目数量
                int dataCount = _context.Resources.FromSql(sql).Count();
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
                var list = _context.Resources.FromSql(sql).OrderBy(item => item.ResourceStatus).Include("Cource")
                    .Skip((index - 1) * pageSize).Take(pageSize).Select(r => new
                    {
                        resourceId = r.ResourceId,
                        name = r.Name,
                        lengthOfStudy = r.LengthOfStudy,
                        description = r.Description,
                        cource = r.Cource,
                        courceId = r.CourceId,
                        status = r.ResourceStatus == ResourceStatus.Normal ? "未使用" : "使用中"
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
        
        public IActionResult List([Required] int cId, [Required] int status, [Required] int type)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.CourcesManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你并无题库管理操作权限"
                    });
                }

                String sql = "select * from Resources where ResourceId > 0";

                if (cId != -1)
                {
                    sql += $" and CourceId = {cId}";
                }

                if (status != -1)
                {
                    sql += $" and ResourceStatus = {status}";
                }

                if (type != -1)
                {
                    sql += $" and ResourceType = {type}";
                }

                var resources = _context.Resources.FromSql(sql).Include("Cource").Select(r => new
                {
                    resourceId = r.ResourceId,
                    name = r.Name,
                    lengthOfStudy = r.LengthOfStudy,
                    description = r.Description,
                    cource = r.Cource,
                    courceId = r.CourceId,
                    status = r.ResourceStatus == ResourceStatus.Normal ? "未使用" : "使用中"
                }).ToList();

                return Json(new
                {
                    isOk = true,
                    title = "消息提示",
                    items = resources,
                    message = "加载成功"
                });
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}