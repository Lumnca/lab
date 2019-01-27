using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LabExam.Models.Entities
{
    public class Learing
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int  LearingId { get; set; }

        [ForeignKey("Student"),MaxLength(40)]
        public String StudentId { get; set; }

        [ForeignKey("Cource")]
        public int CourceId { get; set; }

        public DateTime AddTime { get; set; }

        public virtual  Student Student { get; set; }

        public  virtual Cource Cource { get; set; }
    }
}
