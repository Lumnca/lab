using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.Models.Entities
{
    public class LogStudentOperation
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogStudentOperationId { get; set; }

        [MaxLength(80)]
        public String StudentId { get; set; }

        public  StuOperationCode StuOperationCode { get; set; }

        public StuOperationStatus StuOperationStatus { get; set; }

        [MaxLength(40)]
        public String  StuOperationName { get; set; }

        public StuOperationType StuOperationType { get; set; }

        public DateTime AddTime { get; set; }

        [Column("StuOperationContent",TypeName = "nvarchar(300)")]
        public String StuOperationContent { get; set; }

        [MaxLength(50)]
        public String OperationIp { get; set; }

        public String Title { get; set; }

    }
}
