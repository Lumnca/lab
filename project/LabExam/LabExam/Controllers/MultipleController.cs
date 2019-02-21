using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class MultipleController : Controller
    {
        private readonly LabContext _context;
        private readonly IEncryptionDataService _encryption;
        private readonly IHostingEnvironment _hosting;
        private readonly IHttpContextAnalysisService _analysis;

        public MultipleController(LabContext context, IEncryptionDataService encryption, IHostingEnvironment hosting, IHttpContextAnalysisService analysis)
        {
            _context = context;
            _encryption = encryption;
            _hosting = hosting;
            _analysis = analysis;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Page([Required] int index, [Required] Int32 mId, String pId, String content)
        {
            if (ModelState.IsValid && index > 0)
            {
                String sql = "select * from MultipleChoices where MultipleId > 0";
                if (mId != -1)
                {
                    sql += $" and ModuleId = {mId}";
                }
                if (pId != null && pId.Trim() != "")
                {
                    sql += $" and PrincipalId = {pId.Trim().Replace(";", ".")}";
                }

                if (content != null && content.Trim() != "")
                {
                    sql += $" and Content like '%{content.Trim().Replace(";", ".")}%'";
                }
                int pageSize = 10; //每一页的题目数量
                int dataCount = _context.MultipleChoices.FromSql(sql).Count();
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
                var list = _context.MultipleChoices.FromSql(sql).OrderBy(item => item.MultipleId).Include("Module")
                    .Skip((index - 1) * pageSize).Take(pageSize).ToList();
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

        public IActionResult Create([Bind(include: "ModuleId,Content,Answer,Count,A,B,C,D,E,F")] MultipleChoices item)
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
                #region 功能实现区域
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                String Key = _encryption.EncodeByMd5(item.Content.Trim());
                if (_context.MultipleChoices.Any(s => s.Key == Key && s.ModuleId == item.ModuleId))
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你的题目已经存在！ 重复题目无法加入"
                    });
                }

                item.Content = item.Content.Trim();
                item.AddTime = DateTime.Now;
                item.Key = Key;
                Char[] answer = item.Answer.ToUpper().Trim().ToCharArray();
                Array.Sort(answer);
                item.Answer = String.Join("", answer); //答案全部大写
                item.A = item.A.Trim();
                item.B = item.B.Trim();
                item.C = item.C?.Trim();
                item.D = item.D?.Trim();
                item.E = item.E?.Trim();
                item.F = item.F?.Trim();
                item.Count = item.Count;
                item.PrincipalId = user.UserId;
                item.DegreeOfDifficulty = 1;

                _context.MultipleChoices.Add(item);
                _context.SaveChanges();
                return Json(new
                {
                    isOk = true,
                    title = "消息提示",
                    message = "添加成功！"
                });
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

        public IActionResult Item([Required] int itemId)
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

                #region 功能实现区域
                MultipleChoices item = _context.MultipleChoices.Include("Module").FirstOrDefault(j => j.MultipleId == itemId);
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

        public IActionResult Update([Bind(include: "MultipleId,ModuleId,Content,Answer,A,B,C,D,E,F")] MultipleChoices item)
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

                MultipleChoices multiple = _context.MultipleChoices.Find(item.MultipleId);
                if (multiple != null)
                {
                    String key = _encryption.EncodeByMd5(item.Content.Trim());

                    //如果题干已经被改变了 那么重新编码
                    if (multiple.Content != item.Content.Trim())
                    {
                        //检查重复性
                        if (_context.MultipleChoices.Any(i => i.Key == key && i.ModuleId == item.ModuleId))
                        {
                            return Json(new
                            {
                                isOk = false,
                                title = "错误提示",
                                message = "你的题目已经存在！ 或者未作出修改"
                            });
                        }
                    }
                    multiple.Content = item.Content.Trim();
                    multiple.AddTime = DateTime.Now;
                    multiple.Key = key;
                    //答案排序一下
                    Char[] answer = item.Answer.ToUpper().Trim().ToCharArray();
                    Array.Sort(answer);
                    multiple.Answer = String.Join("", answer); //答案全部大写
                    multiple.DegreeOfDifficulty = 1; //重置题目难度
                    multiple.A = item.A.Trim();
                    multiple.B = item.B.Trim();
                    multiple.C = item.C?.Trim();
                    multiple.D = item.D?.Trim();
                    multiple.E = item.E?.Trim();
                    multiple.F = item.F?.Trim();
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
                        title = "错误提示",
                        message = "不存在此题目"
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

        public IActionResult Delete([Required] int singleId)
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

                #region 功能实现区域
                MultipleChoices item = _context.MultipleChoices.Find(singleId);
                if (item != null)
                {
                    _context.MultipleChoices.Remove(item);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title = "信息提示",
                        message = "删除成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "此题目不存在！或者已经被删除了"
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
