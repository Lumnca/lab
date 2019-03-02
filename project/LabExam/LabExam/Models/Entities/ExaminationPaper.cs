using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExam.Models.Entities
{
    public class ExaminationPaper
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaperId { get; set; }
        [MaxLength(40)]
        public String StudentId { get; set; }

        public float PassScore { get; set; }

        public float ExamTime { get; set; }

        public float LeaveExamTime { get; set; }

        public float TotleScore { get; set; }

        public DateTime AddTime { get; set; }
        
        public float Score { get; set; }

        public Boolean IsFinish { get; set; }

        public virtual List<ExamSingleChoices> ExamSingleChoices { get; set; } //试卷含有的单选题

        public virtual List<ExamMultipleChoices> ExamMultipleChoices { get; set; } //试卷含有的多选题

        public virtual List<ExamJudgeChoices> ExamJudgeChoices { get; set; } //判断题
    }
}
