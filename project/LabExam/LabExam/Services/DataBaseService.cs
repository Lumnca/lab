using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Entities;
using LabExam.Models.Map;
using Microsoft.EntityFrameworkCore;

namespace LabExam.Services
{
    public class DataBaseService: IDataBaseService
    {

        public float GetPaperScore(int paperId, LabContext context)
        {
            ExaminationPaper paper = context.ExaminationPapers.Find(paperId);
            if (paper == null)
            {
                return -100.0f;
            }
            else
            {
                float judgeScore = context.ExamJudgeChoices.Where(p => p.PaperId == paper.PaperId)
                    .Where(p => p.RealAnswer == p.StudentAnswer).Sum(p => p.Score);
                float singleScore = context.ExamSingleChoices.Where(p => p.PaperId == paper.PaperId)
                    .Where(p => p.RealAnswer == p.StudentAnswer).Sum(p => p.Score);
                float multipleScore = context.ExamMultipleChoices.Where(p => p.PaperId == paper.PaperId)
                    .Where(p => p.RealAnswer == p.StudentAnswer).Sum(p => p.Score);

                return judgeScore + singleScore + multipleScore;
            }
        }

        public void FinishPaper(ExaminationPaper paper, LabContext context)
        {
            if (paper != null)
            {
                if (!paper.IsFinish)
                {
                    if (paper.LeaveExamTime <= 1.0)
                    {
                        paper.LeaveExamTime = 0;
                    }

                    paper.IsFinish = true;

                    if (String.IsNullOrEmpty(paper.Review))
                    {
                        paper.Review = "学生未作出评价！";
                    }
                    paper.Score = GetPaperScore(paper.PaperId, context);

                    Student student = context.Student.Find(paper.StudentId);


                    if (student != null)
                    {
                        if (paper.Score >= paper.PassScore)
                        {
                            student.IsPassExam = true;
                        }

                        double middle = Math.Round((paper.Score / paper.TotleScore) * 100, 1);

                        if (middle > student.MaxExamCount)
                        {
                            student.MaxExamScore = (float)middle;
                        }
                    }
                    else
                    {
                        context.LogPricipalOperations.Add(new LogPricipalOperation
                        {
                            PrincpalOperationStatus = PrincpalOperationStatus.Success,
                            AddTime = DateTime.Now,
                            PrincpalOperationCode = PrincpalOperationCode.SystemRuntimeError,
                            OperationIp = "系统运行产生！",
                            PrincpalOperationName = $"试卷ID:{paper.PaperId} ,试卷所属学生学号:{paper.StudentId}",
                            PrincpalOperationContent = "完成试卷的时候发现学生已经被删除！ 考试期间管理员删除了学生导致考试异常! 请查看."
                        });
                    }


                    context.SaveChanges();
                }
            }

        }

        public bool DeletePaper(ExaminationPaper ePaper, LabContext context)
        {
            if (ePaper != null && context.ExaminationPapers.Any(p => p.PaperId == ePaper.PaperId))
            {
                try
                {
                    context.Database.BeginTransaction();
                    context.Database.ExecuteSqlCommand("Delete from ExamJudgeChoices Where PaperId = @pid", new SqlParameter
                    {
                        Value = ePaper.PaperId,
                        ParameterName = "@pid",
                        SqlDbType = SqlDbType.Int
                    });

                    context.Database.ExecuteSqlCommand("Delete from ExamMultipleChoices Where PaperId = @pid", new SqlParameter
                    {
                        Value = ePaper.PaperId,
                        ParameterName = "@pid",
                        SqlDbType = SqlDbType.Int
                    });

                    context.Database.ExecuteSqlCommand("Delete from ExamSingleChoices Where PaperId = @pid", new SqlParameter
                    {
                        Value = ePaper.PaperId,
                        ParameterName = "@pid",
                        SqlDbType = SqlDbType.Int
                    });

                    context.ExaminationPapers.Remove(ePaper);
                    int result = context.SaveChanges();
                    context.Database.CommitTransaction();
                    return result == 1;
                }
                catch (Exception e)
                {
                    context.Database.RollbackTransaction();
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public int NotLearningCount(LabContext context)
        {
            String sql ="Select count(*) from student where Student.StudentId not in (select StudentId from Learings)";
            if (context.Database.GetDbConnection().State == ConnectionState.Open)
            {

            }
            else
            {
                context.Database.GetDbConnection().Open();
            }
            DbCommand cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            int result =Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }

        public int NotJoinSystemStudentCount(LabContext context)
        {
            String sql = "select count(*) from Student where StudentId not in(select StudentId from LogStudentOperations  )";
            if (context.Database.GetDbConnection().State == ConnectionState.Open)
            {

            }
            else
            {
                context.Database.GetDbConnection().Open();
            }
            DbCommand cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }
    }
}
