using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.JsonModel
{
    public class ExamOpenSetting
    {
        public String ModuleName { get; set; }

        public int ModuleId { get; set; }

        public Boolean IsOpen { get; set; }
    }
}
