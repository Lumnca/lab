using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.EntitiyViews
{
    /// <summary>
    ///  视图语句
    /// 
    /// </summary>
    public class vInstituteMap
    {
        public int InstituteId { get; set; }

        [MaxLength(80)]
        public String Name { get; set; }

        [MaxLength(200)]
        public String ModuleName { get; set; } //名称

        public int ModuleId { get; set; } //编号
    }

}
