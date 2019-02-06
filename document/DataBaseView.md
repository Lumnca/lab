### [数据库视图设计](#top) :grey_exclamation: <b id="top"></b>
`文章简单介绍概括`:white_check_mark:

------

- [x] [`1.InstituteIdView`](#target1)
- [x] [`2.InstituteStudentCountView`](#target2)
- [x] [`3.title3`](#target3)

------

#####  :octocat: [1.InstituteIdView](#top) <b id="target1"></b> 
`概括`:`查询类型 vInstituteIdMap`
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
#####  [1.1 little-title](#top) <b id="little1"></b> 
`概括`
...

#####  :octocat: [2.title2](#top) <b id="target2"></b> 
`概括`
...

#####  :octocat: [3.title3](#top) <b id="target3"></b> 
`概括`
...



--------------------
`作者:` `模板` 
`完成时间`:`2018年12月31日18:33:38`
`备注信息`: `禁止转载` 
