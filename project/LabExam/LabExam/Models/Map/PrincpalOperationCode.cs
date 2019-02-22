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
        DelelteStudent = 1,//删除学生
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
    }
}
