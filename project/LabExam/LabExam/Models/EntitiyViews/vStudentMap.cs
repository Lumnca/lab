using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Entities;
using LabExam.Models.Map;

namespace LabExam.Models.EntitiyViews
{
    public class vStudentMap
    {
        public int InstituteId { get; set; }

        [MaxLength(80)]
        public String InstituteName { get; set; }

        public int ProfessionId { get; set; }

        [MaxLength(80)]
        public String ProfessionName { get; set; }

        public ProfessionType ProfessionType { get; set; }

        public String StudentId { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [MaxLength(800)]
        public String IDNumber { get; set; }

        [MaxLength(80)]
        public String StudentName { get; set; }

        public int Grade { get; set; }

        [MaxLength(200)]
        public String Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public Boolean Sex { get; set; }

        public StudentType StudentType { get; set; }

        [MaxLength(300), EmailAddress]
        public String Email { get; set; }

        public Boolean IsPassExam { get; set; }

        /// <summary>
        /// 最好成绩
        /// </summary>
        public float MaxExamScore { get; set; }
        /// <summary>
        /// 允许最大考试次数
        /// </summary>
        public int MaxExamCount { get; set; }

        public String ModuleName { get; set; }

        public int ModuleId { get; set; }
    }
}
