using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.Models
{
    public class LoginUserModel
    {
        public String UserId { get; set; }

        public String UserPassword { get; set; }

        public UserType UserType { get; set; }

        public DateTime LoginTime { get; set; }
    }
}
