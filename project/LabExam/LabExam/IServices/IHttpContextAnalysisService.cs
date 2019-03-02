using LabExam.Models;
using LabExam.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using LabExam.Models.JsonModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LabExam.IServices
{
    public interface IHttpContextAnalysisService
    {
        UserType GetUserType(String userId);

        LoginUserModel GetLoginUserModel(HttpContext httpContext);

        PrincipalConfig GetLoginUserConfig(HttpContext httpContext);

        List<string> ModelStateDictionaryError(ModelStateDictionary modelState);

        Boolean TryConvertDateTime(String waitConvert, out DateTime result);
    }
}
