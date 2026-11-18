
using G_NET_92_EF03.OnlineLearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_NET_92_EF03.OnlineLearningPlatform.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            // Primary Key
            builder.HasKey(c => c.Id);

            // Title
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Description
            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            // Price
            builder.Property(c => c.Price)
                .HasPrecision(10, 2)
                .IsRequired();

            // Level
            builder.Property(c => c.Level)
                .IsRequired();

            // IsPublished
            builder.Property(c => c.IsPublished)
                .IsRequired();

            // PublishedDate
            builder.Property(c => c.PublishedDate)
                .IsRequired(false);

            // Instructor 1 : Many Courses
            builder.HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category 1 : Many Courses
            builder.HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
