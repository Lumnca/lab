using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.JsonModel
{
    /// <summary>
    /// 模块考试出题设置
    /// </summary>
    public class ModuleExamSetting
    {
        /// <summary>
        /// 允许考试次数
        /// </summary>
        public Int32 AllowExamTime { get; set; }
        /// <summary>
        /// 所属模块
        /// </summary>
        public Int32 ModuleId { get; set; }

        public String ModuleName { get; set; }

        /// <summary>
        /// 通过分数
        /// </summary>
        public float PassFloat { get; set; }

        /// <summary>
        /// 允许考试时间
        /// </summary>
        public float ExamTime { get; set; }
        
        /// <summary>
        /// 总分
        /// </summary>
        public float TotalScore { get; set; }

        public Single Single { get; set; }

        public Multiple Multiple { get; set; }

        public Judge Judge { get; set; }
    }
}
