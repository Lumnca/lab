using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Http;

namespace LabExam.Services
{
    public class LoggerService:ILoggerService
    {
        private readonly IHttpContextAnalysisService _analysis;
        private readonly IHttpContextAccessor _accessor;
        private readonly LabContext _context;
        public LoggerService(IHttpContextAnalysisService analysis, IHttpContextAccessor accessor, LabContext context)
        {
            _analysis = analysis;
            _accessor = accessor;
            _context = context;
        }

        public LogPricipalOperation GetDefaultLogPricipalOperation(PrincpalOperationCode code,String target,String content)
        {
            LogPricipalOperation operation = new LogPricipalOperation
            {
                AddTime = DateTime.Now,
                OperationIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                PrincpalOperationCode = code,
                PrincipalId = _analysis.GetLoginUserModel(_accessor.HttpContext).UserId,
                PrincpalOperationStatus = PrincpalOperationStatus.Fail,
                PrincpalOperationName = target,
                PrincpalOperationContent = content
            };
            //天生失败
            return operation;
        }



        int ILoggerService.Logger(LogPricipalOperation operation)
        {
            _context.LogPricipalOperations.Add(operation);
            return _context.SaveChanges();
        }


        public async Task<int> LoggerAsync(LogPricipalOperation operation)
        {
            _context.LogPricipalOperations.Add(operation);
            return  await _context.SaveChangesAsync();
        }

    }
}
