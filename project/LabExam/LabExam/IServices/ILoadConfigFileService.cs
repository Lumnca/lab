using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.JsonModel;

namespace LabExam.IServices
{
    public interface ILoadConfigFileService
    {
        SystemSetting LoadSystemSetting();

        void ReWriteSystemSetting(SystemSetting setting);

        Dictionary<int, ExamOpenSetting> LoadModuleExamOpenSetting();

        void ReWriteModuleExamOpenSetting(Dictionary<int, ExamOpenSetting> setting);
    }
}
