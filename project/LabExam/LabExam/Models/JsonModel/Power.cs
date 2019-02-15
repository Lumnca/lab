using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.JsonModel
{
    public class Power
    {
        public Boolean StudentManager {get; set;} 
        public Boolean ExamManager { get; set;}
        public Boolean CourcesManager { get; set;}
        public Boolean QuestionBankManager { get; set;}
        public Boolean SystemSettingManager { get; set;} 
        public Boolean SystemInfoManager { get; set;}
        public Boolean SystemManager { get; set; } 

        public Power()
        {
            StudentManager = true;
            ExamManager = false;
            CourcesManager = false;
            QuestionBankManager = false;
            SystemSettingManager = false;
            SystemInfoManager = false;
            SystemManager = false;
        }
    }
}
