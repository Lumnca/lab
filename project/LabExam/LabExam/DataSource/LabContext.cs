using LabExam.Models.Entities;
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .Property(b => b.BirthDate)
                .HasDefaultValueSql("getdate()");
        }

        public  DbSet<Module> Modules { get; set; }

        public DbSet<Principal> Principals { get; set; }

        public DbSet<Institute> Institute { get; set; }

        public DbSet<Profession> Professions { get; set; }

        public DbSet<Student> Student { get; set; }

        public DbSet<Cource> Cources { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Learing> Learings { get; set; }

        public DbSet<Progress> Progresses { get; set; }

        public DbSet<SingleChoices> SingleChoices { get; set; }

        public DbSet<MultipleChoices> MultipleChoices { get; set; }

        public DbSet<JudgeChoices> JudgeChoices { get; set; }

        public DbSet<ExaminationPaper> ExaminationPapers { get; set;  }

        public DbSet<ExamSingleChoices> ExamSingleChoices { get; set; }

        public DbSet<ExamJudgeChoices> ExamJudgeChoices { get; set; }

        public DbSet<ApplicationJoinTheExamination> ApplicationJoinTheExaminations { get; set; }

        public DbSet<ApplicationForReExamination> ApplicationForReExaminations { get; set; }

        public DbSet<LogStudentOperation> LogStudentOperations { get; set; }

        public DbSet<LogPricipalOperation> LogPricipalOperations { get; set; }

        public DbSet<LogUserLogin> LogUserLogin { get; set; }

    }
}
