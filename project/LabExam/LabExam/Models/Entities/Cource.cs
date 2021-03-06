﻿using LabExam.Models.Map;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExam.Models.Entities
{
    public class Cource
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int CourceId { get; set; }

        [MaxLength(400)]
        public  String Name { get; set; }

        public DateTime AddTime { get; set; }

        public String Introduction { get; set; }

        public  float Credit { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; } //编号

        public virtual Module Module { get; set; }

        public CourceStatus CourceStatus { get; set; }

        [MaxLength(100)]
        public String PrincipalId { get; set; }
    }
}
