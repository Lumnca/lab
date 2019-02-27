using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models;
using LabExam.Models.JsonModel;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace LabExam.Services
{
    public class HttpContextAnalysisService: IHttpContextAnalysisService
    {
        private readonly LabContext _context;
        private readonly ILoadConfigFileService _config;

        public HttpContextAnalysisService(LabContext context, ILoadConfigFileService config)
        {
            _context = context;
            _config = config;
        }

        public UserType GetUserType(String userId)
        {
            if (_context.Student.Any(s => s.StudentId == userId))
            {
                return UserType.Student;
            }
            else if(_context.Principals.Any(p => p.PrincipalId == userId))
            {
                return UserType.Principal;
            }
            else
            {
                return UserType.Anonymous;
            }
        }

        public LoginUserModel GetLoginUserModel(HttpContext httpContext)
        {
            try
            {
                var result = httpContext.AuthenticateAsync().Result;
                if (result.Succeeded)
                {
                    String uData = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;

                    LoginUserModel user = JsonConvert.DeserializeObject<LoginUserModel>(uData);
                    user.UserId = user.UserId.Trim();
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public PrincipalConfig GetLoginUserConfig(HttpContext httpContext)
        {
            try
            {
                LoginUserModel user = GetLoginUserModel(httpContext);
                if (user == null)
                {
                    throw new Exception("用户尚未登录成功！");
                }
                PrincipalConfig setting = _config.LoadPrincipalConfig(user.UserId);
                return setting;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public List<string> ModelStateDictionaryError(ModelStateDictionary modelState)
        {
            List<string> sb = new List<string>();
            List<string> Keys = modelState.Keys.ToList();
            foreach (var key in Keys)
            {
                var errors = modelState[key].Errors.ToList();
                //将错误描述添加到sb中
                foreach (var error in errors)
                {
                    sb.Add(error.ErrorMessage);
                }
            }
            return sb;
        }
    }
}
