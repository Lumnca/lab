using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.Models.Entities
{
    public class ApplicationJoinTheExamination
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationJoinId { get; set; }

        [MaxLength(40)]
        public String StudentId { get; set; }

        [Column("Reason",TypeName = "ntext")]
        public String Reason { get; set; }

        [EmailAddress,MaxLength(300)]
        public String Email { get; set; }

        [MaxLength(300)]
        public String IDNumber { get; set; }

        public int InstituteId { get; set; }

        public int ProfessionId { get; set; }

        public int Grade { get; set; }

        [MaxLength(100)]
        public  String Phone { get; set; }

        [MaxLength(80)]
        public String Name { get; set; }

        public DateTime BirthDate { get; set; }

        public Boolean Sex { get; set; }

        public  StudentType StudentType { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
