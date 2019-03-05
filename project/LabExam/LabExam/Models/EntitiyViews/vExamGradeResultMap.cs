using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.EntitiyViews
{
    public class vExamGradeResultMap
    {
        public Int32 Grade { get; set; }

        public Int32 Total { get; set; }

        public Int32 PassTotal { get; set; }

        public Int32 PostCount { get; set; }

        public Int32 UnderCount { get; set; }
        
        public int UnderPassCount { get; set; }

        public int PostPassCount { get; set; }

        public System.Decimal PassTotleRate { get; set; }

        public System.Decimal PostPassRate { get; set; }

        public System.Decimal UnderPassRate { get; set; }
    }
}
