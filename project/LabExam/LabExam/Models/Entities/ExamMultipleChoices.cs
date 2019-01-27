using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExam.Models.Entities
{
    public class ExamMultipleChoices
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamMultipleChoicesId { get; set; }

        [ForeignKey("Paper")]
        public int PaperId { get; set; }

        public virtual ExaminationPaper ExaminationPaper { get; set; }

        [MaxLength(40)]
        public String StudentId { get; set; }

        [ForeignKey("MultipleChoices")]
        public int MultipleId { get; set; }

        public virtual MultipleChoices MultipleChoices { get; set; }

        [MaxLength(10)]
        public String StudentAnswer { get; set; }

        public String RealAnswer { get; set; }

        public float Score { get; set; }
    }
}
