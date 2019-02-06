using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.Models.Entities
{
    public class Institute
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int InstituteId { get; set; }

        [MaxLength(80)]
        public String Name { get; set; }

    }
}
