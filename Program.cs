
using G_NET_92_EF03.OnlineLearningPlatform.Operations;

namespace G_NET_92_EF03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ================= CREATE =================
            Console.WriteLine("\n========== CREATE OPERATIONS ==========");

            CreateOperations.AddInstructor();
            CreateOperations.AddCourse();
            CreateOperations.AddThreeStudents();
            CreateOperations.EnrollStudent();


            // ================= READ =================
            Console.WriteLine("\n========== READ OPERATIONS ==========");

            ReadOperations.ReadCourseById();
            ReadOperations.ReadCourseByTitle();
            ReadOperations.ReadCoursesByPriceRange();
            ReadOperations.ReadCategories();
            ReadOperations.ReadCourseRoster();


            // ================= UPDATE =================
            Console.WriteLine("\n========== UPDATE OPERATIONS ==========");

            UpdateOperations.UpdateCoursePrice();
            UpdateOperations.UpdateEnrollmentProgress();
            UpdateOperations.UpdateCoursePublication();


            // ================= DELETE =================
            Console.WriteLine("\n========== DELETE OPERATIONS ==========");

            DeleteOperations.UnenrollStudent();
            DeleteOperations.TryDeleteCategoryWithSubcategories();
            DeleteOperations.DeleteCategorySafely();
            DeleteOperations.DeleteCourse();


            Console.WriteLine("\nAll operations finished.");
            Console.ReadKey();
        }
    }
}
