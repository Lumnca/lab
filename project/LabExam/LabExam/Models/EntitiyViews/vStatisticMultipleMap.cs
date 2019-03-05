using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.EntitiyViews
{
    public class vStatisticMultipleMap
    {
        public String StudentId { get; set; }
        public int PaperId { get; set; }
        public int AllCount { get; set; }
        public float Score { get; set; }
        public float TotalScore { get; set; }
        public int RightCount { get; set; }
    }
}
