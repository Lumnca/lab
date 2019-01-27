using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.Models.Entities
{
    public class LogPricipalOperation
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int LogPricipalOperationId { get; set; }

        [MaxLength(100)]
        public String PrincipalId { get; set; }

        [MaxLength(50)]
        public String OperationIp { get; set; }

        public DateTime AddTime { get; set; }

        public PrincpalOperationCode PrincpalOperationCode { get; set; }

        [MaxLength(40)]
        public String PrincpalOperationName { get; set; }

        [MaxLength(300)]
        public String PrincpalOperationContent { get; set; }

        public PrincpalOperationStatus PrincpalOperationStatus { get; set; }


    }
}
