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

        public DbSet<Cource> Cource { get; set; }
        
    }
}
