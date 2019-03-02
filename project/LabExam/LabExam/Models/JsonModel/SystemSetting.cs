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

        public static ModuleExamSetting GetDefault()
        {
            return new ModuleExamSetting
            {
                AllowExamTime = 3,
                ExamTime = 120,
                PassFloat = 60,
                TotalScore = 100,
                Multiple = new Multiple() {Count = 20, Score = 2},
                Single = new LabExam.Models.JsonModel.Single() {Count = 30, Score = 1},
                Judge = new Judge() {Count = 30, Score = 1}
            };
        }
    }
}
