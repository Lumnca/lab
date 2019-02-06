using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.Models.EntitiyViews
{
    public class vProfessionMap
    {
        public int ProfessionId { get; set; }

        public int InstituteId { get; set; }

        public ProfessionType ProfessionType { get; set; }

        [MaxLength(80)]
        public String Name { get; set; }

        [MaxLength(80)]
        public String InstituteName { get; set; }
    }
}
