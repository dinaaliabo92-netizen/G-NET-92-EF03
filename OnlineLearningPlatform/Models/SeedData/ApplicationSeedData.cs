
using G_NET_92_EF03.OnlineLearningPlatform.Models;
using G_NET_92_EF03.OnlineLearningPlatform.Enums;
using Microsoft.EntityFrameworkCore;

namespace G_NET_92_EF03.OnlineLearningPlatform.SeedData
{
    public static class ApplicationSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Instructors
            modelBuilder.Entity<Instructor>().HasData(
                new Instructor
                {
                    Id = 1,
                    FullName = "Ahmed Mohamed",
                    Email = "ahmed@example.com",
                    Bio = "Software Development Instructor",
                    JoinedDate = new DateTime(2025, 1, 1)
                }
            );

            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Programming",
                    Description = "Programming Courses",
                    ParentCategoryId = null
                },
                new Category
                {
                    Id = 2,
                    Name = "Backend",
                    Description = "Backend Development",
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 3,
                    Name = "ASP.NET",
                    Description = "ASP.NET Courses",
                    ParentCategoryId = 2
                },
                new Category
                {
                    Id = 4,
                    Name = "Design",
                    Description = "Design Courses",
                    ParentCategoryId = null
                }
            );

            // Courses
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "C# Fundamentals",
                    Description = "Learn C# Basics",
                    Price = 500,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedDate = new DateTime(2025, 2, 1),
                    InstructorId = 1,
                    CategoryId = 1
                },
                new Course
                {
                    Id = 2,
                    Title = "ASP.NET Core",
                    Description = "Learn ASP.NET Core",
                    Price = 800,
                    Level = CourseLevel.Intermediate,
                    IsPublished = true,
                    PublishedDate = new DateTime(2025, 3, 1),
                    InstructorId = 1,
                    CategoryId = 3
                }
            );

            // Students
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FullName = "Ali Hassan",
                    Email = "ali@example.com",
                    DateOfBirth = new DateTime(2000, 5, 10)
                },
                new Student
                {
                    Id = 2,
                    FullName = "Mona Ahmed",
                    Email = "mona@example.com",
                    DateOfBirth = new DateTime(2001, 8, 15)
                },
                new Student
                {
                    Id = 3,
                    FullName = "Omar Samir",
                    Email = "omar@example.com",
                    DateOfBirth = new DateTime(1999, 12, 20)
                }
            );

            // Enrollments
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    StudentId = 1,
                    CourseId = 1,
                    EnrollmentDate = new DateTime(2025, 4, 1),
                    Progress = 100,
                    Status = EnrollmentStatus.Completed,
                    FinalGrade = 95
                },
                new Enrollment
                {
                    StudentId = 2,
                    CourseId = 1,
                    EnrollmentDate = new DateTime(2025, 4, 2),
                    Progress = 50,
                    Status = EnrollmentStatus.Active,
                    FinalGrade = null
                },
                new Enrollment
                {
                    StudentId = 3,
                    CourseId = 2,
                    EnrollmentDate = new DateTime(2025, 4, 3),
                    Progress = 20,
                    Status = EnrollmentStatus.Active,
                    FinalGrade = null
                }
            );
        }
    }
}
