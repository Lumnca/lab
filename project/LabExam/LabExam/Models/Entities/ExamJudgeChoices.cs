using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LabExam.Models.Entities
{
    public class ExamJudgeChoices
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamJudgeChoicesId { get; set; }

        [ForeignKey("Paper")]
        public int PaperId { get; set; }

        public virtual ExaminationPaper ExaminationPaper { get; set; }

        [MaxLength(40)]
        public String StudentId { get; set; }

        public int JudgeId { get; set; }

        [MaxLength(10)]
        public String StudentAnswer { get; set; }

        public String RealAnswer { get; set; }

        public float Score { get; set; }
    }
}
