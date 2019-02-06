using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.Models.EntitiyViews
{
    public class vInstituteToModuleMap
    {

        public Int32 InstituteToModuleId { get; set; }


        public Int32 InstituteId { get; set; }

        [MaxLength(80)]
        public String InstituteName { get; set; }

        public int ModuleId { get; set; } //名称

        [MaxLength(200)]
        public String ModuleName { get; set; } //名称

        public DateTime AddTime { get; set; } //添加时间

        [MaxLength(100)]
        public String PrincipalId { get; set; }

        [MaxLength(50)]
        public String JobNumber { get; set; } //工号

        [MaxLength(100)]
        public String Phone { get; set; }

        [MaxLength(100)]
        public String PrincipalName { get; set; }

        public PrincipalStatus PrincipalStatus { get; set; }

    }
}
