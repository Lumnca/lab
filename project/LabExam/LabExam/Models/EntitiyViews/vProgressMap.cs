using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Entities;
using LabExam.Models.Map;

namespace LabExam.Models.EntitiyViews
{
    public class vProgressMap
    {
        public int ProgressId { get; set; }

        public String StudentId { get; set; }

        public int ResourceId { get; set; }

        public int CourceId { get; set; }

        public float StudyTime { get; set; }

        public float NeedTime { get; set; }

        public DateTime AddTime { get; set; }

        public ResourceType ResourceType { get; set; }
    }
}
