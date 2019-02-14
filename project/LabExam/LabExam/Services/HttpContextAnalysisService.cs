using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Map;

namespace LabExam.Services
{
    public class HttpContextAnalysisService: IHttpContextAnalysisService
    {
        private readonly LabContext _context;

        public HttpContextAnalysisService(LabContext context)
        {
            _context = context;
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


    }
}
