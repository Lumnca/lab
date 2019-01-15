### [四川师范大学安全考试数据库和中间数据设计](#top) :speech_balloon: <b id="top"></b>

-----
`版本`:`1.0.1`

- [x] [`1.学生模块表设计`](#stu)
    - [x] [`1.4 模块表`](#tabmod)  
    - [x] [`1.4 学院表`](#tabins)  
    - [x] [`1.4 专业表`](#tabpro)  
    - [x] [`1.4 学生表`](#tabstu)  
- [x] [`2.管理员模块设计`](#admin)
    - [x] [`2.1 管理员表`](#tabadm)    
- [x] [`3.课程模块设计`](#cources)
    - [x] [`3.1 课程表`](#cour)
    - [x] [`3.2 资源表`](#res)
    - [x] [`3.4 学习课程表`](#learing)
    - [x] [`3.5 学习进度记录表`](#study)
- [x] [`4.题库管理设计`](#bank)
    - [x] [`4.1 单选题表`](#only)
    - [x] [`4.2 多选题表`](#many)
    - [x] [`4.3 判断题表`](#judge)
    - [x] [`4.4 试卷表`](#paper)
    - [x] [`4.5 单选题仓库`](#onlyStore)
    - [x] [`4.6 多选题仓库`](#manyStore)
    - [x] [`4.7 判断题仓库`](#judgeStore)
- [x] [`5.申请管理设计`](#appli)
    - [x] [`5.1 重考申请表`](#examagainapp)
    - [x] [`5.2 加入考试计划申请表`](#again)
- [x] [`6.系统信息设计`](#system)
    - [x] [`6.1 系统开发信息配置`](#sysopen)
- [x] [`7.系统日志设计`](#log)
    - [x] [`7.1 学生操作日志记录`](#logstu)
    - [x] [`7.2 管理员操作日志记录`](#logpri)
    - [x] [`7.3 用户登录日志记录`](#loglogin)
-----
#####  :octocat: [1.学生模块表设计](#top) <b id="stu"></b> 
`核心信息为教务处发送过来的信息！保持不变`


##### [模块表 Module](#top)  <b id="tabmod"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`ModuleId`|`int`|`模块ID 主键`|
|`Name`|`varchar(80)`|`模块名称`|
|`AddTime`|`DateTime`|`添加时间`|
|`PrincipalId`|`varchar(200)`|`添加人员`|

* `系统的基本单位是模块,每个模块学习资源不同,考试内容不同,达标要求不同`
* `学院分属在不同的模块内部,并且开发考试学习期间不允许修改学院所属模块`

##### [学院表 Institute](#top)  <b id="tabins"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`InstituteId`|`int`|`学院ID 主键`|
|`Name`|`varchar(80)`|`学院名称`|
|`ModuleId`|`int`|`所属模块 [外键]`|


##### [专业表 Profession](#top)  <b id="tabpro"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`ProfessionId`|`int`|`专业ID 主键`|
|`ProfessionType`|`int`|`专业类型 研究生或者本科生 映射为枚举`|
|`Name`|`varchar(80)`|`专业名称`|
|`InstituteId`|`int`|`所属学院ID [外键]`|


##### [学生表 Student](#top)  <b id="tabstu"></b> 

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`StudentId`|`varchar(40)`|`学生学号 主键 not null`|
|`Password`|`varchar(440)`|`学生密码,使用MD5 反复加密`|
|`IDNumber`|`varchar(800)`|`身份证号码 对称加密`|
|`InstituId`|`int`|`学院ID [外键]`|
|`Name`|`varchat(80)`|`学生姓名`|
|`Grade`|`int`|`学生年级 判断是否本届`|
|`Phone`|`varchar(200)`|`手机号码`|
|`ProfessionId`|`int`|`专业ID [外键]`|
|`BirthDate`|`DateTime`|`出生日期`|
|`Sex`|`bit`|`性别`|
|`StudentType`|`int`|`研究生/本科生 映射为枚举`|
|`Email`|`varchar(300)`|`QQ邮件`|
|`IsPassExam`|`bit`|`是否已经通过考试 不允许通过非法方式修改`|
|`MaxExamScore`|`float`|`考试最高分数`|
|`MaxExamCount`|`float`|`最大考试次数`|


* `最大考试次数初始化为系统规定数量,可以通过申请重考增加最大考试次数,对于重修学生,即非本届学生,如果其最大考试超过正常数量,那么全部重置,重修学生
重制除了教务处信息的所有信息`
* `考试分数取最大值最高分`

* `IsPassExam 只有通过了考试才能设置为 true 不能够手动修改`
* `研究生本科生只是在统计的时候有用,其他情况下本条信息无用,系统对于研究生本科生一视同仁！`

#####  :octocat: [2.管理员模块设计](#top) <b id="admin"></b> 
`管理员复制后台管理,具有权限配置 json文件`


##### [管理员表 Principal](#top)  <b id="tabadm"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`PrincipalID`|`varchar(100)`|`管理员【又称为负责人】ID [主键]`|
|`Password`|`varchar(600)`|`密码 加密使用 MD5 反复加密`|
|`JobNumber`|`varchar(800)`|`工号`|
|`Name`|`varchar(100)`|`姓名`|
|`Phone`|`varchar(100)`|`电话号码`|
|`PrincipalStatus`|`int`|`用户状态`|
|`PrincipalConfige`|`varchar(360)`|`用户配置文件路径`|

`默认权限配置文件内容`
```json
{
   "PrincipalId":"200204156",
   "power":{
     "StudentManager":true,
     "ExamManager":false,
     "CourcesManager":false,
     "QuestionBankManager":false,
     "SystemSettingManager":false,
     "SystemInfoManager":false,
     "SystemManager":false  
   },
   "SettingTime":"2018-5-6 15:45"
}
```

#####  :octocat: [3.课程模块设计](#top) <b id="cources"></b>
`课程资源学习有关的表`

##### [课程表 Cource](#top)  <b id="cour"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`CourceId`|`int`|`课程ID [主键]`|
|`Name`|`varchar(300)`|`课程名称`|
|`PrincipalID`|`varchar(200)`|`添加人员`|
|`AddTime`|`DateTime`|`添加时间`|
|`Introduction`|`varchar(1000)`|`课程简介`|
|`Credit`|`float`|`学分`|
|`ModuleID`|`int`|`模块ID [外键]`|
|`CourceStatus`|`int`|`课程状态 映射为 [枚举]`|

```c#
public enum CourceStatus{
  Using = 0, //使用中
  Normal = 1 //未使用正常状态
}
```
    
##### [资源表 Resource](#top)  <b id="res"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`ResourceId`|`int`|`资源ID [主键]`|
|`Name`|`int`|`资源名称`|
|`CourceId`|`int`|`所属课程`|
|`ResourceType`|`int`|`资源类型 枚举`|
|`Link`|`varchar(600)`|`资源链接`|
|`Description`|`text`|`资源描述`|
|`LengthOfStudy`|`float`|`学习时长`|
|`ResourceStatus`|`int`|`资源状态`|

```c#
public enum ResourceType{
  Link = 0, //链接文件 
  Vedio = 1 //视频文件
}

public enum ResourceStatus{
  Using = 0, //使用中
  Normal = 1 //未使用正常状态
}
```

##### [学习课程表 Learing](#top)  <b id="learing"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`LearingID`|`int`|`自动增加 主键`|
|`StudentId`|`varchar(40)`|`学号`|
|`CourceId`|`int`|`课程号`|
|`AddTime`|`DateTime`|`添加时间`|

##### [学习进度记录表 Progress](#top)  <b id="study"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`ProgressID`|`int`|`自动增加 主键`|
|`StudentId`|`varchar(40)`|`学号`|
|`ResourceId`|`int`|`资源ID `|
|`StudyTime`|`float`|`已经学习的时长`|
|`NeedTime`|`float`|`需要的学习时间`|
|`AddTime`|`DateTime`|`学习任务安排时间`|

#####  :octocat: [4.题库管理权限](#top) <b id="bank"></b>

##### [单选题表 SingleChoices](#top)  <b id="only"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`SingleId`|`int`|`单选题ID [主键]`|
|`ModuleId`|`int`|`模块ID 所属模块`|
|`Content`|`text`|`题干`|
|`Answer`|`varchar(10)`|`答案`|
|`DegreeOfDifficulty`|`float?`|`难度系数`|
|`AddTime`|`DateTime`|`添加时间`|
|`PrincipalID`|`varchar(200)`|`题目添加人`|
|`Count`|`int`|`选项数量 最多八个`|
|`A`|`varchar(1000)`|`选项内容`|
|`B`|`varchar(1000)`|`选项内容`|
|`C`|`varchar(1000)`|`选项内容`|
|`D`|`varchar(1000)`|`选项内容`|
|`E`|`varchar(1000)`|`选项内容`|
|`F`|`varchar(1000)`|`选项内容`|
|`G`|`varchar(1000)`|`选项内容`|
|`H`|`varchar(1000)`|`选项内容`|


##### [多选题表 MultipleChoices](#top)  <b id="many"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`MultipleId`|`int`|`单选题ID [主键]`|
|`ModuleId`|`int`|`模块ID 所属模块`|
|`Content`|`text`|`题干`|
|`Answer`|`varchar(10)`|`答案`|
|`DegreeOfDifficulty`|`float?`|`难度系数`|
|`AddTime`|`DateTime`|`添加时间`|
|`PrincipalID`|`varchar(200)`|`题目添加人`|
|`Count`|`int`|`选项数量 最多八个`|
|`A`|`varchar(1000)`|`选项内容`|
|`B`|`varchar(1000)`|`选项内容`|
|`C`|`varchar(1000)`|`选项内容`|
|`D`|`varchar(1000)`|`选项内容`|
|`E`|`varchar(1000)`|`选项内容`|
|`F`|`varchar(1000)`|`选项内容`|
|`G`|`varchar(1000)`|`选项内容`|
|`H`|`varchar(1000)`|`选项内容`|

##### [判断题表 JudgeChoices](#top)  <b id="judge"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`JudgeId`|`int`|`判断题ID [主键]`|
|`ModuleId`|`int`|`模块ID 所属模块`|
|`Content`|`text`|`题干`|
|`Answer`|`varchar(10)`|`答案`|
|`DegreeOfDifficulty`|`float?`|`难度系数`|
|`AddTime`|`DateTime`|`添加时间`|
|`PrincipalID`|`varchar(200)`|`题目添加人`|
|`A`|`varchar(1000)`|`选项内容`|
|`B`|`varchar(1000)`|`选项内容`|


##### [试卷表 ExaminationPaper](#top)  <b id="paper"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`PaperId`|`int`|`试卷ID`|
|`StudentId`|`varchar(40)`|`学生学号 那个学生的试卷 not null`|
|`PassScore`|`float`|`通过分数`|
|`ExamTime`|`float`|`考试时间`|
|`LeaveExamTime`|`float`|`剩余考试时间`|
|`TotleScore`|`float`|`总分`|
|`AddTime`|`DateTime`|`添加时间`|
|`ModuleId`|`int`|`模块ID 所属模块`|

##### [单选题仓库 ExamSingleChoices](#top)  <b id="onlyStore"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`ExamSingleChoicesId`|`int`|`主键`|
|`StudentId`|`varchar(40)`|`学号`|
|`SingleId`|`int`|`单选题ID`|
|`StudentAnswer`|`varchar(40)`|`学生的答案`|
|`RealAnswer`|`varchar(40)`|`题目答案`|
|`Score`|`float`|`题目分数`|
 

##### [多选题仓库 ExamMultipleChoices](#top)  <b id="manyStore"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`ExamSingleChoicesId`|`int`|`主键`|
|`StudentId`|`varchar(40)`|`学号`|
|`MultipleId`|`int`|`单选题ID`|
|`StudentAnswer`|`varchar(40)`|`学生的答案`|
|`RealAnswer`|`varchar(40)`|`题目答案`|
|`Score`|`float`|`题目分数`|

##### [判断题仓库 ExamJudgeChoices](#top)  <b id="judgeStore"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`ExamSingleChoicesId`|`int`|`主键`|
|`StudentId`|`varchar(40)`|`学号`|
|`JudgeId`|`int`|`学号`|
|`StudentAnswer`|`varchar(40)`|`学生的答案`|
|`RealAnswer`|`varchar(40)`|`题目答案`|
|`Score`|`float`|`题目分数`|

#####  :octocat: [5.申请管理设计](#top) <b id="appli"></b>
* `数据库中已经有信息的往届学生,不用申请重考！再次加入考试重新安排学习任务,删除以往信息记录`

##### [重考申请表 ApplicationForReExamination](#top)  <b id="examagainapp"></b>
`申请重新考试`

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`ApplicationExamId`|`int`|`主键`|
|`StudentId`|`varchar(40)`|`学号`|
|`ModuleId`|`int`|`所属模块`|
|`InstituteId`|`int`|`所属学院`|
|`Reason`|`text`|`申请原因 不少于一百字`|
|`ApplicationStatus`|`int`|`枚举映射`|
|`Email`|`varchar(500)`|`通过邮箱`|


```c#
public enum ApplicationStatus{
  Submit = 0, //已经提交
  Pass = 1, // 申请成功
  Fail = 2 // 申请失败
}
```

##### [加入考试计划申请表 ApplicationJoinTheExamination](#top)  <b id="again"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`ApplicationJoinId`|`int`|`主键`|
|`StudentId`|`varchar(40)`|`学号`|
|`ModuleId`|`int`|`所属模块`|
|`Reason`|`text`|`申请原因 不少于一百字`|
|`Email`|`varchar(500)`|`通过邮箱`|
|`IDNumber`|`varchar(800)`|`身份证号码 对称加密`|
|`InstituId`|`int`|`学院ID [外键]`|
|`Grade`|`int`|`学生年级 判断是否本届`|
|`Phone`|`varchar(200)`|`手机号码`|
|`ProfessionId`|`int`|`专业ID [外键]`|
|`Name`|`varchat(80)`|`学生姓名`|
|`BirthDate`|`DateTime`|`出生日期`|
|`Sex`|`bit`|`性别`|
|`StudentType`|`int`|`研究生/本科生 映射为枚举`|
|`ApplicationStatus`|`int`|`枚举映射`|

`每一次申请结果要以邮件的形式通知申请者`

#####  :octocat: [6.系统信息设计](#top) <b id="system"></b>
`系统信息用于json配置 我们将配置开放设置,版本 `
##### [系统开放信息配置 SystemSetting](#top)  <b id="sysopen"></b>
```node
{
  "loginSetting":{
     "studentLogin":true, /*是否允许学生登录*/
     "principalLogin":true, //允许管理员登录
     "openExam":false, //允许学生可以考试
     "allowPastJoinExam":false //允许往届学习考试
  },
  "version":"2019.0.0.1859.12", //系统版本
  "footer":"© All Right Reserved . 四川师范大学 版权所有 (四川师范大学计算机科学学院505实验室制) 长期维护",
  "maintenanceStaff":[  //系统维护人员信息
    {
       "name":"蒋星",
       "phone":"15982690985",
       "qq":"1427035242"
    },
    {
       "name":"Jking",
       "phone":"15982690985",
       "qq":"1427035242"
    }
  ],
  "examMoudleSetting":[  //每一个模块的考试设置
      {
         "moduleId":1,
         "moduleName":"模块名称1",  //模块名称
         "passScore":60,    //通过分数
         "ExamTime":100,    //考试时长
         "IsOpen":false,    //是否开放考试
         "AlgorithmNumber":1, //算法编号
         "single":{
            "count": 30,
            "score":1
         },
         "multiple":{
            "count": 20,
            "score":2
         },
         "judge":{
            "count": 30,
            "score":1
         },
         "TotalScore":100
      }, 
      {
         "moduleId":2,
         "moduleName":"模块名称2",
         "passScore":70,
         "ExamTime":100,
         "IsOpen":false,
         "AlgorithmNumber":1,
         "single":{
            "count": 30,
            "score":1
         },
         "multiple":{
            "count": 20,
            "score":2
         },
         "judge":{
            "count": 30,
            "score":1
         },
         "TotalScore":100
      }
    ]
}

```
#####  :octocat: [7.系统日志设计](#top) <b id="log"></b>

##### [学生操作日志记录 LogStudentOperation](#top)  <b id="logstu"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`LogStudentOperationId`|`int`|`主键`|
|`StudentId`|`varchar(100)`|`学号`|
|`StuOperationCode`|`int`|`枚举`|
|`StuOperationStatus`|`int`|`操作结果 `|
|`StuOperationName`|`varchar(30)`|`操作名称`|
|`StuOperationType`|`int`|`操作类型`|
|`AddTime`|`DateTime`|`操作时间`|
|`StuOperationContent`|`varchar(400)`|`操作内容`|
|`OperationIp`|`varchar(100)`|`操作IP`|
|`Title`|`varchar(200)`|`操作评语 例如 第一次登录 第一次观看视频 第一次... 达成什么成就`|

```c#
public enum StuOperationCode{
  WatchVideo = 0, //观看视频
  ChangePassword = 1, // 修改密码
  JoinExam = 2, // 参加考试
  BindEmail = 4 ,// 绑定邮箱
  PassExam =8, //通过考试
}

public enum StuOperationStatus{
  Fail = 0, //成功
  Success = 1 //失败
}

public enum StuOperationType{
  Normal = 0,  //普通操作
  Achieved = 1 //成就操作
}
```

##### [管理员操作日志记录 LogPricipalOperation](#top)  <b id="logpri"></b>

|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`LogPricipalOperationId`|`int`|`主键`|
|`PrincipalID`|`varchar(100)`|`管理员【又称为负责人】ID`|
|`OperationIp`|`varchar(100)`|`操作IP`|
|`AddTime`|`DateTime`|`操作时间`|
|`PrincpalOperationCode`|`int`|`操作类型`|
|`PrincpalOperationName`|`varchar(200)`|`操作名称`|
|`PrincpalOperationContent`|`varchar(700)`|`操作内容`|
|`PrincpalOperationStatus`|`int`|`操作结果 `|

```c#

public enum PrincpalOperationCode{
    AddStudent = 0,
    DelelteStudent = 1,
    DealApplication = 2,
    ...
}

public enum PrincpalOperationStatus{
  Fail = 0, //成功
  Success = 1 //失败
}

```

##### [用户登录日志记录 LogUserLogin](#top)  <b id="loglogin"></b>


|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`LogUserLoginId`|`int`|`主键`|
|`ID`|`varchar(100)`|`登录ID`|
|`LoginTime`|`DateTime`|`登录时间 只记录到几点`|
|`LoginDate`|`Date`|`登录日期`|
|`LoginIp`|`varchar(100)`|`Ip地址`|
|`Terminal`|`int`|`登录终端`|


```c#
public enum Terminal{
  Phone = 0,  // 手机
  TabletPC = 1, //平板
  PC = 2 //电脑
}
```
