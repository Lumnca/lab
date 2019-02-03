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
using Microsoft.AspNetCore.Http;

namespace LabExam.Controllers
{
    public class AccountController : Controller
    {

        private readonly IEncryptionDataService _ncryption;
        private readonly LabContext _context;
        private IHttpContextAccessor _accessor;

        public AccountController(IEncryptionDataService ncryption, LabContext context, IHttpContextAccessor accessor)
        {
            _ncryption = ncryption;
            _context = context;
            _accessor = accessor;
        }

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Apply([Bind(include: "StudentId,Reason,Email,IDNumber,InstituteId,ProfessionId,Grade,Phone,Name,BirthDate,Sex,StudentType")] ApplicationJoinTheExamination applicationJoinTheExamination)
        {
            return Json(applicationJoinTheExamination);
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
        /// <param name="UserId"></param>
        /// <param name="UserPassword"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login([FromForm] String UserId,[FromForm] String UserPassword, string returnUrl = null)
        {
            var list = new List<dynamic> {
                new { Name ="蒋星" ,UserName = "2016110418", Password = "111111", Role = "Student" },
                new { Name ="六神丸",UserName = "2017110418", Password = "222222", Role = "Principal" }
            };
            var user = list.SingleOrDefault(s => s.UserName == UserId && s.Password == UserPassword);

            if (user != null)
            {
                JObject userDate = new JObject();
                userDate["Uid"] = UserId;
                userDate["Password"] = UserPassword;

                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, "蒋星")); //用户名 姓名
                identity.AddClaim(new Claim(ClaimTypes.Role, "Principal")); //角色
                identity.AddClaim(new Claim(ClaimTypes.UserData, userDate.ToString())); //用户数据
                ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);

                AuthenticationProperties property = new AuthenticationProperties();
                property.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(3);
                property.IsPersistent = true; 
                //持久化 Cookie 浏览器关闭了 只有在IsPersistent为True时，才会在写入Cookie指定Expires 
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, property);

                var result = HttpContext.AuthenticateAsync().Result;
                if (result?.Principal != null) HttpContext.User = result.Principal;

                 UserType type = UserType.Student;
                 return RedirectToAction("Result", "Account", new
                 {
                     header = "恭喜你成功登陆！ 即将跳转去用户功能主页.......",
                     UType = type,
                     urlLeft = "/Index",
                     urlRight = "/Student/Index",
                     titleLeft = "返回系统首页",
                     titleRight = "前往学生主页"
                 });
            }
            else
            {
                return RedirectToAction("Result", "Account", new
                {
                    header = "登陆失败！ 你的密码错误....",
                    UType = UserType.Anonymous,
                    urlLeft = "/Account/Login",
                    urlRight = "/Account/Alter",
                    titleLeft = "返回重新登录",
                    titleRight = "修改密码"
                });
            }
        }

        [HttpGet]
        public IActionResult Alter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alter([FromForm] String StudentId,[FromForm] String Password,[FromForm] String IDNumber)
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("AlteResult", "Account", new
            {
                header = "恭喜你,已经修改密码！ 需要重新登录账号..... ",
                rightUrl = "/Account/Login",
                rightTitle = "立马前去登录"
            });
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
                LogUserLogin log = new LogUserLogin();
                log.ID = uId;
                log.Terminal = (Terminal)terminal;
                log.LoginIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();  
                log.LoginTime = DateTime.Now;
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

                    JObject user = JObject.Parse(uData);

                    return Json(new
                    {
                        isLogin = true,
                        Name = uName,
                        Role = uRole,
                        uId = user["Uid"]
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

        /// <summary>
        ///  存在返回 true 不存在 返回 false
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult IsExisit([Required] String userId)
        {
            var list = new List<dynamic> {
                new { Name ="蒋星" ,UserName = "2016110418", Password = "111111", Role = "Student" },
                new { Name ="六神丸",UserName = "2017110418", Password = "222222", Role = "Pricipal" }
            };
            Boolean not = list.SingleOrDefault(s => s.UserName == userId) != null;
            return Json(new
            {
                IsExisit = not
            });
        }

        public IActionResult Result(String header,UserType UType, String urlLeft,String urlRight,String titleLeft,String titleRight)
        {
            ViewBag.Header = header;
            if (UType == UserType.Student)
            {
                ViewBag.urlRight = "/Student/Index";
            }
            else if(UType == UserType.Pricipal)
            {
                ViewBag.urlRight = "/Principal/Index";
            }
            ViewBag.titleLeft = titleLeft;
            ViewBag.titleRight = titleRight;
            ViewBag.urlLeft = urlLeft;
            return View();
        }

        public IActionResult AlteResult(String header, String rightUrl, String rightTitle)
        {
            ViewBag.Header = header;
            ViewBag.RightUrl = rightUrl;
            ViewBag.RightTitle = rightTitle;
            return View();
        }

    }
}
