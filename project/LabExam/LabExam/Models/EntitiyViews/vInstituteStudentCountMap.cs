using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.EntitiyViews
{
    public class vInstituteStudentCountMap
    {
        public Int32 InstituteId { get; set; }

        [MaxLength(80)]
        public String Name { get; set; }

        public int StuCount { get; set; }
    }
}
