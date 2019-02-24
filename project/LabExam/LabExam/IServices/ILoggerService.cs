using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Entities;
using LabExam.Models.Map;

namespace LabExam.IServices
{
    public interface ILoggerService
    {
        LogPricipalOperation GetDefaultLogPricipalOperation(PrincpalOperationCode code, String target, String content);

        LogStudentOperation GetDefaultLogStudentOperation(StuOperationCode code, String target, String content);

        Task<int> LoggerAsync(LogPricipalOperation operation);

        int Logger(LogPricipalOperation operation);

        Task<int> LoggerAsync(LogStudentOperation operation);

        int Logger(LogStudentOperation operation);

        String FormatDateLocal(DateTime dt);
    }
}
