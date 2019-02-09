using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.JsonModel
{
    public class ModuleExamSetting
    {
        public Int32 AllowExamTime { get; set; }
        public Int32 ModuleId { get; set; }

        public String ModuleName { get; set; }

        public float PassFloat { get; set; }

        public float ExamTime { get; set; }
        
        public float TotalScore { get; set; }

        public Single Single { get; set; }

        public Multiple Multiple { get; set; }

        public Judge Judge { get; set; }
    }
}
