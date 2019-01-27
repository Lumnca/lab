using LabExam.Models.Map;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExam.Models.Entities
{
    [Table("Principal")]
    public class Principal
    {
        [MaxLength(100),Key]
        public String PrincipalId { get; set; }

        [MaxLength(600)]
        public  String Password { get; set; }

        [MaxLength(50)]
        public  String  JobNumber { get; set; }

        [MaxLength(100)]
        public  String Name { get; set; }
        [MaxLength(100)]
        public  String Phone { get; set; }

        public PrincipalStatus PrincipalStatus { get; set; }

        [MaxLength(300)]
        public  String PrincipalConfig { get; set; }

    }
}
