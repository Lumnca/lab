using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.JsonModel
{
    public class LoginSetting
    {
        public Boolean StudentLogim { get; set; }

        public Boolean PrincipalLogin { get; set; }

        public Boolean IsOpenExam { get; set; }

        public Boolean AllowPastJoinExam { get; set; }
    }
}
