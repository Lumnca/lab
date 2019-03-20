using LabExam.Models.Entities;
using LabExam.Models.EntitiyViews;
using Microsoft.EntityFrameworkCore;

namespace LabExam.DataSource
{
    public class LabContext:DbContext
    {
        public LabContext(DbContextOptions<LabContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* 建立索引*/

            modelBuilder.Entity<InstituteToModule>().HasIndex(b => b.InstituteId).IsUnique(false);

            modelBuilder.Entity<Student>().HasIndex(s => s.MaxExamScore).IsUnique(false);

            modelBuilder.Entity<Learing>().HasIndex(s => s.CourceId).IsUnique(false);


            modelBuilder.Entity<Learing>().HasIndex(s => s.StudentId).IsUnique(false);

            modelBuilder.Entity<Progress>().HasIndex(s => s.StudentId).IsUnique(false);

            modelBuilder.Entity<Progress>().HasIndex(s => s.ResourceId).IsUnique(false);

            modelBuilder.Entity<JudgeChoices>().HasIndex(s => s.ModuleId).IsUnique(false);
            modelBuilder.Entity<MultipleChoices>().HasIndex(s => s.ModuleId).IsUnique(false);
            modelBuilder.Entity<SingleChoices>().HasIndex(s => s.ModuleId).IsUnique(false);

            modelBuilder.Entity<ExamJudgeChoices>().HasIndex(s => s.PaperId).IsUnique(false);
            modelBuilder.Entity<ExamMultipleChoices>().HasIndex(s => s.PaperId).IsUnique(false);
            modelBuilder.Entity<ExamSingleChoices>().HasIndex(s => s.PaperId).IsUnique(false);

            modelBuilder.Entity<LogStudentOperation>().HasIndex(s => s.StudentId).IsUnique(false);
            modelBuilder.Entity<ExaminationPaper>().HasIndex(s => s.StudentId).IsUnique(false);
            modelBuilder.Entity<ExaminationPaper>().HasIndex(s => s.IsFinish).IsUnique(false);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().Property(b => b.BirthDate).HasDefaultValueSql("getdate()");


            modelBuilder.Query<vInstituteToModuleMap>().ToView("InstituteToModuleView", "dbo");

            modelBuilder.Query<vInstituteWithoutModuleMap>().ToView("InstituteWithoutModuleView", "dbo");

            modelBuilder.Query<vInstituteStudentCountMap>().ToView("InstituteStudentCountView", "dbo");

            modelBuilder.Query<vInstituteMap>().ToView("InstituteView", "dbo");

            modelBuilder.Query<vInstituteStudentNotPassCountMap>().ToView("InstituteStudentNotPassCountView", "dbo");

            modelBuilder.Query<vProfessionMap>().ToView("ProfessionView", "dbo");

            modelBuilder.Query<vStudentMap>().ToView("StudentView", "dbo");

            modelBuilder.Query<vReExamApplicationMap>().ToView("ReExamApplicationView", "dbo");

            modelBuilder.Query<vLearningMap>().ToView("LearningView", "dbo");

            modelBuilder.Query<vLogStudentMap>().ToView("LogStudentView", "dbo");

            modelBuilder.Query<vCourceMap>().ToView("CourceView", "dbo");

            modelBuilder.Query<vProgressMap>().ToView("ProgressView", "dbo");

            modelBuilder.Query<vExamResultMap>().ToView("ExamResultView", "dbo");

            modelBuilder.Query<vExamGradeResultMap>().ToView("ExamGradeResultView", "dbo");

            modelBuilder.Query<vRandomJudgeMap>().ToView("RandomJudgeView", "dbo");

            modelBuilder.Query<vRandomMultipleMap>().ToView("RandomMultipleView", "dbo");

            modelBuilder.Query<vRandomSingleMap>().ToView("RandomSingleView", "dbo");

            modelBuilder.Query<vStatisticJudgeMap>().ToView("StatisticJudgeView", "dbo");

            modelBuilder.Query<vStatisticMultipleMap>().ToView("StatisticMultipleView", "dbo");

            modelBuilder.Query<vStatisticSingleMap>().ToView("StatisticSingleView", "dbo");

            modelBuilder.Query<vUserLoginStatisticMap>().ToView("UserLoginStatisticView", "dbo");

            modelBuilder.Query<vStudentDistributionMap>().ToView("StudentDistributionView", "dbo");

            modelBuilder.Query<vUserLoginTerminalMap>().ToView("UserLoginTerminalView", "dbo");

            modelBuilder.Query<vInstituteStatisticMap>().ToView("InstituteStatisticView", "dbo");

            modelBuilder.Query<vExamAllStatisticMap>().ToView("ExamAllStatisticView", "dbo");

            modelBuilder.Query<vExamGradeStatisticMap>().ToView("ExamGradeStatisticView", "dbo");
            
        }
        /* 视图 View */
        public virtual DbQuery<vInstituteToModuleMap> VInstituteToModuleMaps { get; set; }

        public virtual DbQuery<vInstituteWithoutModuleMap> VInstituteWithoutModuleMaps { get; set; }

        public virtual DbQuery<vInstituteStudentCountMap> VInstituteStudentCountMaps { get; set; }

        public virtual DbQuery<vInstituteMap> VInstituteMaps { get; set; }

        public virtual DbQuery<vInstituteStudentNotPassCountMap> VInstituteStudentNotPassCountMaps { get; set; }

        public virtual DbQuery<vProfessionMap> VProfessionMaps { get; set; }

        public virtual DbQuery<vStudentMap> VStudentMaps { get; set; }

        public virtual DbQuery<vReExamApplicationMap> VReExamApplicationMaps { get; set; }

        public virtual DbQuery<vLearningMap> VLearningMaps { get; set; }

        public virtual DbQuery<vLogStudentMap> VLogStudentMaps { get; set; }

        public virtual DbQuery<vCourceMap> VCourceMaps { get; set; }

        public virtual DbQuery<vProgressMap> VProgressMaps { get; set; }

        public virtual DbQuery<vExamResultMap> VExamResultMaps { get; set; }

        public virtual DbQuery<vExamGradeResultMap> VExamGradeResultMaps { get; set; }

        public virtual DbQuery<vRandomJudgeMap> VRandomJudgeMaps { get; set; }

        public virtual DbQuery<vRandomMultipleMap> VRandomMultipleMaps { get; set; }

        public virtual DbQuery<vRandomSingleMap> VRandomSingleMaps { get; set; }

        public virtual DbQuery<vStatisticMultipleMap> VStatisticMultipleMaps { get; set; }

        public virtual DbQuery<vStatisticJudgeMap> VStatisticJudgeMaps { get; set; }

        public virtual DbQuery<vStatisticSingleMap> VStatisticSingleMaps { get; set; }

        public virtual DbQuery<vUserLoginStatisticMap> VUserLoginStatisticMaps { get; set; }

        public virtual DbQuery<vStudentDistributionMap> VStudentDistributionMaps { get; set; }

        public virtual DbQuery<vUserLoginTerminalMap> VUserLoginTerminalMaps { get; set; }

        public virtual DbQuery<vInstituteStatisticMap> VInstituteStatisticMaps { get; set; }

        public virtual DbQuery<vExamAllStatisticMap> VExamAllStatisticMaps { get; set; }

        public virtual DbQuery<vExamGradeStatisticMap> VExamGradeStatisticMaps { get; set; }
        
        /* 表 Table */
        public virtual DbSet<Module> Modules { get; set; }

        public virtual DbSet<Principal> Principals { get; set; }

        public virtual DbSet<Institute> Institute { get; set; }

        public virtual DbSet<InstituteToModule> InstituteToModules { get; set; }

        public virtual DbSet<Profession> Professions { get; set; }

        public virtual DbSet<Student> Student { get; set; }

        public virtual DbSet<Cource> Cources { get; set; }

        public virtual DbSet<Resource> Resources { get; set; }

        public virtual DbSet<Learing> Learings { get; set; }

        public virtual DbSet<Progress> Progresses { get; set; }

        public virtual DbSet<SingleChoices> SingleChoices { get; set; }

        public virtual DbSet<MultipleChoices> MultipleChoices { get; set; }

        public virtual DbSet<JudgeChoices> JudgeChoices { get; set; }

        public virtual DbSet<ExaminationPaper> ExaminationPapers { get; set;  }

        public virtual DbSet<ExamSingleChoices> ExamSingleChoices { get; set; }

        public virtual DbSet<ExamJudgeChoices> ExamJudgeChoices { get; set; }

        public virtual DbSet<ExamMultipleChoices> ExamMultipleChoices { get; set; }
        

        public virtual DbSet<ApplicationJoinTheExamination> ApplicationJoinTheExaminations { get; set; }

        public virtual DbSet<ApplicationForReExamination> ApplicationForReExaminations { get; set; }

        public virtual DbSet<LogStudentOperation> LogStudentOperations { get; set; }

        public virtual  DbSet<LogPricipalOperation> LogPricipalOperations { get; set; }

        public virtual DbSet<LogUserLogin> LogUserLogin { get; set; }

    }
}
