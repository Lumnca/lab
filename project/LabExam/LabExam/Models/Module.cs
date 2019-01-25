using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LabExam.Models
{
   [Table("Modules")]
    public class Module
    {
        [Key]
        public int ModuleID { get; set; } //编号
        [MaxLength(200)]
        public String Name { get; set; } //名称
        public DateTime AddTime { get; set; } //添加时间

    }
}
