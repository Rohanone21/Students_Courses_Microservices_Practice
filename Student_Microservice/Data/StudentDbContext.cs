using Microsoft.EntityFrameworkCore;
using Student_Microservice.Models;

namespace Student_Microservice.Data
{
    public class StudentDbContext:DbContext
    {

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { } 

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.EnrollmentDate).IsRequired();
            });
        }

    }
}
