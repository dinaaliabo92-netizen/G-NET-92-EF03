
using G_NET_92_EF03.OnlineLearningPlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace G_NET_92_EF03.OnlineLearningPlatform.Operations
{
    public class DeleteOperations
    {
        // 1. Unenroll Student from Course
        public static void UnenrollStudent()
        {
            using (var context = new ApplicationDbContext())
            {
                Console.Write("Enter Student ID: ");

                if (!int.TryParse(Console.ReadLine(), out int studentId))
                {
                    Console.WriteLine("Invalid Student ID!");
                    return;
                }

                Console.Write("Enter Course ID: ");

                if (!int.TryParse(Console.ReadLine(), out int courseId))
                {
                    Console.WriteLine("Invalid Course ID!");
                    return;
                }

                // Find Enrollment using Composite Primary Key
                var enrollment = context.Enrollments.Find(studentId, courseId);

                if (enrollment == null)
                {
                    Console.WriteLine("Enrollment not found!");
                    return;
                }

                context.Enrollments.Remove(enrollment);
                context.SaveChanges();

                Console.WriteLine("Student unenrolled successfully!");
            }
        }

        // 2. Attempt to Delete Category with Subcategories
        public static void TryDeleteCategoryWithSubcategories()
        {
            using (var context = new ApplicationDbContext())
            {
                Console.Write("Enter Category ID: ");

                if (!int.TryParse(Console.ReadLine(), out int categoryId))
                {
                    Console.WriteLine("Invalid Category ID!");
                    return;
                }

                var category = context.Categories.Find(categoryId);

                if (category == null)
                {
                    Console.WriteLine("Category not found!");
                    return;
                }

                try
                {
                    context.Categories.Remove(category);
                    context.SaveChanges();

                    Console.WriteLine("Category deleted successfully!");
                }
                catch (DbUpdateException)
                {
                    Console.WriteLine(
                        "Cannot delete this category because it has related subcategories or courses!");
                }
            }
        }

        // 3. Safely Delete Category after Checking Subcategories
        public static void DeleteCategorySafely()
        {
            using (var context = new ApplicationDbContext())
            {
                Console.Write("Enter Category ID: ");

                if (!int.TryParse(Console.ReadLine(), out int categoryId))
                {
                    Console.WriteLine("Invalid Category ID!");
                    return;
                }

                var category = context.Categories.Find(categoryId);

                if (category == null)
                {
                    Console.WriteLine("Category not found!");
                    return;
                }

                bool hasSubcategories = context.Categories
                    .Any(c => c.ParentCategoryId == categoryId);

                if (hasSubcategories)
                {
                    Console.WriteLine(
                        "Cannot delete category because it has subcategories!");
                    return;
                }

                bool hasCourses = context.Courses
                    .Any(c => c.CategoryId == categoryId);

                if (hasCourses)
                {
                    Console.WriteLine(
                        "Cannot delete category because it has courses!");
                    return;
                }

                context.Categories.Remove(category);
                context.SaveChanges();

                Console.WriteLine("Category deleted successfully!");
            }
        }

        // 4. Delete Course and Check Cascade Delete
        public static void DeleteCourse()
        {
            using (var context = new ApplicationDbContext())
            {
                Console.Write("Enter Course ID: ");

                if (!int.TryParse(Console.ReadLine(), out int courseId))
                {
                    Console.WriteLine("Invalid Course ID!");
                    return;
                }

                var course = context.Courses.Find(courseId);

                if (course == null)
                {
                    Console.WriteLine("Course not found!");
                    return;
                }

                int beforeCount = context.Enrollments
                    .Count(e => e.CourseId == courseId);

                Console.WriteLine(
                    $"Enrollments before deletion: {beforeCount}");

                // Cascade Delete removes related enrollments automatically.
                context.Courses.Remove(course);
                context.SaveChanges();

                int afterCount = context.Enrollments
                    .Count(e => e.CourseId == courseId);

                Console.WriteLine(
                    $"Enrollments after deletion: {afterCount}");

                Console.WriteLine("Course deleted successfully!");
            }
        }
    }
}