﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExam.Models.Entities
{
    public class InstituteToModule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstituteToModuleId { get; set; }
        
        [ForeignKey("Module")]
        public int ModuleId { get; set; } //编号

        [ForeignKey("Institute")]
        public int InstituteId { get; set; } //学院

        public Module Module { get; set; }

        public Institute Institute { get; set; }

    }
}
