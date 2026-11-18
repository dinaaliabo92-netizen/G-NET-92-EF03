
using G_NET_92_EF03.OnlineLearningPlatform.Data;

namespace G_NET_92_EF03.OnlineLearningPlatform.Operations
{
    public class ReadOperations
    {
        // 1. Find Course by ID using Find()
        public static void ReadCourseById()
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

                Console.WriteLine($"ID: {course.Id}");
                Console.WriteLine($"Title: {course.Title}");
                Console.WriteLine($"Price: {course.Price}");
                Console.WriteLine($"Level: {course.Level}");
            }
        }

        // 2. Find Course by Title using FirstOrDefault()
        public static void ReadCourseByTitle()
        {
            using (var context = new ApplicationDbContext())
            {
                Console.Write("Enter Course Title: ");
                string title = Console.ReadLine() ?? "";

                var course = context.Courses.FirstOrDefault(
                    c => c.Title == title);

                if (course == null)
                {
                    Console.WriteLine("Course not found!");
                    return;
                }

                Console.WriteLine($"ID: {course.Id}");
                Console.WriteLine($"Title: {course.Title}");
                Console.WriteLine($"Price: {course.Price}");
                Console.WriteLine($"Level: {course.Level}");
            }
        }

        // 3. Filter Courses by Price Range and Order Descending
        public static void ReadCoursesByPriceRange()
        {
            using (var context = new ApplicationDbContext())
            {
                Console.Write("Enter Minimum Price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
                {
                    Console.WriteLine("Invalid Minimum Price!");
                    return;
                }

                Console.Write("Enter Maximum Price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
                {
                    Console.WriteLine("Invalid Maximum Price!");
                    return;
                }

                if (minPrice > maxPrice)
                {
                    Console.WriteLine("Invalid Price Range!");
                    return;
                }

                var courses = context.Courses
                    .Where(c => c.Price >= minPrice &&
                                c.Price <= maxPrice)
                    .OrderByDescending(c => c.Price)
                    .ToList();

                if (courses.Count == 0)
                {
                    Console.WriteLine("No courses found!");
                    return;
                }

                foreach (var course in courses)
                {
                    Console.WriteLine(
                        $"{course.Title} - {course.Level} - {course.Price}");
                }
            }
        }

        // 4. Read Direct Subcategories and Top-Level Categories
        public static void ReadCategories()
        {
            using (var context = new ApplicationDbContext())
            {
                Console.Write("Enter Parent Category ID: ");
                if (!int.TryParse(Console.ReadLine(), out int parentId))
                {
                    Console.WriteLine("Invalid Category ID!");
                    return;
                }

                var subCategories = context.Categories
                    .Where(c => c.ParentCategoryId == parentId)
                    .ToList();

                Console.WriteLine("Direct Subcategories:");

                foreach (var category in subCategories)
                {
                    Console.WriteLine($"{category.Id} - {category.Name}");
                }

                var topLevelCategories = context.Categories
                    .Where(c => c.ParentCategoryId == null)
                    .ToList();

                Console.WriteLine("Top-Level Categories:");

                foreach (var category in topLevelCategories)
                {
                    Console.WriteLine($"{category.Id} - {category.Name}");
                }
            }
        }

        // 5. Read Course Roster without Joins
        public static void ReadCourseRoster()
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

                // Get Student IDs from Enrollments first
                var studentIds = context.Enrollments
                    .Where(e => e.CourseId == courseId)
                    .Select(e => e.StudentId)
                    .ToList();

                Console.WriteLine(
                    $"Enrollment Count: {studentIds.Count}");

                // Get Students using Contains()
                var students = context.Students
                    .Where(s => studentIds.Contains(s.Id))
                    .ToList();

                foreach (var student in students)
                {
                    Console.WriteLine(student.FullName);
                }
            }
        }
    }
}
