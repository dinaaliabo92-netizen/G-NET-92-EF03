
using G_NET_92_EF03.OnlineLearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using G_NET_92_EF03.OnlineLearningPlatform.SeedData;

namespace G_NET_92_EF03.OnlineLearningPlatform.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Parameterless Constructor
        public ApplicationDbContext()
        {
        }

        // Constructor
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        // SQL Server Connection
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=.;Database=OnlineLearningPlatformDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        // Apply Configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);

            // Apply Seed Data
            ApplicationSeedData.Seed(modelBuilder);
        }

    }
}
