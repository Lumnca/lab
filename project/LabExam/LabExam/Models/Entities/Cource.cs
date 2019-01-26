using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.Models.Entities
{
    public class Cource
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int CourceId { get; set; }

        [MaxLength(300)]
        public  String Name { get; set; }

        [MaxLength(100),ForeignKey("Principal")]
        public String PrincipalId { get; set; }

        public virtual Principal Principal { get; set; }

        public DateTime AddTime { get; set; }

        public String Introduction { get; set; }

        public  float Credit { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; } //编号

        public virtual Module Module { get; set; }

        public CourceStatus CourceStatus { get; set; }

    }
}
