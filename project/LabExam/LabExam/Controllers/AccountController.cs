using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LabExam.Models;
using LabExam.Models.JsonModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LabExam.Controllers
{
    public class AccountController : Controller
    {

        private readonly IEncryptionDataService _ncryption;
        private readonly LabContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly ILoadConfigFileService _config;

        public AccountController(IEncryptionDataService ncryption, LabContext context, IHttpContextAccessor accessor, IHttpContextAnalysisService analysis, ILoadConfigFileService config)
        {
            _ncryption = ncryption;
            _context = context;
            _accessor = accessor;
            _analysis = analysis;
            _config = config;
        }

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Apply([Bind(include: "StudentId,Reason,Email,IDNumber,InstituteId,ProfessionId,Grade,Phone,Name,BirthDate,Sex,StudentType")] ApplicationJoinTheExamination applicationJoinTheExamination)
        {
            if (ModelState.IsValid)
            {
                if (_context.Student.Any(s=>s.StudentId == applicationJoinTheExamination.StudentId))
                {
                    applicationJoinTheExamination.ApplicationStatus = ApplicationStatus.Submit;
                    _context.ApplicationJoinTheExaminations.Add(applicationJoinTheExamination);
                    _context.SaveChanges();
                    return Json(new
                    {
                        isOk = true,
                        title="提示信息",
                        message = "提交成功！请等待审核通过"
                    });
                }
                else
                {
                    return Json(new
                    {
                        isOk = false,
                        message = "学生不存在或者已经被删除了！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误信息",
                    message = "输入了非法的不合格的参数"
                });
            }
        }
        
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }


        /// <summary>
        ///  用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login([Required] String userId,[Required] String userPassword)
        {   
            if (ModelState.IsValid)
            {
                //判断用户身份
                UserType type = _analysis.GetUserType(userId);
                if (type == UserType.Anonymous) //匿名用户
                {
                    return Json(new
                    {
                        isOk = false,
                        message = "账户不存在！如果你的账号尚未录入请联系系统维护人员录入！",
                    });
                }
                //如果是管理员判断密码是否正确
                if (type == UserType.Principal)
                {
                    Principal principal = _context.Principals.Find(userId);
                    if ( _ncryption.DecryptByRsa(principal.Password) != _ncryption.EncodeByMd5(_ncryption.EncodeByMd5(userPassword)))
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "管理员的密码不正确！",
                        });
                    }
                }
                //如果是学生判断密码是正确
                if (type == UserType.Student)
                {
                    if (_context.Student.Any(stu => stu.Password != _ncryption.EncodeByMd5(_ncryption.EncodeByMd5(userPassword))))
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "同学你的密码不正确！忘记了可以修改密码。",
                        });
                    }
                }
                //判断是否让管理员登录 超级管理员不被禁止登录
                SystemSetting setting = _config.LoadSystemSetting();
                if (type == UserType.Principal)
                {
                    Principal principal = _context.Principals.Find(userId);

                    if (!setting.LoginSetting.PrincipalLogin && principal.PrincipalStatus != PrincipalStatus.Super)
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "系统维护中,管理员请等待系统维护之后进入！",
                        });
                    }
                    //判断此管理员是否已经被禁止
                    if (principal.PrincipalStatus == PrincipalStatus.Ban)
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "管理员,你已经被禁止登录！",
                        });
                    }

                    //验证成功保存信息让其登录
                    LoginUserModel user = new LoginUserModel()
                    {
                        UserId = userId,
                        UserPassword = userPassword,
                        LoginTime = DateTime.Now,
                        UserType = type
                    };
                    var userData = JsonConvert.SerializeObject(user,Formatting.None);
                    ClaimsIdentity identity = new ClaimsIdentity();
                    identity.AddClaim(new Claim(ClaimTypes.Name, principal.Name)); //用户名 姓名
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Principal")); //角色
                    identity.AddClaim(new Claim(ClaimTypes.UserData, userData)); //用户数据
                    ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);

                    AuthenticationProperties property = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(16),//保存 16小时
                        IsPersistent = true
                    };
                    //持久化 Cookie 浏览器关闭了 只有在IsPersistent为True时，才会在写入Cookie指定Expires 
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, property);

                    return Json(new
                    {
                        isOk = true,
                        url = "/Principal/Index",
                        message="登录成功！"
                    });
                }
                else
                {
                    Student student = _context.Student.Find(userId);
                    if (!setting.LoginSetting.StudentLogin)
                    {
                        return Json(new
                        {
                            isOk = false,
                            message = "系统尚未允许学生登录！请等待通知...",
                        });
                    }

                    LoginUserModel user = new LoginUserModel()
                    {
                        UserId = userId,
                        UserPassword = userPassword,
                        LoginTime = DateTime.Now,
                        UserType = type
                    };
                    var userData = JsonConvert.SerializeObject(user, Formatting.None);
                    ClaimsIdentity identity = new ClaimsIdentity();
                    identity.AddClaim(new Claim(ClaimTypes.Name, student.Name)); //用户名 姓名
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Student")); //角色
                    identity.AddClaim(new Claim(ClaimTypes.UserData, userData)); //用户数据
                    ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);

                    AuthenticationProperties property = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(28),//保存28小时
                        IsPersistent = true
                    };
                    //持久化 Cookie 浏览器关闭了 只有在IsPersistent为True时，才会在写入Cookie指定Expires 
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, property);

                    return Json(new
                    {
                        isOk = true,
                        url = "/Student/Index",
                        message = "登录成功！"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = "传递了错误的参数！无法登录",
                    url = "/Error/ParameterError"
                });
            }
        }

        [HttpGet]
        public IActionResult Alter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alter([FromForm,Required] String StudentId,[FromForm, Required] String Password,[FromForm, Required] String IDNumber)
        {
            if (ModelState.IsValid)
            {
                UserType type = _analysis.GetUserType(StudentId);
                if (type == UserType.Anonymous)
                {
                    return RedirectToAction("AlterResult", "Account", new
                    {
                        header = "你的账号不存在,无法修改密码！如果未录入系统,请和管理员联系！ ",
                        rightUrl = "/Account/Alter",
                        rightTitle = "再次修改"
                    });
                }
                if (type == UserType.Principal)
                {
                    return RedirectToAction("AlterResult", "Account", new
                    {
                        header = "管理员无法在此修改密码,如果忘记密码,请联系系统维护人员！ ",
                        rightUrl = "/Account/Alter",
                        rightTitle = "返回修改页面"
                    });
                }
                //如果是学生的话
                Student student =
                    _context.Student.FirstOrDefault(s => s.IDNumber == IDNumber && s.StudentId == StudentId);

                #region 记录学生操作日志

                LogStudentOperation operation = new LogStudentOperation
                {
                    StudentId = StudentId,
                    AddTime = DateTime.Now,
                    OperationIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    StuOperationCode = StuOperationCode.ChangePassword,
                    StuOperationContent = "修改账号密码",
                    StuOperationName = "修改密码",
                    StuOperationStatus = StuOperationStatus.Success,
                    Title = "修改密码",
                    StuOperationType = StuOperationType.Normal
                };

                #endregion

                if (student != null)
                {
                    student.Password = _ncryption.EncodeByMd5(_ncryption.EncodeByMd5(Password));
                    operation.StuOperationStatus = StuOperationStatus.Success;
                    _context.LogStudentOperations.Add(operation);
                    _context.SaveChanges();

                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    
                    return RedirectToAction("AlterResult", "Account", new
                    {
                        header = "恭喜你,已经修改密码！ 需要重新登录账号..... ",
                        rightUrl = "/Account/Login",
                        rightTitle = "立马前去登录"
                    });
                }
                else
                {
                    operation.StuOperationStatus = StuOperationStatus.Fail;
                    _context.LogStudentOperations.Add(operation);
                    _context.SaveChanges();
                    //学生的身份证号码错误
                    return RedirectToAction("AlterResult", "Account", new
                    {
                        header = "修改失败了！ 你的身份证号码输入错误！ ",
                        rightUrl = "/Account/Alter",
                        rightTitle = "返回修改页面"
                    });
                }
            }
            else
            {
                return RedirectToAction("ParameterError", "Error");
            }
        }

        public async Task<IActionResult> LoginOut()
        {
             await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
             return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Log([Required] int terminal, [Required] String uId)
        {
            if (ModelState.IsValid && terminal < 3 && terminal >=0)
            {
                LogUserLogin log = new LogUserLogin
                {
                    ID = uId,
                    Terminal = (Terminal) terminal,
                    LoginIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    LoginTime = DateTime.Now
                };

                _context.LogUserLogin.Add(log);
                await _context.SaveChangesAsync();
            }
            return Json(new
            {
                logResult = true
            });
        }

        [HttpGet]
        public IActionResult UserInfo()
        {
            try
            {
                var result = HttpContext.AuthenticateAsync().Result;
                if (result.Succeeded)
                {
                    String uName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    String uRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                    String uData = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;

                    LoginUserModel user = JsonConvert.DeserializeObject<LoginUserModel>(uData);

                    return Json(new
                    {
                        isLogin = true,
                        Name = uName,
                        Role = uRole,
                        uId = user.UserId
                    });
                }
                else
                {
                    return Json(new
                    {
                        isLogin = false
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IActionResult Result(String header,UserType UType, String urlLeft,String urlRight,String titleLeft,String titleRight)
        {
            ViewBag.Header = header;
            if (UType == UserType.Student)
            {
                ViewBag.urlRight = "/Student/Index";
            }
            else if(UType == UserType.Principal)
            {
                ViewBag.urlRight = "/Principal/Index";
            }
            ViewBag.titleLeft = titleLeft;
            ViewBag.titleRight = titleRight;
            ViewBag.urlLeft = urlLeft;
            return View();
        }

        public IActionResult AlterResult(String header, String rightUrl, String rightTitle)
        {
            ViewBag.Header = header;
            ViewBag.RightUrl = rightUrl;
            ViewBag.RightTitle = rightTitle;
            return View();
        }

    }
}
