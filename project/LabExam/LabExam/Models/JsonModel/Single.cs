using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.JsonModel
{
    /// <summary>
    /// 单选题 出多少个 每一个多少分
    /// </summary>
    public class Single
    {
        public  int Count { get; set; }

        public  float Score { get; set; }
    }
}
