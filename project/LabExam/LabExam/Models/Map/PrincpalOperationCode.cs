using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.Models.Map
{
    public enum PrincpalOperationCode
    {
        AddStudent = 0, //添加学生
        AddResource = 3,
        DeleteStudent = 1,//删除学生
        DealApplication = 2, //处理申请
        DeleteResource = 4,
        SearchData = 5,
        UpdateResource = 6, //修改资源信息
        StopUseResource = 7, //停用资源
        UseResource = 8,//弃用课程资源,
        AddModule = 9,
        InspectJoinApplication = 10, //审核加入考试的申请,
        InspectAllJoinApplicationFail = 11, //审核所有加入考试不通过
        InspectAllJoinApplicationPass = 12,//审核所有加入考试通过
        InspectReExamApplication = 13,//审核重新考试的申请
        InspectAllReExamApplicationFail = 14,//审核重新考试的申请全部不通过
        InspectAllReExamApplicationPass = 15,
        AddJudge = 16, //天机判断题
        UploadInsertStudent = 17,//导入学生信息
        ExportExamData = 18,//导出学生考试信息
        SystemRuntimeError = 19,
        ChangeInstituteToModule = 20,// 将一个学院归属要这个模块
        DeleteInstituteFromModule = 21,// 将一个学院从此模块中排除出去
        ModuleReName = 22, //模块重命名
        ModuleDelete = 23, //删除模块
        InstituteDelete = 24,//删除学院
        InstituteAdd= 25, //添加学院
        InstituteUpdate = 26,//修改学院名称
        ProfessionAdd = 27,//添加专业
        ProfessionUpdate = 28,//修改专业
        ProfessionDelete = 29,//删除专业
        ApplicationForReExaminationsDelete = 30,//删除重考申请
        JudgeDelete = 31, //删除判断题
        JudgeUpdate = 32, //更新判断题
        MultipleAdd= 33,//添加多选题
        MultipleDelete = 34,//删除多选题
        MultipleUpdate = 35, //修改多选题
        SingleAdd = 36,//添加单选题
        SingleDelete = 37,//删除单选题
        SingleUpdate = 39, //修改单选题

    }
}
