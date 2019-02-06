### [数据库视图设计](#top) :grey_exclamation: <b id="top"></b>
`数据的试图向查询实体类的映射`:white_check_mark:

------

- [x] [`1.InstituteIdView`](#target1)
- [x] [`2.InstituteStudentCountView`](#target2)
- [x] [`3.InstituteToModuleView`](#target3)
- [x] [`4.InstituteWithoutModuleView`](#target4)


------

#####  :octocat: [1.InstituteView](#top) <b id="target1"></b> 
`概括`:`查询类型 vInstituteMap`
```sql
SELECT dbo.Institute.InstituteId, dbo.Institute.Name, CASE WHEN im.Name IS NULL 
              THEN '暂无所属模块' ELSE im.Name END AS ModuleName
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
SELECT InstituteId, Name, CASE WHEN Scount IS NULL THEN 0 ELSE Scount END AS StuCount
FROM  (SELECT dbo.Institute.InstituteId, dbo.Institute.Name, c.id, c.Scount
 FROM dbo.Institute LEFT OUTER JOIN
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
#####  :octocat: [2.](#top) <b id="target2"></b> 
`概括`:`查询类型 v Map`
```sql

```
#####  :octocat: [2.](#top) <b id="target2"></b> 
`概括`:`查询类型 v Map`
```sql

```
#####  :octocat: [2.](#top) <b id="target2"></b> 
`概括`:`查询类型 v Map`
```sql

```
#####  :octocat: [2.](#top) <b id="target2"></b> 
`概括`:`查询类型 v Map`
```sql

```



--------------------
`作者:` `KickGod` 
`完成时间`:`2018年12月31日18:33:38`
`备注信息`: `禁止转载` 
