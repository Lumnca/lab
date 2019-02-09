using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.JsonModel
{
    /// <summary>
    /// 多选题 出多少个 每一个多少分
    /// </summary>
    public class Multiple
    {
        public int Count { get; set; }

        public float Score { get; set; }
    }
}
