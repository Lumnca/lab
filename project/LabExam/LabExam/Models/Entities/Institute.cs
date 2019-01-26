using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.Entities
{
    public class Institute
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int InstituteId { get; set; }

        [MaxLength(80)]
        public String Name { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; } //编号 

        public virtual Module Module { get; set; }//导航属性

        public virtual List<Profession> Professions { get; set; } //专业
    }
}
