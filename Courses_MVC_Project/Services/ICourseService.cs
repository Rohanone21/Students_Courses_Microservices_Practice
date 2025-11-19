using Courses_MVC_Project.Models;

namespace Courses_MVC_Project.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task<Course> CreateCourseAsync(Course course);
        Task<bool> UpdateCourseAsync(int id, Course course);
        Task<bool> DeleteCourseAsync(int id);
    }
}
