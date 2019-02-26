using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.Models.EntitiyViews
{
    public class vLearningMap
    {
        // ReSharper disable once IdentifierTypo
        public int CourceId { get; set; }

         public String StudentId { get; set; }

         // ReSharper disable once IdentifierTypo
         public int LearingId { get; set; }

         public DateTime AddTime { get; set; }

        public String Name { get; set; }

        public int RCount { get; set; }

        public  String Introduction { get; set; }

        public float Credit { get; set; }

        public int ModuleId { get; set; }
        
        // ReSharper disable once IdentifierTypo
        public  CourceStatus CourceStatus { get; set; }

        public Boolean IsFinish { get; set; }

    }
}
