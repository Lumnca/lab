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

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class JudgeController : Controller
    {
        private readonly LabContext _context;
        private readonly IEncryptionDataService _encryption;
        private readonly ILoggerService _logger;
        private readonly IHttpContextAnalysisService _analysis;

        public JudgeController(IHttpContextAnalysisService analysis, IEncryptionDataService encryption, LabContext context, ILoggerService logger)
        {
            _analysis = analysis;
            _encryption = encryption;
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Page([Required] int index, [Required] Int32 mId,String pId, String content)
        {
            if (ModelState.IsValid && index > 0)
            {
                String sql = "select * from JudgeChoices where JudgeId > 0";
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
                int dataCount = _context.JudgeChoices.FromSql(sql).Count();
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

                var list = _context.JudgeChoices.FromSql(sql).OrderBy(item => item.JudgeId).Include("Module")
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
                //获取每一个key对应的ModelStateDictionary
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

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="judge"></param>
        /// <returns></returns>
        public IActionResult Create([Bind(include: "ModuleId,Content,Answer")] JudgeChoices judge)
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

                LogPricipalOperation log = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.AddJudge,
                    "查询Id:无", $"增加判断题 题目内容{judge.Content}");

                #region 功能实现区域
                LoginUserModel user = _analysis.GetLoginUserModel(HttpContext);
                String Key = _encryption.EncodeByMd5(judge.Content.Trim());
                if (_context.JudgeChoices.Any(j => j.Key == Key))
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "你的题目已经存在！ 重复题目无法加入"
                    });
                }

                judge.Content = judge.Content.Trim();
                judge.AddTime = DateTime.Now;
                judge.Key = Key;
                judge.Answer = judge.Answer.ToUpper().Trim(); //答案全部大写
                judge.A = "是";
                judge.B = "否";
                judge.Count = 2;
                judge.PrincipalId = user.UserId;
                judge.DegreeOfDifficulty = 1;

                _context.JudgeChoices.Add(judge);
                _context.SaveChanges();

                log.PrincpalOperationName = $"查询Id:{judge.JudgeId}";
                _logger.Logger(log);
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

        public IActionResult Item([Required] int judgeId)
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
                JudgeChoices judge = _context.JudgeChoices.Include("Module").FirstOrDefault(j => j.JudgeId == judgeId);
                if (judge != null)
                {
                    return Json(new
                    {
                        isOk = true,
                        item = judge,
                        title = "消息提示",
                        message = "加载成功"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = true,
                        item = judge,
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

        public IActionResult Update([Required] int judgeId,[Required] String content,[Required] String answer )
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

                JudgeChoices judge = _context.JudgeChoices.Find(judgeId);
                if (judge != null)
                {
                    String Key = _encryption.EncodeByMd5(content.Trim());

                    //如果题干已经被改变了 那么重新编码
                    if (judge.Content != content)
                    {
                        //检查重复性
                        if (_context.JudgeChoices.Any(j => j.Key == Key))
                        {
                            return Json(new
                            {
                                isOk = false,
                                title = "错误提示",
                                message = "你的题目已经存在！ 或者未作出修改"
                            });
                        }
                    }
                    judge.Content = content.Trim();
                    judge.AddTime = DateTime.Now;
                    judge.Key = Key;
                    judge.Answer = answer.ToUpper().Trim(); //答案全部大写
                    judge.DegreeOfDifficulty = 1; //重置题目难度

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

        public IActionResult Delete([Required] int judgeId)
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
                JudgeChoices judge = _context.JudgeChoices.Find(judgeId);
                if (judge != null)
                {
                    _context.JudgeChoices.Remove(judge);
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