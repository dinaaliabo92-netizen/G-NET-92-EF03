
using G_NET_92_EF03.OnlineLearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_NET_92_EF03.OnlineLearningPlatform.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Primary Key
            builder.HasKey(c => c.Id);

            // Name
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Description
            builder.Property(c => c.Description)
                .HasMaxLength(500);

            // Self-Referencing Relationship
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Category 1 : Many Courses
            builder.HasMany(c => c.Courses)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
