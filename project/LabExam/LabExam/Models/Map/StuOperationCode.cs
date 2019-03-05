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
        PassExam = 8 ,//通过考试,
        ChangeEmail =  10, //修改邮箱
        ChangePhone = 12 , //修改手机
        DeleteReExamApplication = 14,//删除未审核申请
        StudyCourse = 15,//学习视频资料
        FinishCourseBySelf = 16,//自己手动完成课程学习
        EnterTheExamAgain = 17, //再次进入考试
        IllegalAttack = 18,//非法攻击
        DisruptingExamination = 19,//扰乱考试 企图干扰考试时间
        GiveUpTheExam = 20,//放弃考试
        SubmitExam = 30,//提交考试
        SubmitExamBySystem = 31,//系统帮他提交了考试
    }
}
