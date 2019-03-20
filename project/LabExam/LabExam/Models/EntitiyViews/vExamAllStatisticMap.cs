using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.EntitiyViews
{
    public class vExamAllStatisticMap
    {
        public int InstituteId { get; set; }
        public String Name { get; set; }
        public int StuNotPassCount { get; set; }

        public int PassScount { get; set; }

        public int AllCount { get; set; }
    }
}
