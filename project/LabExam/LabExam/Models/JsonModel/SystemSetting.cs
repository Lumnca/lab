using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.JsonModel
{
    public class SystemSetting
    {
        public LoginSetting LoginSetting { get; set; }

        public String Version { get; set; } //系统版本

        public Dictionary<Int32, ModuleExamSetting> ExamModuleSettings { get; set; }

        public List<MaintenanceStaff> Staffs;

        
    }
}
