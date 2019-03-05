using LabExam.DataSource;
using LabExam.Filters;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using LabExam.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExam.Controllers
{
    /// <summary>
    /// 完成日志记录 资源控制器
    /// </summary>
    public class ResourceController : Controller
    {
        private readonly LabContext _context;
        private readonly ILoggerService _logger;
        private readonly IHostingEnvironment _hosting;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly IHttpContextAccessor _accessor;

        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public ResourceController(LabContext context, IEncryptionDataService encryption, IHostingEnvironment hosting, IHttpContextAnalysisService analysis, IHttpContextAccessor accessor, ILoggerService logger)
        {
            _context = context;
            _hosting = hosting;
            _analysis = analysis;
            _accessor = accessor;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Cource> cources = _context.Cources.ToList();
            return View(cources);
        }

        public IActionResult Page([Required] int index, [Required] Int32 cId, [Required] int status,[Required] int type, String content)
        {
            if (ModelState.IsValid && index > 0)
            {
                String sql = "select * from Resources where ResourceId > 0";
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
                var list = _context.Resources.FromSql(sql).OrderByDescending(item => item.ResourceStatus).Include("Cource")
                    .Skip((index - 1) * pageSize).Take(pageSize).Select(r => new
                    {
                        resourceId = r.ResourceId,
                        name = r.Name,
                        lengthOfStudy = r.LengthOfStudy,
                        description = r.Description,
                        cource = r.Cource,
                        courceId = r.CourceId,
                        rType = r.ResourceType,
                        type = r.ResourceType == ResourceType.Link?"链接资源":"视频资源",
                        status = r.ResourceStatus == ResourceStatus.Normal ? "未使用" : "使用中"
                    }).ToList();
                return Json(new
                {
                    isOk = true,
                    lineCount = dataCount,
                    PageCount = pageCount, //总共是多少页
                    pageNowIndex = index, //当前是第几页
                    Items = list,
                    size = pageSize
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
                    type = r.ResourceType == ResourceType.Link?"链接文件":"视频文件",
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
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Cource> cources = _context.Cources.ToList();
            return View(cources);
        }

        /// <summary>
        /// 启动日志记录
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Link([Bind(include: "Name,CourceId,Description,LengthOfStudy,ResourceUrl,ResourceType")] Resource resource)
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

                LogPricipalOperation operation =
                    _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.AddResource,
                        $"查询编码:无", "添加课程资源:资源链接{resource.ResourceUrl}");

                if(_context.Cources.Any(c => c.CourceId == resource.CourceId))
                {
                    if (_context.Resources.Any(r => r.Name == resource.Name && r.ResourceType == ResourceType.Link))
                    {
                        await _context.LogPricipalOperations.AddAsync(operation).ContinueWith( async r =>
                        {
                            await _context.SaveChangesAsync();
                        });


                        return Json(new
                        {
                            isOk = false,
                            title = "错误提示",
                            message = "此名称的资源已经存在"
                        });
                    }

                    resource.Name = resource.Name.Trim();
                    resource.Description = resource.Description.Trim();
                    resource.ResourceUrl = resource.ResourceUrl.Trim();
                    resource.ResourceStatus = ResourceStatus.Normal;
                    resource.AddTime = DateTime.Now;
                    resource.PrincipalId = _analysis.GetLoginUserModel(HttpContext).UserId;
                    _context.Resources.Add(resource);

                    await _context.SaveChangesAsync().ContinueWith( async result =>
                    {
                        operation.PrincpalOperationContent = $"添加资源ID {resource.ResourceId}";
                        operation.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                        _context.LogPricipalOperations.Add(operation);
                        _context.SaveChanges();
                    });
                    return Json(new
                    {
                        isOk = true,
                        title = "消息提示",
                        message = "添加成功"
                    });
                }
                else
                {
                    _context.LogPricipalOperations.Add(operation);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "资源所属课程不存在或者已经被删除！"
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

        /// <summary>
        /// 启动日志记录
        /// </summary>
        /// <param name="rId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Stop([Required] int rId)
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

                LogPricipalOperation operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.StopUseResource, $"停用课程资源 编号{rId}", "停用课程资源");
                Resource resource = _context.Resources.Find(rId);
                if (resource != null)
                {
                    resource.ResourceStatus = ResourceStatus.Normal;
                    operation.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                    _context.LogPricipalOperations.Add(operation);
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
                    await _context.LogPricipalOperations.AddAsync(operation).ContinueWith(r =>
                    {
                        _context.SaveChangesAsync();
                    });
                     

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
        /// 启动日志
        /// </summary>
        /// <param name="rId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Use([Required] int rId)
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
                LogPricipalOperation operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.UseResource,$"启用课程资源 编号{rId}","启用课程资源");
                Resource resource = _context.Resources.Find(rId);
                if (resource != null)
                {
                    resource.ResourceStatus = ResourceStatus.Using;
                    operation.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                    _context.LogPricipalOperations.Add(operation);
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
                    _context.LogPricipalOperations.Add(operation);
                    await _context.SaveChangesAsync();
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
        /// 开启日志记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
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

            DateTime dt = DateTime.Now;
            int year = dt.Year;
            String month = dt.Month > 9 ? dt.Month.ToString() : $"0{dt.Month}";
            String day = dt.Day > 9 ? dt.Day.ToString() : $"0{dt.Day}";
            String hour = dt.Hour > 9 ? dt.Hour.ToString() : $"0{dt.Hour}";
            String m = dt.Minute > 9 ? dt.Minute.ToString() : $"0{dt.Minute}";
            String s = dt.Second > 9 ? dt.Second.ToString() : $"0{dt.Second}";
            int ms = dt.Millisecond;
            String fileName = $"{year}{month}{day}{hour}{m}{s}{ms}.mp4";

            FormValueProvider formModel = await Request.StreamFiles(Path.GetFullPath($@"{_hosting.WebRootPath}/video"), fileName);

            Resource resource = new Resource();;

            var bindingSuccessful = await TryUpdateModelAsync(resource, prefix: "",valueProvider: formModel);

            if (!bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    var path = Path.GetFullPath($@"{_hosting.WebRootPath}/video/{fileName}");
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    return Json(new
                    {
                        isOk = false,
                        title = "错误信息",
                        model = resource,
                        message = $"参数错误! 增加错误！"
                    });
                }
            }

            LogPricipalOperation operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.AddResource,
                $"新添资源类型{resource.ResourceType}", "添加新的课程资源");

            if (!_context.Cources.Any(c => c.CourceId == resource.CourceId))
            {
                _context.LogPricipalOperations.Add(operation);
                _context.SaveChanges();
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    message = "资源所属课程不存在或者已经被删除！"
                });
            }

            if (_context.Resources.Any(r => r.Name == resource.Name && r.ResourceType == ResourceType.Vedio))
            {
                var path = Path.GetFullPath($@"{_hosting.WebRootPath}/video/{fileName}");
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                _context.LogPricipalOperations.Add(operation);
                _context.SaveChanges();
                return Json(new
                {
                    isOk = false,
                    title = "错误信息",
                    message = $"这个资源已经具有同名的了！请另外取一个名字吧！"
                });
            }
            else
            {
                resource.Name = resource.Name.Trim();
                resource.ResourceType = ResourceType.Vedio;
                resource.Description = resource.Description.Trim();
                resource.ResourceStatus = ResourceStatus.Normal;
                resource.ResourceUrl = fileName;
                resource.AddTime = DateTime.Now;
                resource.PrincipalId = _analysis.GetLoginUserModel(HttpContext).UserId;
                _context.Resources.Add(resource);
                operation.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                _context.LogPricipalOperations.Add(operation);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    isOk = true,
                    title = "信息提示",
                    message = $"添加成功！"
                });
            }
        }

        /// <summary>
        /// 开启日志记录
        /// </summary>
        /// <param name="rId">资源编号Id</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([Required] int rId)
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

                Resource resource = _context.Resources.Find(rId);
                /*日志记录*/
                LogPricipalOperation operation =
                    _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.DeleteResource, $"删除资源编码:{rId}",
                        "删除课程资源");
                if (resource != null)
                {
                    if (resource.ResourceType == ResourceType.Vedio)
                    {
                        String path = Path.GetFullPath($@"{_hosting.WebRootPath}/video/{resource.ResourceUrl}");
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }

                    _context.Resources.Remove(resource);
                    await _context.SaveChangesAsync().ContinueWith(result =>
                    {
                        operation.PrincpalOperationName = $"删除资源编码:{rId} 资源名称{resource.Name}";
                        operation.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                       _context.LogPricipalOperations.Add(operation);
                       _context.SaveChanges();
                    });
                    return Json(new
                    {
                        isOk = true,
                        title = "消息提示",
                        message = "删除成功！"
                    });
                }
                else
                {
                    _context.LogPricipalOperations.Add(operation);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        message = "资源不存在或者已经被删除了"
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
        /// 不需要日志
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IActionResult Exisit([Required] String name,[Required] ResourceType type)
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

                if (_context.Resources.Any(r => r.Name == name && r.ResourceType == type))
                {
                    return Json(new
                    {
                        isOk = true,
                        isHave = true,
                        title = "错误信息",
                        message = $"资源信息不重复"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = true,
                        isHave = false,
                        title = "错误信息",
                        message = $"这个资源已经具有同名的了！请另外取一个名字吧！"
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
        /// 开启日志记录
        /// </summary>
        /// <param name="rId"></param>
        /// <returns></returns>
        public IActionResult Item([Required] int rId)
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

                Resource r = _context.Resources.Find(rId);
                LogPricipalOperation log =
                    _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.SearchData, $"查询编码:{rId}", "查询课程资源信息");
                if (r != null)
                {
                    log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                    _context.LogPricipalOperations.Add(log);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        resourceId = r.ResourceId,
                        id = r.ResourceId,
                        name = r.Name,
                        addTime = r.AddTime,
                        lengthOfStudy = r.LengthOfStudy,
                        description = r.Description,
                        cource = r.Cource,
                        courceId = r.CourceId,
                        link = r.ResourceUrl,
                        url = $"/video/{r.ResourceUrl}",
                        rType = r.ResourceType,
                        isVideo = r.ResourceType != ResourceType.Link,
                        type = r.ResourceType == ResourceType.Link ? "链接资源" : "视频资源",
                        status = r.ResourceStatus == ResourceStatus.Normal ? "未使用" : "使用中",
                        title = "消息提示",
                        message = "加载成功！"
                    });
                }
                else
                {
                    _context.LogPricipalOperations.Add(log);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = false,
                        error = _analysis.ModelStateDictionaryError(ModelState),
                        title = "错误提示",
                        message = "资源不存在！或者已经被删除了"
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
        /// 开启日志记录
        /// </summary>
        /// <param name="rId">编号</param>
        /// <param name="name">新名称</param>
        /// <param name="link">新连接</param>
        /// <returns></returns>
        public IActionResult Update([Required] int rId,[Required] String name,[Required] String link)
        {
            if (ModelState.IsValid || name.Length > 150 || link.Length >= 400)
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

                Resource r = _context.Resources.Find(rId);
                LogPricipalOperation log =
                    _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.UpdateResource,$"查询编码:{rId}", $"修改链接资源信息 新连接{link}, 修改新名称:{name}");
                if (r != null)
                {
                    try
                    {
                        if (r.ResourceType == ResourceType.Link)
                        {
                            r.ResourceUrl = link.Trim();
                        }
                        r.Name = name.Trim();
                        log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                        _context.LogPricipalOperations.Add(log);
                        _context.SaveChanges();
                        return Json(new
                        {
                            isOk = true,
                            title = "提示信息",
                            message = "修改成功！"
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
                    _context.LogPricipalOperations.Add(log);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = false,
                        error = _analysis.ModelStateDictionaryError(ModelState),
                        title = "错误提示",
                        message = "资源不存在！或者已经被删除了"
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

        public IActionResult All([Required] Int32 cId, [Required] int status, [Required] int type, String content)
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

                var list = _context.Resources.FromSql(sql).Where(r => r.ResourceStatus == ResourceStatus.Normal)
                    .ToList();
                foreach (var val in list)
                {
                    val.ResourceStatus = ResourceStatus.Using;
                }
                _context.SaveChanges();
                return Json(new
                {
                    isOk = true,
                    title = "消息提示",
                    message = "操作成功"
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

        public IActionResult Not([Required] Int32 cId, [Required] int status, [Required] int type, String content)
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

                var list = _context.Resources.FromSql(sql).Where(r => r.ResourceStatus == ResourceStatus.Using)
                    .ToList();
                foreach (var val in list)
                {
                    val.ResourceStatus = ResourceStatus.Normal;
                }
                _context.SaveChanges();
                return Json(new
                {
                    isOk = true,
                    title = "消息提示",
                    message = "操作成功"
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