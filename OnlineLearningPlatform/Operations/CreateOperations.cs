
using G_NET_92_EF03.OnlineLearningPlatform.Data;
using G_NET_92_EF03.OnlineLearningPlatform.Models;
using G_NET_92_EF03.OnlineLearningPlatform.Enums;

namespace G_NET_92_EF03.OnlineLearningPlatform.Operations
{
    public class CreateOperations
    {
        
        public static void AddInstructor()
        {
            using (var context = new ApplicationDbContext())
            {
                Console.Write("Enter Instructor Full Name: ");
                string fullName = Console.ReadLine() ?? "";

                Console.Write("Enter Instructor Email: ");
                string email = Console.ReadLine() ?? "";

                Console.Write("Enter Instructor Bio: ");
                string bio = Console.ReadLine() ?? "";

                var instructor = new Instructor
                {
                    FullName = fullName,
                    Email = email,
                    Bio = bio,
                    JoinedDate = DateTime.Now
                };

                context.Instructors.Add(instructor);
                context.SaveChanges();

                Console.WriteLine("Instructor added successfully!");
                Console.WriteLine($"Instructor ID: {instructor.Id}");
            }
        }

        public static void AddCourse()
        {
            using (var context = new ApplicationDbContext())
            {
                Console.Write("Enter Instructor ID: ");
                if (!int.TryParse(Console.ReadLine(), out int instructorId))
                {
                    Console.WriteLine("Invalid Instructor ID!");
                    return;
                }

                var instructor = context.Instructors.Find(instructorId);

                if (instructor == null)
                {
                    Console.WriteLine("Instructor not found!");
                    return;
                }

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

                Console.Write("Enter Course Title: ");
                string title = Console.ReadLine() ?? "";

                Console.Write("Enter Course Description: ");
                string description = Console.ReadLine() ?? "";

                Console.Write("Enter Course Price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
                {
                    Console.WriteLine("Invalid Price!");
                    return;
                }

                Console.WriteLine("Select Course Level:");
                Console.WriteLine("1 - Beginner");
                Console.WriteLine("2 - Intermediate");
                Console.WriteLine("3 - Advanced");

                if (!int.TryParse(Console.ReadLine(), out int levelChoice))
                {
                    Console.WriteLine("Invalid Level!");
                    return;
                }

                CourseLevel level;

                switch (levelChoice)
                {
                    case 1:
                        level = CourseLevel.Beginner;
                        break;
                    case 2:
                        level = CourseLevel.Intermediate;
                        break;
                    case 3:
                        level = CourseLevel.Advanced;
                        break;
                    default:
                        Console.WriteLine("Invalid Level!");
                        return;
                }

                var course = new Course
                {
                    Title = title,
                    Description = description,
                    Price = price,
                    Level = level,
                    IsPublished = false,
                    PublishedDate = null,
                    InstructorId = instructorId,
                    CategoryId = categoryId
                };

                context.Courses.Add(course);
                context.SaveChanges();

                Console.WriteLine("Course added successfully!");
                Console.WriteLine($"Course ID: {course.Id}");
            }
        }

        // 3. Add Three Students using AddRange
        public static void AddThreeStudents()
        {
            using (var context = new ApplicationDbContext())
            {
                var students = new List<Student>();

                for (int i = 1; i <= 3; i++)
                {
                    Console.WriteLine($"Enter Student {i} Data:");

                    Console.Write("Full Name: ");
                    string fullName = Console.ReadLine() ?? "";

                    Console.Write("Email: ");
                    string email = Console.ReadLine() ?? "";

                    Console.Write("Date Of Birth (yyyy-MM-dd): ");

                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                    {
                        Console.WriteLine("Invalid Date!");
                        return;
                    }

                    students.Add(new Student
                    {
                        FullName = fullName,
                        Email = email,
                        DateOfBirth = dateOfBirth
                    });
                }

                context.Students.AddRange(students);
                context.SaveChanges();

                Console.WriteLine("Three Students added successfully!");
            }
        }

        public static void EnrollStudent()
        {
            using (var context = new ApplicationDbContext())
            {
                Console.Write("Enter Student ID: ");
                if (!int.TryParse(Console.ReadLine(), out int studentId))
                {
                    Console.WriteLine("Invalid Student ID!");
                    return;
                }

                var student = context.Students.Find(studentId);

                if (student == null)
                {
                    Console.WriteLine("Student not found!");
                    return;
                }

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

                if (!course.IsPublished)
                {
                    Console.WriteLine("Cannot enroll in an unpublished course!");
                    return;
                }

                var existingEnrollment =
                    context.Enrollments.Find(studentId, courseId);

                if (existingEnrollment != null)
                {
                    Console.WriteLine("Student is already enrolled!");
                    return;
                }

                var enrollment = new Enrollment
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 0,
                    Status = EnrollmentStatus.Active,
                    FinalGrade = null
                };

                context.Enrollments.Add(enrollment);
                context.SaveChanges();

                Console.WriteLine("Student enrolled successfully!");
            }
        }
    }
}
