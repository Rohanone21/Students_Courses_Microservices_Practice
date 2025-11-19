using Microsoft.EntityFrameworkCore;


namespace Courses_Microservices.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Course entity
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);
                entity.Property(c => c.Title)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(c => c.Description)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(c => c.Credits)
                      .IsRequired();

                // Optional: Add unique constraint on Title
                entity.HasIndex(c => c.Title).IsUnique();

                // Optional: Seed data
                entity.HasData(
                    new Course { CourseId = 1, Title = "Mathematics", Description = "Advanced Mathematics Course", Credits = 4 },
                    new Course { CourseId = 2, Title = "Physics", Description = "Fundamental Physics Principles", Credits = 3 },
                    new Course { CourseId = 3, Title = "Computer Science", Description = "Introduction to Programming", Credits = 4 }
                );
            });
        }
    }
}