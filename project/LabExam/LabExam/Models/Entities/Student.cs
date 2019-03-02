using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LabExam.Models.Map;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LabExam.Models.Entities
{
    public class Student
    {
        [Key,MaxLength(40)]
        public String StudentId { get; set; }

        [MaxLength(440)]
        public String Password { get; set; }
        [MaxLength(800)]
        public String IDNumber { get; set; }

        public int InstituteId { get; set; }

        [MaxLength(80)]
        public String Name { get; set; }

        public  int Grade { get; set; }

        [MaxLength(200)]
        public  String Phone { get; set; }


        [ForeignKey("Profession")]
        public int ProfessionId { get; set; }

        public virtual Profession Profession { get; set; }

        public  DateTime BirthDate { get; set; }

        public Boolean Sex { get; set; }

        public  StudentType StudentType { get; set; }

        [MaxLength(300), EmailAddress]
        public String Email { get; set; }
        public Boolean  IsPassExam { get; set; }

        public  float MaxExamScore { get; set; }

        public  int MaxExamCount { get; set; }

    }
}
