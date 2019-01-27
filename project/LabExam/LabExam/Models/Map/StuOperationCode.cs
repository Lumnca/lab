using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.Map
{
    public enum StuOperationCode
    {
        WatchVideo = 0, //观看视频
        ChangePassword = 1, // 修改密码
        JoinExam = 2, // 参加考试
        BindEmail = 4,// 绑定邮箱
        PassExam = 8 //通过考试
    }
}
