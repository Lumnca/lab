using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.IServices;
using Microsoft.AspNetCore.Http;

namespace LabExam.Services
{
    public class HttpRequstAnalysisService: IHttpRequstAnalysisService
    {
        public string HttpRequstIPAddress(HttpRequest request)
        {
            return "";
        }
    }
}
