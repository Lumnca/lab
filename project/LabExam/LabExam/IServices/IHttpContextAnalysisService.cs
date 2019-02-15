using LabExam.Models;
using LabExam.Models.Map;
using System;
using LabExam.Models.JsonModel;
using Microsoft.AspNetCore.Http;

namespace LabExam.IServices
{
    public interface IHttpContextAnalysisService
    {
        UserType GetUserType(String userId);

        LoginUserModel GetLoginUserModel(HttpContext httpContext);

        PrincipalConfig GetLoginUserConfig(HttpContext httpContext);
    }
}
