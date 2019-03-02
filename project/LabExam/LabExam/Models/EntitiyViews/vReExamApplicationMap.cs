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
    public class vReExamApplicationMap
    {
        public  int ModuleId { get; set; }

        public int ApplicationExamId { get; set; }

        public String StudentId { get; set; }


        public int InstituteId { get; set; }

        public String Reason { get; set; }

        [EmailAddress, MaxLength(500)]
        public String Email { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

        public String Name { get; set; }

        public Boolean IsPassExam { get; set; }

        public float MaxExamScore { get; set; }

        public int MaxExamCount { get; set; }

        public int Grade { get; set; }

        public String ModuleName { get; set; }

        public String InstituteName { get; set; }

        public int ProfessionId { get; set; }
        
        public String ProfessionName { get; set; }

        public Boolean Sex { get; set; }

        public StudentType StudentType { get; set; }

        public DateTime AddTime { get; set; }
    }
}
