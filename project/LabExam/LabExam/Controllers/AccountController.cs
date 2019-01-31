using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using LabExam.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabExam.Controllers
{
    public class AccountController : Controller
    {

        private readonly IEncryptionDataService _ncryption;

        public AccountController(IEncryptionDataService ncryption)
        {
            _ncryption = ncryption;
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public  IActionResult Login([FromForm] String UserId,[FromForm] String UserPassword, string returnUrl = null)
        {
            var list = new List<dynamic> {
                new { UserName = "2016110418", Password = "111111", Role = "Student" },
                new { UserName = "2017110418", Password = "222222", Role = "Pricipal" }
            };

            var user = list.SingleOrDefault(s => s.UserName == UserId && s.Password == UserPassword);

            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name,UserId));
                identity.AddClaim(new Claim(ClaimTypes.Role, "Student"));
                identity.AddClaim(new Claim(ClaimTypes.UserData, UserPassword));
                ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);

                AuthenticationProperties Property = new AuthenticationProperties();
                Property.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(3);
                Property.IsPersistent = true; //持久化 Cookie 浏览器关闭了 只有在IsPersistent为True时，才会在写入Cookie指定Expires 


                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, Property);

                var result = HttpContext.AuthenticateAsync().Result;
                if (result?.Principal != null) HttpContext.User = result.Principal;

                return Content("登录成功");
            }
            else
            {
                return Content("登录失败");
            }
        }

        [HttpPost("LoginOut")]
        public async Task<IActionResult> LoginOut()
        {
             await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
             return RedirectToAction("Index", "Home");

        }

        public IActionResult Ses()
        {
           String id = HttpContext.Session.GetString("uid");
           String upd = HttpContext.Session.GetString("upd");

           return Json(new
           {
               isLogin = id != null,
               UserId = id,
               Pwd = upd          
           });
        }

        public IActionResult Ses2()
        {

            String Uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            String URole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            String Password = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
            return Json(new
            {
                isLogin = Uid != null,
                UserId = Uid,
                Role = URole,
                UserData= Password
            });
        }
    }
}
