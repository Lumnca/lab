using LabExam.Models.Map;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExam.Models.Entities
{
    public class ApplicationForReExamination
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationExamId { get; set; }

        [MaxLength(40),ForeignKey("Student")]
        public String StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int ModuleId { get; set; }

        public int InstituteId { get; set; }

        [Column("Reason", TypeName = "ntext")]
        public String Reason { get; set; }

        [EmailAddress,MaxLength(500)]
        public String Email { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
