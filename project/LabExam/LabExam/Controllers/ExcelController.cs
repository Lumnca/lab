using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models;
using LabExam.Models.Entities;
using LabExam.Models.EntitiyViews;
using LabExam.Models.JsonModel;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using OfficeOpenXml;

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]                                         
    public class ExcelController : Controller
    {
        private readonly LabContext _context;
        private readonly ILoggerService _logger;
        private readonly IHostingEnvironment _hosting; 
        private readonly ILoadConfigFileService _config;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly IEncryptionDataService _encryption;
        private readonly IFileHandleService _fileHandle;
        public ExcelController(LabContext context, ILoggerService logger, ILoadConfigFileService config, IHttpContextAnalysisService analysis, IEncryptionDataService encryption, IHostingEnvironment hosting, IFileHandleService fileHandle)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _analysis = analysis;
            _encryption = encryption;
            _hosting = hosting;
            _fileHandle = fileHandle;
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert([Required] IFormFile excelFileInfo, Boolean isAllowAddInstitute = false, Boolean isAllowAddProfession = false)
        {
            if (ModelState.IsValid)
            {
                if (!_analysis.GetLoginUserConfig(HttpContext).Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误",
                        message = "你并无学生管理操作权限"
                    });
                }

                if ( Path.GetExtension(excelFileInfo.FileName).Trim() != ".xlsx")
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        error = Path.GetExtension(excelFileInfo.FileName).Trim(),
                        uploadCount = 0,
                        repeatCount = 0,
                        postCount = 0,
                        underCount = 0,
                        Error = 0.ToString(),
                        success = 0,
                        newProfession = 0,
                        newInstitute = 0,
                        extension = false,
                        message = "请上传 Excel2007/2010 或以上版本的Excel 文件！后缀为 xlsx"
                    });
                }

                var operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.UploadInsertStudent,$"上传Excel文件名称:{excelFileInfo.FileName}", "导入学生信息");

                try
                {

                    using (ExcelPackage package = new ExcelPackage(excelFileInfo.OpenReadStream()))
                    {
                        int repeatStudentCount = 0; //重复学生数量
                        int underGraduateCount = 0; //本科生学生数量
                        int postGraduateCount = 0;  //研究生数量
                        int uploadStudentCount = 0; //上传学生总数
                        int successUploadCount = 0; //成功导入学生总数
                        int newInstituteCount = 0;  //新增学院总数
                        int newProfessionCount = 0; //新增专业总数

                        ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                        int rowCount = workSheet.Dimension.Rows;

                        uploadStudentCount = rowCount -1 ;
                        StringBuilder errorBuilder = new StringBuilder();

                        List<Student> students = new List<Student>();

                        SystemSetting setting = _config.LoadSystemSetting();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            Student student = new Student();

                            student.StudentId = workSheet.Cells[row, 1].Value?.ToString()?.Trim();  //学号
                            student.Name = workSheet.Cells[row, 2].Value?.ToString()?.Trim(); //姓名
                            String sex = workSheet.Cells[row, 3].Value?.ToString()?.Trim();

                            if (sex == "男")
                            {
                                student.Sex = true;
                            }
                            else if (sex == "女")
                            {
                                student.Sex = false;
                            }
                            else
                            {
                                errorBuilder.Append($"学号为{student.StudentId}的学生[位置:第{row}行]性别不明！ 请填写 男 或者 女! <br/>");
                                continue; //不添加这个了  下一个直接
                            }

                            String type = workSheet.Cells[row,4].Value?.ToString()?.Trim();

                            if (type == "本科生")
                            {
                                student.StudentType = StudentType.UnderGraduate;
                                underGraduateCount++;
                            }
                            else if (type == "研究生")
                            {
                                student.StudentType = StudentType.PostGraduate;
                                postGraduateCount++;
                            }
                            else
                            {
                                errorBuilder.Append($"学号为{student.StudentId}的学生[位置:第{row}行]类别不明！ 请填写 本科生 或者 研究生!<br/>");
                                continue; //不添加这个了  下一个直接
                            }

                            String instituteName = workSheet.Cells[row, 5].Value?.ToString()?.Trim();

                            if (String.IsNullOrEmpty(instituteName))
                            {
                                errorBuilder.Append($"学号为{student.StudentId}的学生[位置:第{row}行]模块名称为空！  <br/>");
                                continue; //不添加这个了  下一个直接
                            }

                            Institute institute =  _context.Institute.FirstOrDefault(ins => ins.Name.Trim() == instituteName);

                            if (institute != null)
                            {
                                student.InstituteId = institute.InstituteId;
                            }
                            else
                            {
                                //如果允许添加新学院
                                if (isAllowAddInstitute)
                                {
                                    Institute newInstitute = new Institute {Name = instituteName };
                                    _context.Add(newInstitute);
                                    await _context.SaveChangesAsync().ContinueWith(result =>
                                    {
                                        newInstituteCount++;
                                        student.InstituteId =  newInstitute.InstituteId;
                                    });
                                }
                                else
                                {
                                    errorBuilder.Append($"学号为{student.StudentId}的学生[位置:第{row}行]学院信息在数据库中不存在！<br/>");
                                    continue; //不添加这个了  下一个直接
                                }
                            }
                            String professionName = workSheet.Cells[row, 6].Value?.ToString()?.Trim();
                            ProfessionType proType = student.StudentType == StudentType.PostGraduate
                                ? ProfessionType.PostGraduate
                                : ProfessionType.UnderGraduate;
                            Profession profession =  _context.Professions.FirstOrDefault(pro => pro.Name.Trim() == professionName && pro.ProfessionType == proType);

                            if (profession != null)
                            {
                                student.ProfessionId = profession.ProfessionId;
                            }
                            else
                            {
                                if (isAllowAddProfession)
                                {
                                    Profession newProfession = new Profession
                                    {
                                        Name = professionName,
                                        ProfessionType = student.StudentType == StudentType.UnderGraduate
                                            ? ProfessionType.UnderGraduate
                                            : ProfessionType.PostGraduate,
                                        InstituteId = student.InstituteId
                                    };
                                    _context.Professions.Add(newProfession);

                                    await _context.SaveChangesAsync().ContinueWith(res =>
                                    {
                                        newProfessionCount++;
                                        student.ProfessionId = newProfession.ProfessionId;
                                    });
                                }
                                else
                                {
                                    errorBuilder.Append($"学号为{student.StudentId}的学生[位置:第{row}行]专业信息在数据库中不存在！<br/>");
                                    continue; //不添加这个了  下一个直接
                                }
                            }

                            String birthDate = workSheet.Cells[row, 7].Value?.ToString()?.Trim();
                            if (String.IsNullOrEmpty(birthDate))
                            {
                                errorBuilder.Append($"学号为{student.StudentId}的学生[位置:第{row}行]出生日期为空！  <br/>");
                                continue; //不添加这个了  下一个直接
                            }
                            Boolean isSuccess = _analysis.TryConvertDateTime(birthDate, out var birthDateTime);
                            if (isSuccess)
                            {
                                student.BirthDate = birthDateTime;
                            }
                            else
                            {
                                errorBuilder.Append($"学号为{student.StudentId}的学生[位置:第{row}行]出生日期格式错误！ 格式例子: 2019/01/01 或 2019-5-23 <br/>");
                                continue; //不添加这个了  下一个直接
                            }

                            String grade = workSheet.Cells[row, 8].Value?.ToString()?.Trim();

                            Boolean isOkTryParse = Int32.TryParse(grade, out var interger);
                            if (isOkTryParse)
                            {
                                student.Grade = interger;
                            }
                            else
                            {
                                errorBuilder.Append($"学号为{student.StudentId}的学生[位置:第{row}行]年级错误！ 请输入四位数字 <br/>");
                                continue; //不添加这个了  下一个直接
                            }

                            String number = workSheet.Cells[row, 9].Value?.ToString()?.Trim();



                            if (number.Length == 18)
                            {
                                student.IDNumber = number;
                            }
                            else
                            {
                                errorBuilder.Append($"学号为{student.StudentId}的学生[位置:第{row}行]身份证号错误！ 身份证不是18位 <br/>");
                                continue; //不添加这个了  下一个直接
                            }

                            if (!_context.Student.Any(stu => stu.StudentId == student.StudentId))
                            {
                                if (students.FirstOrDefault(s => s.StudentId == student.StudentId) != null)
                                {
                                    errorBuilder.Append($"学号为{student.StudentId}的学生[位置:第{row}行] 自身重复,Excel 有至少两个这样学号相同的学生 <br/>");
                                    continue; //不添加这个了  下一个直接
                                }

                                successUploadCount++;

                                student.Password = _encryption.EncodeByMd5(_encryption.EncodeByMd5(student.IDNumber.Substring(student.IDNumber.Length - 6, 6)));
                                student.IsPassExam = false;
                                student.MaxExamScore = 0;
                                InstituteToModule im = _context.InstituteToModules.FirstOrDefault(m => m.InstituteId == student.InstituteId);
                                if (im == null)
                                {
                                    student.MaxExamCount = 2;
                                }
                                else
                                {
                                    Boolean isHaveSetting =
                                        setting.ExamModuleSettings.TryGetValue(im.ModuleId, out var mSetting);
                                    student.MaxExamCount = isHaveSetting ? mSetting.AllowExamTime : 2;
                                }
                                students.Add(student);
                            }
                            else
                            {
                                student = null;
                                repeatStudentCount++;
                            }
                        }

                        _context.Student.AddRange(students);
                        operation.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                        _context.LogPricipalOperations.Add(operation);
                        _context.SaveChanges();


                        return Json(new
                        {
                            isOk =  true,
                            uploadCount = uploadStudentCount,
                            repeatCount = repeatStudentCount,
                            postCount = postGraduateCount,
                            underCount = underGraduateCount,
                            Error = errorBuilder.ToString(),
                            success = successUploadCount,
                            newProfession = newProfessionCount,
                            newInstitute = newInstituteCount,
                            extension = true,
                            title = "消息提示",
                            message = "上传成功！"
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    title = "错误提示",
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    message = "参数错误,传递了不符合规定的参数,或者邮件格式错误"
                });
            }
        }

        public IActionResult Report()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Export( String sName, String sId, [Required] int iId, [Required] int pid, Boolean isUnder, Boolean isPost, [Required] int grade, [Required] int isPass, float leftScore = 0, float rightScore = Int32.MinValue)
        {
            if (ModelState.IsValid )
            {
                var userConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!userConfig.Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误",
                        message = "你并无学生管理操作权限! 无法导入"
                    });
                }

                String sql = "select * from StudentView where  InstituteId > 0";

                List<SqlParameter> parameters = new List<SqlParameter>();

                if (sName != null && sName.Trim() != "")
                {
                    sql += $" and StudentName like @Name";

                    parameters.Add(new SqlParameter { ParameterName = "@Name", Value = $"%{sName.Trim()}%", SqlDbType = SqlDbType.NVarChar });
                }

                if (sId != null && sId.Trim() != "")
                {
                    sql += $" and StudentId = @StudentId";

                    parameters.Add(new SqlParameter { ParameterName = "@StudentId", Value = sId.Trim(), SqlDbType = SqlDbType.NVarChar });
                }

                if (iId > 0)
                {
                    sql += $" and InstituteId = @InstituteId";

                    parameters.Add(new SqlParameter { ParameterName = "@InstituteId", Value = iId, SqlDbType = SqlDbType.Int });
                }

                if (pid > 0)
                {
                    sql += $" and ProfessionId = @pid";
                    parameters.Add(new SqlParameter { ParameterName = "@pid", Value = pid, SqlDbType = SqlDbType.Int });
                }

                if (isUnder == !isPost)
                {
                    if (isPost)
                    {
                        sql += $" and StudentType = @sType";
                        parameters.Add(new SqlParameter { ParameterName = "@sType", Value = 1, SqlDbType = SqlDbType.Int });
                    }

                    if (isUnder)
                    {
                        sql += $" and StudentType = @uType";
                        parameters.Add(new SqlParameter { ParameterName = "@uType", Value = 0, SqlDbType = SqlDbType.Int });
                    }
                }

                if (grade > 2015)
                {
                    sql += $" and Grade = @grade";
                    parameters.Add(new SqlParameter { ParameterName = "@grade", Value = grade, SqlDbType = SqlDbType.Int });
                }

                if (isPass >= 0)
                {
                    sql += $" and IsPassExam = @PassState";
                    Boolean val = (isPass != 0);
                    parameters.Add(new SqlParameter { ParameterName = "@PassState", Value = val, SqlDbType = SqlDbType.Bit });
                }

                sql += $" and MaxExamScore >= @left and  MaxExamScore <= @right";

                parameters.Add(new SqlParameter { ParameterName = "@left", Value = leftScore, SqlDbType = SqlDbType.Real });
                parameters.Add(new SqlParameter { ParameterName = "@right", Value = rightScore, SqlDbType = SqlDbType.Real });

                LogPricipalOperation log = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.ExportExamData,
                    $"导出学生信息", "导出学习信息到Excel中!");
                
                // ReSharper disable once CoVariantArrayConversion
                var items = _context.VStudentMaps.FromSql(sql, parameters.ToArray())
                    .OrderBy(item => item.InstituteId)
                    .ThenBy(item => item.ProfessionId)
                    .ThenByDescending(item => item.MaxExamScore)
                    .Select(val => new
                    {
                        instituteName = val.InstituteName,
                        professionName = $"{val.ProfessionName}" + (val.ProfessionType == ProfessionType.UnderGraduate ? "[本科生]" : "[研究生]"),
                        professionType = val.ProfessionType,
                        studentId = val.StudentId,
                        studentName = val.StudentName,
                        grade = val.Grade,
                        sex = val.Sex == true ? "男" : "女",
                        studentType = val.StudentType == StudentType.UnderGraduate ? "本科生" : "研究生",
                        isPassExam = val.IsPassExam ? "通过" : "未通过",
                        maxExamScore = val.MaxExamScore,
                    }).ToList();

                
                string sFileName = $"四川师范大学考试统计.xlsx";
                FileInfo file = new FileInfo($"{ _hosting.WebRootPath}/excel/{sFileName}");

                using (var stream = new FileStream(file.ToString(), FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                {
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        //添加worksheet
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("考试结果统计");
                        //添加头
                        worksheet.Cells[1, 1].Value = "编号";
                        worksheet.Cells[1, 2].Value = "学号";
                        worksheet.Cells[1, 3].Value = "性别";
                        worksheet.Cells[1, 4].Value = "专业";
                        worksheet.Cells[1, 5].Value = "学院";
                        worksheet.Cells[1, 6].Value = "年级";
                        worksheet.Cells[1, 7].Value = "学生类型";
                        worksheet.Cells[1, 8].Value = "考试分数[最好成绩]";
                        worksheet.Cells[1, 9].Value = "是否通过";

                        for (int i = 1; i <= 9; i++)
                        {
                            worksheet.Cells[1, i].Style.Font.Bold = true;
                        }
                        int row = 2;
                        int indexNumber = 1;
                        //添加数据
                        foreach (var item in items)
                        {
                            worksheet.Cells[row, 1].Value = indexNumber;
                            worksheet.Cells[row, 2].Value = item.studentId;
                            worksheet.Cells[row, 3].Value = item.sex;
                            worksheet.Cells[row, 4].Value = item.professionName;
                            worksheet.Cells[row, 5].Value = item.instituteName;
                            worksheet.Cells[row, 6].Value = item.grade;
                            worksheet.Cells[row, 7].Value = item.studentType;
                            worksheet.Cells[row, 8].Value = item.maxExamScore;
                            worksheet.Cells[row, 9].Value = item.isPassExam;
                            row++;
                            indexNumber++;
                        }
                        package.Save();
                    }
                }

                log.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                _context.LogPricipalOperations.Add(log);
                _context.SaveChanges();
                return Json(new
                {
                    isOk = true,
                    url = $"/excel/{sFileName}",
                    title = "提示",
                    message ="导出成功!"

                });
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    message = $"参数错误,传递了不符合规定的参数"
                });
            }
        }

        [HttpGet]
        public IActionResult Ieq()
        {
            return View();
        }

        /*导入题目*/
        [ValidateAntiForgeryToken,HttpPost]
        public async Task<IActionResult> InsertExaminationQuestions([Required] IFormFile excelFileInfo)
        {
            if (ModelState.IsValid)
            {
                var userConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!userConfig.Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误",
                        message = "你并无学生管理操作权限! 无法导入"
                    });
                }

                if (Path.GetExtension(excelFileInfo.FileName).Trim() != ".xlsx")
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误提示",
                        extension = false,
                        message = "请上传 Excel2007/2010 或以上版本的Excel 文件！后缀为 xlsx"
                    });
                }

                try
                {

                    using (ExcelPackage package = new ExcelPackage(excelFileInfo.OpenReadStream()))
                    {
                        int allCount = 0;
                        int singleCount = 0; //上传的选择题
                        int multipleCount = 0; //上传的多选题
                        int judgeCount = 0;  //上传的判断题
                        int successSingleCount = 0; //上传学生总数
                        int successMultipleCount = 0; //成功导入学生总数
                        int successJudgeCount = 0;  //新增学院总数
                        int repeatCount = 0; //新增专业总数

                        ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                        int rowCount = workSheet.Dimension.Rows;

                        allCount = rowCount - 1;
                        StringBuilder errorBuilder = new StringBuilder();

                        List<JudgeChoices> judges = new List<JudgeChoices>();
                        List<SingleChoices> singles = new List<SingleChoices>();
                        List<MultipleChoices> multiples = new List<MultipleChoices>();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            String testType = workSheet.Cells[row, 2].Value?.ToString()?.Trim();  //题目类型
                            String moduleName = workSheet.Cells[row, 1].Value?.ToString()?.Trim();  //题目类型
                            String content = workSheet.Cells[row, 3].Value?.ToString()?.Trim();  //题目类型
                            String answer = workSheet.Cells[row, 4].Value?.ToString()?.Trim();  //题目类型
                            String difficult = workSheet.Cells[row, 5].Value?.ToString()?.Trim();  //题目类型
                            String count = workSheet.Cells[row, 6].Value?.ToString()?.Trim();  //题目类型
                            String A = workSheet.Cells[row, 7].Value?.ToString()?.Trim();  //题目类型
                            String B = workSheet.Cells[row, 8].Value?.ToString()?.Trim();  //题目类型
                            String C = workSheet.Cells[row, 9].Value?.ToString()?.Trim();  //题目类型
                            String D = workSheet.Cells[row, 10].Value?.ToString()?.Trim();  //题目类型
                            String E = workSheet.Cells[row, 11].Value?.ToString()?.Trim();  //题目类型
                            String F = workSheet.Cells[row, 12].Value?.ToString()?.Trim();  //题目类型

                            if (String.IsNullOrEmpty(answer))
                            {
                                errorBuilder.Append($"[位置:第{row}行]答案 为空 <br/>");
                                continue;
                            }

                            if (String.IsNullOrEmpty(content))
                            {
                                errorBuilder.Append($"[位置:第{row}行]题目内容 为空 <br/>");
                                continue;
                            }

                            if (String.IsNullOrEmpty(moduleName))
                            {
                                errorBuilder.Append($"[位置:第{row}行]模块名称 为空 <br/>");
                                continue;
                            }

                            if (String.IsNullOrEmpty(A))
                            {
                                errorBuilder.Append($"[位置:第{row}行]选项A 为空 <br/>");
                                continue;
                            }

                            if (String.IsNullOrEmpty(B))
                            {
                                errorBuilder.Append($"[位置:第{row}行]选项B 为空 <br/>");
                                continue;
                            }

                            Module module = _context.Modules.FirstOrDefault(m => m.Name.Trim() == moduleName);
                            if (module == null)
                            {
                                errorBuilder.Append($"[位置:第{row}行]所属模块不明！请输入系统已有模块！或注意模块名称符号问题 <br/>");
                                continue;
                            }

                            Boolean isCanParse = Int32.TryParse(count, out var interger);
                            if (!isCanParse || interger < 2)
                            {
                                errorBuilder.Append($"[位置:第{row}行]选项数目无法转换为数字！ 或者选项数目小于 2 <br/>");
                                continue;
                            }

                            isCanParse = float.TryParse(difficult, out var difficultFloat);
                            if (!isCanParse || difficultFloat <= 0)
                            {
                                errorBuilder.Append($"[位置:第{row}行]题目难度无法转换为数字！ 或者题目难度小于0 <br/>");
                                continue;
                            }

                            Char[] checkAnswer = answer.ToUpper().Trim().ToCharArray();

                            String dealResultAnswer = String.Join("", checkAnswer);

                            String key = _encryption.EncodeByMd5(content);
                            switch (testType)
                            {
                                case "单选题":
                                {
                                    singleCount++;
                                    if (dealResultAnswer.Length != 1)
                                    {
                                        errorBuilder.Append($"[位置:第{row}行]单选题的答案不是一个选项！ <br/>");
                                        break;
                                    }
                                        SingleChoices single = new SingleChoices
                                    {
                                        ModuleId = module.ModuleId,
                                        Answer = dealResultAnswer,
                                        AddTime = DateTime.Now
                                    };

                                    if (_context.SingleChoices.Any(s => s.Key == key && s.ModuleId == module.ModuleId))
                                    {
                                        repeatCount++;
                                        errorBuilder.Append($"[位置:第{row}行]单选题已经存在数据库中！无需再上传 <br/>");
                                        break;
                                    }

                                    single.Content = content;
                                    single.Key = key;
                                    single.Count = interger;
                                    single.DegreeOfDifficulty = difficultFloat;
                                    single.A = A;
                                    single.B = B;
                                    if (!string.IsNullOrEmpty(C) && C != "null" && C != "NULL")
                                    {
                                        single.C = C;
                                    }

                                    if (!string.IsNullOrEmpty(D) && D != "null" && D != "NULL")
                                    {
                                        single.D = D;
                                    }
                                    
                                    if (!string.IsNullOrEmpty(E) && E != "null" && E != "NULL")
                                    {
                                        single.E = E;
                                    }

                                    if (!string.IsNullOrEmpty(F) && F != "null" && F != "NULL")
                                    {
                                        single.F = F;
                                    }

                                    if (singles.FirstOrDefault(sg => sg.Key == single.Key && sg.ModuleId == single.ModuleId) != null)
                                    {
                                        errorBuilder.Append($"[位置:第{row}行]单选题在上传Excel 重复！无需再上传 <br/>");
                                        break;
                                    }

                                    single.PrincipalId = userConfig.PrincipalId;
                                    singles.Add(single);
                                    successSingleCount++;
                                    break;
                                 }  
                                case "多选题":
                                {
                                    multipleCount++;
                                    MultipleChoices multiple = new MultipleChoices
                                    {
                                        ModuleId = module.ModuleId,
                                        Answer = dealResultAnswer,
                                        AddTime = DateTime.Now
                                    };

                                    if (_context.MultipleChoices.Any(s => s.Key == key && s.ModuleId == module.ModuleId))
                                    {
                                        repeatCount++;
                                        errorBuilder.Append($"[位置:第{row}行]多选题已经存在在数据库中！无需再上传 <br/>");
                                        break;
                                    }

                                    multiple.Content = content;
                                    multiple.Key = key;
                                    multiple.Count = interger;
                                    multiple.DegreeOfDifficulty = difficultFloat;
                                    multiple.A = A;
                                    multiple.B = B;
                                    if (!string.IsNullOrEmpty(C) && C != "null" && C != "NULL")
                                    {
                                        multiple.C = C;
                                    }

                                    if (!string.IsNullOrEmpty(D) && D != "null" && D != "NULL")
                                    {
                                        multiple.D = D;
                                    }

                                    if (!string.IsNullOrEmpty(E) && E != "null" && E != "NULL")
                                    {
                                        multiple.E = E;
                                    }

                                    if (!string.IsNullOrEmpty(F) && F != "null" && F != "NULL")
                                    {
                                        multiple.F = F;
                                    }

                                    if (multiples.FirstOrDefault(sg => sg.Key == multiple.Key && sg.ModuleId == multiple.ModuleId) != null)
                                    {
                                        errorBuilder.Append($"[位置:第{row}行]多选题在上传Excel 重复！无需再上传 <br/>");
                                        break;
                                    }

                                        multiple.PrincipalId = userConfig.PrincipalId;
                                    multiples.Add(multiple);
                                    successMultipleCount++;
                                    break;
                                }
                                case "判断题":
                                {
                                    judgeCount++;
                                    if (dealResultAnswer.Length != 1)
                                    {
                                        errorBuilder.Append($"[位置:第{row}行]判断题的答案不是一个选项！ <br/>");
                                        break;
                                    }    
                                    JudgeChoices judge = new JudgeChoices
                                    {
                                        ModuleId = module.ModuleId,
                                        Answer = dealResultAnswer,
                                        AddTime = DateTime.Now
                                    };

                                    if (_context.JudgeChoices.Any(s => s.Key == key && s.ModuleId == module.ModuleId))
                                    {
                                        repeatCount++;
                                        errorBuilder.Append($"[位置:第{row}行]判断题已经存在在数据库中！无需再上传 <br/>");
                                        break;
                                    }

                                    judge.Content = content;
                                    judge.Key = key;
                                    judge.Count = interger;
                                    judge.DegreeOfDifficulty = difficultFloat;
                                    judge.A = A;
                                    judge.B = B;

                                    if (judges.FirstOrDefault(sg => sg.Key == judge.Key && sg.ModuleId == judge.ModuleId) != null)
                                    {
                                        errorBuilder.Append($"[位置:第{row}行]判断题在上传Excel 重复！无需再上传 <br/>");
                                        break;
                                    }
                                    judge.PrincipalId = userConfig.PrincipalId;
                                    successJudgeCount++;
                                    judges.Add(judge);
                                    break;
                                }
                                default:
                                {
                                    errorBuilder.Append($"[位置:第{row}行]题目类型不符合规定！ 请输入 判断题/单选题/多选题 <br/>");
                                    break;
                                }
                            }
                        }

                        var operation = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.UploadInsertStudent, $"上传Excel文件名称:{excelFileInfo.FileName}", "导入学生信息");
                        operation.PrincpalOperationStatus = PrincpalOperationStatus.Success;
                        _context.JudgeChoices.AddRange(judges);
                        _context.MultipleChoices.AddRange(multiples);
                        _context.SingleChoices.AddRange(singles);
                        _context.LogPricipalOperations.Add(operation);
                        await _context.SaveChangesAsync();

                        return Json(new
                        {
                            isOk = true,
                            title = "提示",
                            extension = true,
                            error = errorBuilder.ToString(),
                            message = "导入成功",
                            allCount = allCount,
                            singleCount = singleCount,
                            multipleCount = multipleCount,
                            judgeCount = judgeCount,
                            successSingleCount= successSingleCount,
                            successMultipleCount= successMultipleCount,
                            successJudgeCount= successJudgeCount,
                            repeatCount = repeatCount,
                            success = successSingleCount + successMultipleCount + successJudgeCount
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(e.StackTrace);
                    throw;
                }
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    message = $"参数错误,传递了不符合规定的参数"
                });
            }
        }

        [HttpPost]
        public IActionResult Statistics([Required] int instituteId, [Required] int grade, [Required] int orderOne)
        {
            if (ModelState.IsValid)
            {
                var userConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!userConfig.Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误",
                        message = "你并无学生管理操作权限! 无法导出"
                    });
                }
                StringBuilder builder = new StringBuilder("select * from  [ExamResultView] where [InstituteId] > 0");

                if (instituteId > 0)
                {
                    builder.Append($" and [InstituteId] = {instituteId}");
                }

                if (grade > -1)
                {
                    builder.Append($" and [Grade] = {grade}");
                }

                List<vExamResultMap> list = null;

                if (orderOne == 0)
                {
                    list = _context.VExamResultMaps
                        .FromSql(builder.ToString())
                        .OrderByDescending(v => v.TotalPassRate).ToList();
                }
                else if (orderOne > 0)
                {
                    list = _context.VExamResultMaps
                        .FromSql(builder.ToString())
                        .OrderByDescending(v => v.PostPassRate).ToList();
                }
                else
                {
                    list = _context.VExamResultMaps
                        .FromSql(builder.ToString())
                        .OrderByDescending(v => v.UnderPassRate).ToList();
                }

                String sFileName = $"四川师范大学考试学院考试情况统计.xlsx";
                FileInfo file = new FileInfo($"{ _hosting.WebRootPath}/excel/{sFileName}");

                using (var stream = new FileStream(file.ToString(), FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                {
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        //添加worksheet
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("考试结果统计");
                        //添加头
                        worksheet.Cells[1, 1].Value = "编号";
                        worksheet.Cells[1, 2].Value = "年级";
                        worksheet.Cells[1, 3].Value = "学院";
                        worksheet.Cells[1, 4].Value = "总人数";
                        worksheet.Cells[1, 5].Value = "总通过人数";
                        worksheet.Cells[1, 6].Value = "研究生";
                        worksheet.Cells[1, 7].Value = "本科生";
                        worksheet.Cells[1, 8].Value = "研究生通过";
                        worksheet.Cells[1, 9].Value = "研究生未通过";
                        worksheet.Cells[1, 10].Value = "本科生通过";
                        worksheet.Cells[1, 11].Value = "本科生未通过";
                        worksheet.Cells[1, 12].Value = "总通过率";
                        worksheet.Cells[1, 13].Value = "研究生通过率";
                        worksheet.Cells[1, 14].Value = "本科生通过率";
                        
                        for (int i = 1; i <= 14; i++)
                        {
                            worksheet.Cells[1, i].Style.Font.Bold = true;
                        }
                        int row = 2;
                        int indexNumber = 1;
                        //添加数据
                        foreach (var item in list)
                        {
                            worksheet.Cells[row, 1].Value = indexNumber;
                            worksheet.Cells[row, 2].Value = item.Grade;
                            worksheet.Cells[row, 3].Value = item.Name;
                            worksheet.Cells[row, 4].Value = item.Total;
                            worksheet.Cells[row, 5].Value = item.PassTotal;
                            worksheet.Cells[row, 6].Value = item.PostCount;
                            worksheet.Cells[row, 7].Value = item.UnderCount;
                            worksheet.Cells[row, 8].Value = item.PostPassCount;
                            worksheet.Cells[row, 9].Value = item.PostNotPassCount;
                            worksheet.Cells[row, 10].Value = item.UnderPassCount;
                            worksheet.Cells[row, 11].Value = item.UnderNotPassCount ;
                            worksheet.Cells[row, 12].Value = item.TotalPassRate + "%";
                            worksheet.Cells[row, 13].Value = item.PostPassRate + "%";
                            worksheet.Cells[row, 14].Value = item.UnderPassRate + "%";
                            
                            row++;
                            indexNumber++;
                        }
                        package.Save();
                    }
                }
                LogPricipalOperation log = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.ExportExamData,
                    $"导出考试统计信息", "导出各学院考试统计信息到Excel中!");
                _context.LogPricipalOperations.Add(log);
                _context.SaveChanges();
                return Json(new
                {
                    isOk = true,
                    url = $"/excel/{sFileName}",
                    title = "提示",
                    message = "导出成功!"

                });
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

        [HttpPost]
        public IActionResult Grade( [Required] int grade, [Required] int orderOne)
        {
            if (ModelState.IsValid)
            {
                var userConfig = _analysis.GetLoginUserConfig(HttpContext);
                if (!userConfig.Power.StudentManager)
                {
                    return Json(new
                    {
                        isOk = false,
                        title = "错误",
                        message = "你并无学生管理操作权限! 无法导出"
                    });
                }
                StringBuilder builder = new StringBuilder("select * from  [ExamGradeResultView] where [Grade] > 0");

                if (grade > -1)
                {
                    builder.Append($" and [Grade] = {grade}");
                }

                List<vExamGradeResultMap> list = null;

                if (orderOne == 0)
                {
                    list = _context.VExamGradeResultMaps
                        .FromSql(builder.ToString())
                        .OrderByDescending(v => v.PassTotleRate).ToList();
                }
                else if (orderOne > 0)
                {
                    list = _context.VExamGradeResultMaps
                        .FromSql(builder.ToString())
                        .OrderByDescending(v => v.PostPassRate).ToList();
                }
                else
                {
                    list = _context.VExamGradeResultMaps
                        .FromSql(builder.ToString())
                        .OrderByDescending(v => v.UnderPassRate).ToList();
                }

                String sFileName = $"四川师范大学考试年度考试情况统计.xlsx";
                FileInfo file = new FileInfo($"{ _hosting.WebRootPath}/excel/{sFileName}");

                using (var stream = new FileStream(file.ToString(), FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                {
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        //添加worksheet
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("考试结果统计");
                        //添加头
                        worksheet.Cells[1, 1].Value = "编号";
                        worksheet.Cells[1, 2].Value = "年级";
                        worksheet.Cells[1, 3].Value = "总人数";
                        worksheet.Cells[1, 4].Value = "总通过人数";
                        worksheet.Cells[1, 5].Value = "研究生";
                        worksheet.Cells[1, 6].Value = "本科生";
                        worksheet.Cells[1, 7].Value = "研究生通过";
                        worksheet.Cells[1, 8].Value = "本科生通过";
                        worksheet.Cells[1, 9].Value = "总通过率";
                        worksheet.Cells[1, 10].Value = "研究生通过率";
                        worksheet.Cells[1, 11].Value = "本科生通过率";

                        for (int i = 1; i <= 14; i++)
                        {
                            worksheet.Cells[1, i].Style.Font.Bold = true;
                        }
                        int row = 2;
                        int indexNumber = 1;
                        //添加数据
                        foreach (var item in list)
                        {
                            worksheet.Cells[row, 1].Value = indexNumber;
                            worksheet.Cells[row, 2].Value = item.Grade;
                            worksheet.Cells[row, 3].Value = item.Total;
                            worksheet.Cells[row, 4].Value = item.PassTotal;
                            worksheet.Cells[row, 5].Value = item.PostCount;
                            worksheet.Cells[row, 6].Value = item.UnderCount;
                            worksheet.Cells[row, 7].Value = item.PostPassCount;
                            worksheet.Cells[row, 8].Value = item.UnderPassCount;
                            worksheet.Cells[row, 9].Value = item.PassTotleRate + "%";
                            worksheet.Cells[row, 10].Value = item.PostPassRate + "%";
                            worksheet.Cells[row, 11].Value = item.UnderPassRate + "%";

                            row++;
                            indexNumber++;
                        }
                        package.Save();
                    }
                }
                LogPricipalOperation log = _logger.GetDefaultLogPricipalOperation(PrincpalOperationCode.ExportExamData,
                    $"导出考试统计信息", "导出各学院考试统计信息到Excel中!");
                _context.LogPricipalOperations.Add(log);
                _context.SaveChanges();
                return Json(new
                {
                    isOk = true,
                    url = $"/excel/{sFileName}",
                    title = "提示",
                    message = "导出成功!"

                });
            }
            else
            {
                return Json(new
                {
                    isOk = false,
                    error = _analysis.ModelStateDictionaryError(ModelState),
                    title = "错误提示",
                    message = "参数错误,传递了不符合规定的参数"
                });
            }
        }

    }
}