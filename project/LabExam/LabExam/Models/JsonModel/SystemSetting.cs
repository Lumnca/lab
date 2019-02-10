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
            ModuleExamSetting msetting = new ModuleExamSetting();
            msetting.AllowExamTime = 3;
            msetting.ExamTime = 120;
            msetting.PassFloat = 60;
            msetting.TotalScore = 100;
            msetting.Multiple = new Multiple()
            {
                Count = 20,
                Score = 2
            };
            msetting.Single = new LabExam.Models.JsonModel.Single()
            {
                Count = 30,
                Score = 1
            };
            msetting.Judge = new Judge()
            {
                Count = 30,
                Score = 1
            };
            return msetting;
        }
    }
}
