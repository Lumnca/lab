using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

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
            StringBuilder builder = new StringBuilder("p4:");
            builder.Append(_accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            builder.Append("/p6");
            builder.Append(_accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString());
            return new LogPricipalOperation
            {
                AddTime = DateTime.Now,
                OperationIp = builder.ToString(),
                PrincpalOperationCode = code,
                PrincipalId = _analysis.GetLoginUserModel(_accessor.HttpContext).UserId,
                PrincpalOperationStatus = PrincpalOperationStatus.Fail,
                PrincpalOperationName = target,
                PrincpalOperationContent = content
            };
        }

        public LogStudentOperation GetDefaultLogStudentOperation(StuOperationCode code, String target, String content)
        {
            StringBuilder builder = new StringBuilder("p4:");
            builder.Append(_accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            builder.Append("/p6");
            builder.Append(_accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString());
            return new LogStudentOperation
             {
                 OperationIp = builder.ToString(),
                 AddTime = DateTime.Now,
                 StuOperationCode = code,
                 StuOperationStatus = StuOperationStatus.Fail,
                 StuOperationType = StuOperationType.Normal,
                 StuOperationContent = content,
                 StudentId = _analysis.GetLoginUserModel(_accessor.HttpContext).UserId,
                 StuOperationName = target
             };

        }

        public int Logger(LogPricipalOperation operation)
        {
            _context.LogPricipalOperations.Add(operation);
            return _context.SaveChanges();
        }

        public async Task<int> LoggerAsync(LogPricipalOperation operation)
        {
            _context.LogPricipalOperations.Add(operation);
            return  await _context.SaveChangesAsync();
        }

        public int Logger(LogStudentOperation operation)
        {
            _context.LogStudentOperations.Add(operation);
            return _context.SaveChanges();
        }

        public async Task<int> LoggerAsync(LogStudentOperation operation)
        {
            _context.LogStudentOperations.Add(operation);
            return await _context.SaveChangesAsync();
        }

        public String FormatDateLocal(DateTime dt)
        {
            int year = dt.Year;
            String month = dt.Month > 9 ? dt.Month.ToString() : $"0{dt.Month}";
            String day = dt.Day > 9 ? dt.Day.ToString() : $"0{dt.Day}";
            String hour = dt.Hour > 9 ? dt.Hour.ToString() : $"0{dt.Hour}";
            String m = dt.Minute > 9 ? dt.Minute.ToString() : $"0{dt.Minute}";
            String s = dt.Second > 9 ? dt.Second.ToString() : $"0{dt.Second}";
            return $"{year}/{month}/{day} {hour}:{m}:{s}";
        }
    }
}
