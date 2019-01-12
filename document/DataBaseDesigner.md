### [四川师范大学安全考试数据库设计](#top) :speech_balloon: <b id="top"></b>

-----
`版本`:`1.0.0`

- [x] [`1.学生模块表设计`](#stu)
    - [x] [`1.4 模块表`](#tabmod)  
    - [x] [`1.4 学院表`](#tabins)  
    - [x] [`1.4 专业表`](#tabpro)  
    - [x] [`1.4 学生表`](#tabstu)  
-----
#####  :octocat: [1.学生模块表设计](#top) <b id="stu"></b> 
`核心信息为教务处发送过来的信息！保持不变`


##### [模块表 Module](#top)  <b id="tabstu"></b>
|`字段`|`类型`|`说明`|
|:----|:-----|:------|
|`ModuleID`|`int`|`模块ID 主键`|
|`Name`|`varchar(80)`|`模块名称`|

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
|`Identity`|`int`|`研究生/本科生`|
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
