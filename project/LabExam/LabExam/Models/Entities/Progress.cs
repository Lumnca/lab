using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.Entities
{
    public class Progress
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgressId { get; set; }

        [ForeignKey("Student"),MaxLength(40)]
        public String StudentId { get; set; }

        public  virtual  Student Student { get; set; }

        [ForeignKey("Resource")]
        public int ResourceId { get; set; }

        public  virtual  Resource Resource { get; set; }

        public float StudyTime { get; set; }

        public  float NeedTime { get; set; }

        public  DateTime AddTime { get; set; }
    }
}
