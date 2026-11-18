
using G_NET_92_EF03.OnlineLearningPlatform.Data;
using G_NET_92_EF03.OnlineLearningPlatform.Enums;

namespace G_NET_92_EF03.OnlineLearningPlatform.Operations
{
    public class UpdateOperations
    {
        // 1. Update Course Price
        public static void UpdateCoursePrice()
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

                Console.Write("Enter New Price: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice)
                    || newPrice < 0)
                {
                    Console.WriteLine("Invalid Price!");
                    return;
                }

                course.Price = newPrice;

                context.SaveChanges();

                Console.WriteLine("Course price updated successfully!");
            }
        }

        // 2. Update Enrollment Progress
        public static void UpdateEnrollmentProgress()
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

                Console.Write("Enter New Progress (0-100): ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal progress)
                    || progress < 0 || progress > 100)
                {
                    Console.WriteLine("Progress must be between 0 and 100!");
                    return;
                }

                enrollment.Progress = progress;

                if (progress == 100)
                {
                    enrollment.Status = EnrollmentStatus.Completed;

                    Console.Write("Enter Final Grade: ");

                    if (!decimal.TryParse(Console.ReadLine(), out decimal grade)
                        || grade < 0 || grade > 100)
                    {
                        Console.WriteLine("Invalid Final Grade!");
                        return;
                    }

                    enrollment.FinalGrade = grade;
                }
                else
                {
                    enrollment.Status = EnrollmentStatus.Active;
                    enrollment.FinalGrade = null;
                }

                context.SaveChanges();

                Console.WriteLine("Enrollment updated successfully!");
            }
        }

        // 3. Publish or Unpublish Course
        public static void UpdateCoursePublication()
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

                Console.Write("Publish Course? (true/false): ");

                if (!bool.TryParse(Console.ReadLine(), out bool isPublished))
                {
                    Console.WriteLine("Please enter true or false!");
                    return;
                }

                course.IsPublished = isPublished;

                // PublishedDate is set only when the course is published
                course.PublishedDate = isPublished
                    ? DateTime.Now
                    : null;

                context.SaveChanges();

                Console.WriteLine("Course publication status updated!");
            }
        }
    }
}
