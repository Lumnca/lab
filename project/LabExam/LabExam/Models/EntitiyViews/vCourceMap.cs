using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models.Map;

namespace LabExam.Models.EntitiyViews
{
    /// <summary>
    ///  视图: 统计每一个课程具有的在用的视频资料数目 RCount
    /// </summary>
    // ReSharper disable once IdentifierTypo
    public class vCourceMap
    {

        // ReSharper disable once IdentifierTypo
        public int CourceId { get; set; }

        public String Name { get; set; }

        public DateTime AddTime { get; set; }

        public String Introduction { get; set; }

        public float Credit { get; set; }

        public int ModuleId { get; set; } //编号

        // ReSharper disable once IdentifierTypo
        public CourceStatus CourceStatus { get; set; }

        public int RCount { get; set; }
    }
}
