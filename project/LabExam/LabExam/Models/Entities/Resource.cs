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

        [MaxLength(200)]
        public  String Name { get; set; }

        [ForeignKey("Cource")]
        public int  CourceId { get; set; }

        public  virtual Cource Cource { get; set; }

        public  ResourceType ResourceType { get; set; }

        [Column("Description", TypeName ="ntext")]
        public  String Description { get; set; }

        public  float LengthOfStudy { get; set; }
        
        public ResourceStatus ResourceStatus { get; set; }
    }
}
