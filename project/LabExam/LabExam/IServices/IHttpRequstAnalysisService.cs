using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LabExam.IServices
{
    public interface IHttpRequstAnalysisService
    {
        String HttpRequstIPAddress(HttpRequest request);

    }
}
