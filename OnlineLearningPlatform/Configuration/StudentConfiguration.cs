
using G_NET_92_EF03.OnlineLearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_NET_92_EF03.OnlineLearningPlatform.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Primary Key
            builder.HasKey(s => s.Id);

            // Full Name
            builder.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(150);

            // Email
            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(150);

            // Date Of Birth
            builder.Property(s => s.DateOfBirth)
                .IsRequired();

            // Relationship: Student 1 : Many Enrollments
            builder.HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
