using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.JsonModel
{
    public class PrincipalConfig
    {
        public String PrincipalId { get; set; }

        public Power Power { get; set; }

        public DateTime SettingTime { get; set; }

    }
}
