
using G_NET_92_EF03.OnlineLearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_NET_92_EF03.OnlineLearningPlatform.Configuration
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            // Primary Key
            builder.HasKey(i => i.Id);

            // Full Name
            builder.Property(i => i.FullName)
                .IsRequired()
                .HasMaxLength(100);

            // Email
            builder.Property(i => i.Email)
                .IsRequired()
                .HasMaxLength(150);

            // Bio
            builder.Property(i => i.Bio)
                .HasMaxLength(500);

            // Joined Date
            builder.Property(i => i.JoinedDate)
                .IsRequired();

            // Relationship: Instructor 1 : Many Courses
            builder.HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
