### [四川师范大学安全考试数据库设计](#top) :speech_balloon: <b id="top"></b>

-----
`版本`:`1.0.0`

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
- [x] [`4.题库管理权限`](#bank)
    - [x] [`4.1 单选题表`](#only)
    - [x] [`4.2 多选题表`](#many)
    - [x] [`4.3 判断题表`](#judge)
    - [x] [`4.4 试卷表`](#paper)
    - [x] [`4.5 单选题仓库`](#onlyStore)
    - [x] [`4.6 多选题仓库`](#manyStore)
    - [x] [`4.7 判断题仓库`](#judgeStore)
    
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
|`Phone`|`varchar(200)`|`手机号码`|
|`ProfessionId`|`int`|`专业ID [外键]`|
|`BirthDate`|`DateTime`|`出生日期`|
|`Sex`|`bit`|`性别`|
|`StudentType`|`int`|`研究生/本科生 映射为枚举`|
|`Email`|`varchar(300)`|`QQ邮件`|
|`IsPassExam`|`bit`|`是否已经通过考试 不允许通过非法方式修改`|
|`MaxExamScore`|`float`|`考试最高分数`|
|`MaxExamCount`|`float`|`最大考试次数`|
|`Grade`|`int`|`学生年级 判断是否本届`|

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
|`PrincipalID`|`varchar(200)`|`管理员【又称为负责人】ID [主键]`|
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
     "SystemInfoManager":false
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




