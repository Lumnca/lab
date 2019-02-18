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
    public class ResourceController : Controller
    {

        private readonly LabContext _context;
        private readonly IEncryptionDataService _encryption;
        private readonly IHostingEnvironment _hosting;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly IHttpContextAccessor _accessor;

        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public ResourceController(LabContext context, IEncryptionDataService encryption, IHostingEnvironment hosting, IHttpContextAnalysisService analysis, IHttpContextAccessor accessor)
        {
            _context = context;
            _encryption = encryption;
            _hosting = hosting;
            _analysis = analysis;
            _accessor = accessor;
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
        [GenerateAntiforgeryTokenCookieForAjax]
        public IActionResult Create()
        {
            List<Cource> cources = _context.Cources.ToList();
            return View(cources);
        }

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
                        message = "你并无题库管理操作权限"
                    });
                }
                #region 功能实现区域
                if (_context.Cources.Any(c => c.CourceId == resource.CourceId))
                {
                    if (_context.Resources.Any(r => r.Name == resource.Name))
                    {
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
                    _context.Resources.Add(resource);
                    await _context.SaveChangesAsync().ContinueWith( async result =>
                    {
                        LogPricipalOperation operation = new LogPricipalOperation
                        {
                            PrincipalId = _analysis.GetLoginUserModel(HttpContext).UserId,
                            AddTime = DateTime.Now,
                            OperationIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                            PrincpalOperationCode = PrincpalOperationCode.AddResource,
                            PrincpalOperationName = $"增加资源{resource.Name}",
                            PrincpalOperationStatus = PrincpalOperationStatus.Success,
                            PrincpalOperationContent = $"添加资源ID {resource.ResourceId}"
                        };
                        await _context.LogPricipalOperations.AddAsync(operation);
                        await _context.SaveChangesAsync();
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

        public async Task<IActionResult> Stop([Required] int rId)
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

                Resource resource = _context.Resources.Find(rId);
                if (resource != null)
                {
                    resource.ResourceStatus = ResourceStatus.Normal;
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

        public async Task<IActionResult> Use([Required] int rId)
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

                Resource resource = _context.Resources.Find(rId);
                if (resource != null)
                {
                    resource.ResourceStatus = ResourceStatus.Using;
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

        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {

            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return Json(new
                {
                    isOk = false,
                    title ="错误信息",
                    message = $"Expected a multipart request, but got {Request.ContentType}"
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

            var bindingSuccessful = await TryUpdateModelAsync(resource, prefix: "",
                valueProvider: formModel);

            if (!bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误信息",
                        model = resource,
                        message = $"参数错误"
                    });
                }
            }

            return Json(new
            {
                isOk = false,
                model = resource,
                title = "错误信息",
                message = $"sdddd"
            });


        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
    }

}