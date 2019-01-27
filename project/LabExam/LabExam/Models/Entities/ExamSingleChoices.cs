using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.Entities
{
    public class ExamSingleChoices
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamSingleChoicesId { get; set; }

        [ForeignKey("Paper")]
        public int PaperId { get; set; }

        public virtual ExaminationPaper ExaminationPaper { get; set; }

        [MaxLength(40)]
        public String StudentId { get; set; }

        [ForeignKey("SingleChoices")]
        public int SingleId { get; set; }

        public virtual SingleChoices SingleChoices { get; set; }

        [MaxLength(10)]
        public String StudentAnswer { get; set; }

        public String RealAnswer { get; set; }

        public float Score { get; set; }
    }
}
