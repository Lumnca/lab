### [数据库视图设计](#top) :grey_exclamation: <b id="top"></b>
`数据的试图向查询实体类的映射`:white_check_mark:

------

- [x] [`1.InstituteIdView`](#target1)
- [x] [`2.InstituteStudentCountView`](#target2)
- [x] [`3.InstituteToModuleView`](#target3)
- [x] [`4.InstituteWithoutModuleView`](#target4)
- [x] [`5.InstituteStudentNotPassCountView`](#target5)
- [x] [`6.ProfessionView`](#target6)
- [x] [`7.StudentView`](#target7)
- [x] [`8.ExamGradeResultView`](#target8)
- [x] [`9.ExamResultView`](#target9)
- [x] [`10.ReExamApplicationView`](#target10)
- [x] [`11.LogStudentView`](#target11)
- [x] [`12.LearningView`](#target12)
- [x] [`13.CourceView`](#target13)
- [x] [`14.StatisticSingleView,StatisticMultipleView,StatisticJudgeView`](#target14)
- [x] [`15.RandomMultipleView,MultipleRandomView,RandomSingleView`](#target15)
- [x] [`16.UserLoginStatisticView`](#target16)
- [x] [`17.ProgressView`](#target17)


------

#####  :octocat: [1.InstituteView](#top) <b id="target1"></b> 
`概括`:`查询类型 vInstituteMap`
```sql
SELECT dbo.Institute.InstituteId, dbo.Institute.Name, CASE WHEN im.Name IS NULL 
              THEN '暂无所属模块' ELSE im.Name END AS ModuleName, im.ModuleId
FROM dbo.Institute LEFT OUTER JOIN
     (SELECT dbo.InstituteToModules.InstituteToModuleId, dbo.Module.ModuleId, 
             dbo.InstituteToModules.InstituteId, 
             dbo.Module.Name
             FROM 
             dbo.InstituteToModules 
             INNER JOIN
             dbo.Module ON dbo.Module.ModuleId = dbo.InstituteToModules.ModuleId) AS im ON 
             im.InstituteId = dbo.Institute.InstituteId
```
#####  :octocat: [2.InstituteStudentCountView](#top) <b id="target2"></b> 
`概括`:`查询类型 vInstituteStudentCountMap`:`检索每个学院有多少人`
```sql
SELECT   InstituteId, Name, CASE WHEN Scount IS NULL THEN 0 ELSE Scount END AS StuCount
FROM  (SELECT   dbo.Institute.InstituteId, dbo.Institute.Name, c.id, c.Scount
FROM  dbo.Institute LEFT OUTER JOIN
   (SELECT   InstituteId AS id, COUNT(*) AS Scount
    FROM      dbo.Student
    GROUP BY InstituteId) AS c ON c.id = dbo.Institute.InstituteId) AS D
```
#####  :octocat: [3.InstituteToModuleView](#top) <b id="target3"></b> 
`概括`:`查询类型 vInstituteToModuleMap`:`减少每个模块下辖那些学院`
```sql
SELECT dbo.InstituteToModules.InstituteToModuleId, dbo.Institute.InstituteId, 
       dbo.Institute.Name AS InstituteName, 
       dbo.Module.Name AS ModuleName, dbo.Module.AddTime, 
       dbo.Principal.PrincipalId, dbo.Principal.JobNumber, 
       dbo.Principal.Phone, dbo.Principal.Name AS PrincipalName, 
       dbo.Principal.PrincipalStatus, dbo.Module.ModuleId
FROM   dbo.Institute INNER JOIN
       dbo.InstituteToModules ON dbo.Institute.InstituteId = dbo.InstituteToModules.InstituteId 
       INNER JOIN
       dbo.Module ON dbo.InstituteToModules.ModuleId = dbo.Module.ModuleId INNER JOIN
       dbo.Principal ON dbo.Module.PrincipalId = dbo.Principal.PrincipalId
```
#####  :octocat: [4.InstituteWithoutModuleView](#top) <b id="target4"></b> 
`概括`:`查询类型 vInstituteWithoutModuleMap`:`那些学院没有所属模块`
```sql
SELECT InstituteId, Name
FROM   dbo.Institute
WHERE  (InstituteId NOT IN
          (SELECT   InstituteId
           FROM      dbo.InstituteToModules))
```
#####  :octocat: [5.InstituteStudentNotPassCountView](#top) <b id="target5"></b> 
`概括`:`查询类型 vInstituteStudentNotPassCountMap`:`统计每个学院的没有通过考试的学生数量`
```sql
SELECT   InstituteId, Name, CASE WHEN Scount IS NULL THEN 0 ELSE Scount END AS StuCount
FROM   (SELECT   dbo.Institute.InstituteId, dbo.Institute.Name, c.id, c.Scount
FROM   dbo.Institute LEFT OUTER JOIN
     (SELECT   InstituteId AS id, COUNT(*) AS Scount
      FROM      dbo.Student
      WHERE   (IsPassExam = 0)
      GROUP BY InstituteId) AS c ON c.id = dbo.Institute.InstituteId) AS D
```
#####  :octocat: [6.ProfessionView](#top) <b id="target6"></b> 
`概括`:`查询类型 vProfessionMap`
```sql
SELECT  dbo.Professions.ProfessionType, dbo.Professions.Name, 
        dbo.Professions.InstituteId, dbo.Professions.ProfessionId, 
        dbo.Institute.Name AS Expr1
FROM    dbo.Institute INNER JOIN
        dbo.Professions 
        ON dbo.Institute.InstituteId = dbo.Professions.InstituteId
```
#####  :octocat: [7.StudentView](#top) <b id="target7"></b> 
`概括`:`查询类型 vStudentMap`
```sql
SELECT   dbo.Institute.InstituteId, dbo.Institute.Name AS InstituteName, 
  dbo.Professions.Name AS ProfessionName, 
  dbo.Professions.ProfessionType, dbo.Student.StudentId, 
  dbo.Student.Name AS StudentName, dbo.Student.Grade, 
  dbo.Student.Phone, dbo.Student.BirthDate, dbo.Student.Sex, 
  dbo.Student.StudentType, dbo.Student.IsPassExam, 
  dbo.Student.ProfessionId, dbo.Student.Email, dbo.Student.IDNumber, 
  CASE WHEN dbo.InstituteToModules.ModuleId IS NULL 
  THEN 0 ELSE dbo.InstituteToModules.ModuleId END AS ModuleId, 
  CASE WHEN dbo.Module.Name IS NULL 
  THEN '没有模块归属' ELSE dbo.Module.Name END AS ModuleName, dbo.Student.MaxExamScore, 
  dbo.Student.MaxExamCount
FROM      dbo.Module INNER JOIN
  dbo.InstituteToModules ON dbo.Module.ModuleId = dbo.InstituteToModules.ModuleId 
  RIGHT OUTER JOIN
  dbo.Institute INNER JOIN
  dbo.Professions ON dbo.Institute.InstituteId = dbo.Professions.InstituteId INNER JOIN
  dbo.Student ON dbo.Professions.ProfessionId = dbo.Student.ProfessionId ON 
  dbo.InstituteToModules.InstituteId = dbo.Student.InstituteId
```
#####  :octocat: [8.ExamGradeResultView](#top) <b id="target8"></b> 
`概括`:`查询类型 vExamGradeResultMap 统计每一个年级的考试通过情况`
```sql
Create View ExamGradeResultView 
as
Select  
*,
CONVERT(decimal(18,2),PassTotal * 1.0 / Total * 100) as PassTotleRate,
case 
  when UnderCount = 0 then 0.00 
  else Convert(decimal(18,2),(UnderPassCount * 1.0 /UnderCount) * 100) end  as 'UnderPassRate',
case 
  when PostCount = 0 then 0.00 
  else Convert(decimal(18,2),(PostPassCount * 1.0 /PostCount) * 100) end  as 'PostPassRate'
from 
(select 
	Grade,
	count(*) as 'Total',
	sum(case when Student.IsPassExam = 1 then 1 else 0 end ) as 'PassTotal',
	Sum(case when Student.StudentType = 1 then 1  else  0 end) as 'PostCount',
	Sum(case when Student.StudentType = 0 then 1  else  0 end) as 'UnderCount',
	Sum(case when Student.StudentType = 1 and Student.IsPassExam = 1 then 1  else  0 end) 
  as 'PostPassCount',
	Sum(case when Student.StudentType = 0 and Student.IsPassExam = 1 then 1  else  0 end) 
  as 'UnderPassCount'   
from student group by Grade) as c
```
#####  :octocat: [9.ExamResultView](#top) <b id="target9"></b> 
```sql
SELECT   dbo.Institute.InstituteId, dbo.Institute.Name AS InstituteName, 
dbo.Professions.Name AS ProfessionName, 
  dbo.Professions.ProfessionType, dbo.Student.StudentId, dbo.Student.Name AS StudentName,
  dbo.Student.Grade, 
  dbo.Student.Phone, dbo.Student.BirthDate, dbo.Student.Sex, dbo.Student.StudentType, 
  dbo.Student.IsPassExam, 
  dbo.Student.ProfessionId, dbo.Student.Email, dbo.Student.IDNumber, 
  CASE WHEN dbo.InstituteToModules.ModuleId IS NULL 
  THEN 0 ELSE dbo.InstituteToModules.ModuleId END AS ModuleId, 
  CASE WHEN dbo.Module.Name IS NULL 
  THEN '没有模块归属' ELSE dbo.Module.Name END AS ModuleName, dbo.Student.MaxExamScore, 
  dbo.Student.MaxExamCount
FROM   dbo.Module INNER JOIN
  dbo.InstituteToModules ON dbo.Module.ModuleId = dbo.InstituteToModules.ModuleId 
  RIGHT OUTER JOIN
  dbo.Institute INNER JOIN
  dbo.Professions ON dbo.Institute.InstituteId = dbo.Professions.InstituteId INNER JOIN
  dbo.Student ON dbo.Professions.ProfessionId = dbo.Student.ProfessionId ON 
  dbo.InstituteToModules.InstituteId = dbo.Student.InstituteId
```

#####  :octocat: [10.ReExamApplicationView](#top) <b id="target10"></b> 
`重考申请`
```sql
SELECT   dbo.ApplicationForReExaminations.ModuleId, dbo.ApplicationForReExaminations.ApplicationExamId, 
  dbo.ApplicationForReExaminations.StudentId, dbo.ApplicationForReExaminations.InstituteId, 
  dbo.ApplicationForReExaminations.Reason, dbo.ApplicationForReExaminations.Email, 
  dbo.ApplicationForReExaminations.ApplicationStatus, dbo.Student.Name, dbo.Student.Grade, 
  dbo.Student.MaxExamScore, dbo.Student.MaxExamCount, dbo.Student.IsPassExam, 
  dbo.Professions.Name AS ProfessionName, dbo.Student.ProfessionId, dbo.Module.Name AS ModuleName, 
  dbo.Institute.Name AS InstituteName, dbo.Student.Sex, dbo.Student.StudentType, 
  dbo.ApplicationForReExaminations.AddTime
FROM      dbo.ApplicationForReExaminations INNER JOIN
  dbo.Student ON dbo.ApplicationForReExaminations.StudentId = dbo.Student.StudentId INNER JOIN
  dbo.Institute ON dbo.ApplicationForReExaminations.InstituteId = dbo.Institute.InstituteId INNER JOIN
  dbo.Module ON dbo.ApplicationForReExaminations.ModuleId = dbo.Module.ModuleId INNER JOIN
  dbo.Professions ON dbo.Student.ProfessionId = dbo.Professions.ProfessionId
```
#####  :octocat: [11.LogStudentView](#top) <b id="target11"></b> 
```sql
SELECT ID, uTime AS DoTime, content
FROM   (SELECT   ID, LoginTime AS uTime, '登陆了系统' AS content
   FROM      dbo.LogUserLogin
   UNION ALL
   SELECT   StudentId, AddTime, StuOperationContent
   FROM      dbo.LogStudentOperations) AS LogStudent_1
```
#####  :octocat: [12.LearningView](#top) <b id="target12"></b> 
```sql
SELECT   dbo.Learings.CourceId, dbo.Learings.StudentId, dbo.Learings.LearingId,
  dbo.Learings.AddTime, dbo.CourceView.Name, 
  dbo.CourceView.RCount, dbo.CourceView.Introduction, dbo.CourceView.Credit, dbo.CourceView.ModuleId, 
  dbo.CourceView.CourceStatus, dbo.Learings.IsFinish
FROM      dbo.CourceView RIGHT OUTER JOIN
  dbo.Learings ON dbo.CourceView.CourceId = dbo.Learings.CourceId
```
#####  :octocat: [13.CourceView](#top) <b id="target13"></b> 
`课程信息 每一个课程包含的视频资源个数`
```sql
SELECT   dbo.Cources.CourceId, 
  CASE WHEN [ResourceCount] IS NULL THEN 0 ELSE [ResourceCount] END AS RCount, 
  dbo.Cources.Name, dbo.Cources.AddTime, dbo.Cources.Introduction, 
  dbo.Cources.Credit, dbo.Cources.ModuleId, 
  dbo.Cources.CourceStatus
FROM      dbo.Cources LEFT OUTER JOIN
  (SELECT   CourceId, COUNT(*) AS ResourceCount
   FROM      dbo.Resources
   WHERE   (ResourceStatus = 0) AND (ResourceType = 1)
   GROUP BY CourceId) AS Cc ON dbo.Cources.CourceId = Cc.CourceId
```
#####  :octocat: [14.StatisticSingleView,StatisticMultipleView,StatisticJudgeView](#top) <b id="target14"></b> 
`统计试卷的每种题型的做题结果！`
```sql
Create View StatisticSingleView
as
select 
	StudentId,
	PaperId,
	count(*) as SingleCount,
	Cast(Sum(case when RealAnswer = StudentAnswer then Score else 0 end) as real)as TotalScore,
	Score,Sum(case when RealAnswer = StudentAnswer then 1 else 0 end) as RightCount 
from ExamSingleChoices  Group by  ExamSingleChoices.PaperId,Score,StudentId;

go

Create View StatisticMultipleView
as
select 
	StudentId,
	PaperId,
	count(*) as SingleCount,
	Cast(Sum(case when RealAnswer = StudentAnswer then Score else 0 end) as real)as TotalScore,
	Score,Sum(case when RealAnswer = StudentAnswer then 1 else 0 end) as RightCount 
from ExamMultipleChoices  Group by  ExamMultipleChoices.PaperId,Score,StudentId;


go

Create View StatisticJudgeView
as
select 
	StudentId,
	PaperId,
	count(*) as SingleCount,
	Cast(Sum(case when RealAnswer = StudentAnswer then Score else 0 end) as real)as TotalScore,
	Score,Sum(case when RealAnswer = StudentAnswer then 1 else 0 end) as RightCount 
from ExamJudgeChoices  Group by  ExamJudgeChoices.PaperId,Score,StudentId;
```

#####  :octocat: [15.RandomMultipleView,MultipleRandomView,RandomSingleView](#top) <b id="target15"></b> 
`方便出题的视图！`

```sql
go

create view RandomMultipleView 
as
select MultipleId,newid() as RandomId ,ModuleId from MultipleChoices;
  
go
create view MultipleRandomView 
as
select JudgeId,newid() as RandomId ,ModuleId from JudgeChoices;
  
go
create view RandomSingleView 
as
select SingleId,newid() as RandomId ,ModuleId from SingleChoices;
```
#####  :octocat: [16.UserLoginStatisticView](#top) <b id="target16"></b> 
`统计学生登录情况 创建视图语句`
```sql
create View UserLoginStatisticView
as
select count(*) as LoginCount,ateLogin from
(
select *, CONVERT(nvarchar,LoginTime,111) as dateLogin from LogUserLogin
) as s
group by s.dateLogin;

go;
```
#####  :octocat: [17.ProgressView](#top) <b id="target17"></b> 
`学生学习进度 创建视图语句`
```sql
Create view ProgressView
SELECT  dbo.Progresses.ProgressId, dbo.Progresses.StudentId, dbo.Progresses.ResourceId, 
	dbo.Resources.CourceId, 
        dbo.Progresses.StudyTime, dbo.Progresses.AddTime, dbo.Progresses.NeedTime,
	dbo.Resources.ResourceType
FROM    dbo.Progresses INNER JOIN
        dbo.Resources ON dbo.Progresses.ResourceId = dbo.Resources.ResourceId
```
--------------------
`作者:` `KickGod` 
`完成时间`:`2019年3月16日18:36:36`
`备注信息`: `禁止转载` 

