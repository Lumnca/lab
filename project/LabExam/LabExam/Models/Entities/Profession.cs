using LabExam.Models.Map;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExam.Models.Entities
{
    public class Profession
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfessionId { get; set; }

        public  ProfessionType ProfessionType { get; set; }

        [MaxLength(80)]
        public  String Name { get; set; }

        [ForeignKey("Institute")]
        public int InstituteId { get; set; }

        public virtual Institute Institute { get; set; }

        public virtual List<Student> Students { get; set; } //学生
    }
}
