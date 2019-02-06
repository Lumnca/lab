using LabExam.Models.Map;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExam.Models.Entities
{
    public class LogUserLogin
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  LogUserLoginId { get; set;  }

        [MaxLength(60)]
        public String ID { get; set; }

        public DateTime LoginTime { get; set; }

        [MaxLength(100)]
        public String LoginIp { get; set; }

        public Terminal Terminal { get; set; }
    }
}
