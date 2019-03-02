using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.EntitiyViews
{
    public class vExamResultMap
    {
        public int Grade { get; set; }

        public String Name { get; set; }

        public int InstituteId { get; set; }

        public int Total { get; set; }

        public int PassTotal { get; set; }

        public int PostCount { get; set; }

        public int UnderCount { get; set; }

        public int PostPassCount { get; set; }

        public int PostNotPassCount { get; set; }

        public int UnderPassCount { get; set; }

        public int UnderNotPassCount { get; set; }

        public float TotalPassRate { get; set; }

        public int PostPassRate { get; set; }

        public float UnderPassRate { get; set; }
    }
}
