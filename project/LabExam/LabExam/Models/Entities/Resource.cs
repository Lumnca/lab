using LabExam.Models.Map;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabExam.Models.Entities
{
    public class Resource
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResourceId { get; set; }

        [MaxLength(500)]
        public  String Name { get; set; }

        [ForeignKey("Cource")]
        public int  CourceId { get; set; }

        public  virtual Cource Cource { get; set; }

        public  ResourceType ResourceType { get; set; }

        [Column("Description", TypeName ="ntext")]
        public  String Description { get; set; }

        public  float LengthOfStudy { get; set; }

        [MaxLength(1000)]
        public String ResourceUrl { get; set; }
        
        public ResourceStatus ResourceStatus { get; set; }


        [MaxLength(100)]
        public String PrincipalId { get; set; }

        public DateTime AddTime { get; set; }
    }
}
