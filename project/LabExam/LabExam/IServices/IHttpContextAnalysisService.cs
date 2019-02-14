using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.IServices
{
    public interface IHttpContextAnalysisService
    {
        UserType GetUserType(String userId);
    }
}
