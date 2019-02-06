using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.EntitiyViews
{
    public class vInstituteWithoutModuleMap
    {
        public int InstituteId { get; set; }

        [MaxLength(80)]
        public String Name { get; set; }
    }
}
