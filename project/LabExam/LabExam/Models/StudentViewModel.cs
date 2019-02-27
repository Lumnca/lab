using LabExam.DataSource;
using LabExam.Models.Entities;
using System;
using System.Linq;

namespace LabExam.Models
{
    public class StudentViewModel
    {

        public StudentViewModel()
        {

        }

        public StudentViewModel(Student student,LabContext context)
        {
            if (student == null)
            {
                throw new Exception("传入的学生为null");
            }
            this.Name = student.Name;
            this.LearningTime = context.Progresses.Where(s => s.StudentId == student.StudentId).Sum(v => v.StudyTime);
            this.MaxScore = student.MaxExamScore;
            this.ExamTime = context.ExaminationPapers.Count(p => p.StudentId == student.StudentId);
            this.Sex = student.Sex ? "男" : "女";
            this.ApplicationTime = context.ApplicationForReExaminations.Count(a => a.StudentId == student.StudentId);
            this.StudentId = student.StudentId;
            this.Grade = student.Grade;
            this.IsPass = student.IsPassExam ? "Yes" : "No";
            this.StudentType = student.StudentType == Map.StudentType.PostGraduate ? "研究生" : "本科生";
            this.HeadPortrait = student.Sex ? "student_nan.png" : "student_nv.png";
            this.Email = student.Email;
            this.InstituteName = context.Institute.Find(student.InstituteId).Name;
            this.ProfessionName = context.Professions.Find(student.ProfessionId).Name;
            this.Phone = student.Phone;
        }
        public String Phone { get; set; } 
        public String InstituteName { get; set; }

        public String ProfessionName { get; set; }

        public String StudentId { get; set; }

        public String Name { get; set;}

        public String Sex { get; set; }

        public int Grade { get; set; }

        public String StudentType { get; set; }

        public float LearningTime { get; set; }

        public float MaxScore { get; set; }

        public String HeadPortrait { get; set; }

        public String IsPass { get; set; }

        public int ExamTime { get; set; }

        public Int32 ApplicationTime { get; set; }

        public String Email { get; set; }

    }
}
