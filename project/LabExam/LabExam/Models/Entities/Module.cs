using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LabExam.Models.Entities
{
    [Table("Module")]
    public class Module
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModuleId { get; set; } //编号
        [MaxLength(200)]
        public String Name { get; set; } //名称
        public DateTime AddTime { get; set; } //添加时间

        [ForeignKey("Principal"),MaxLength(100)]
        public String PrincipalId { get; set; }

        public virtual Principal Principal { get; set; }//导航属性

    }
}
