using Student_MVC_Project.Models;
namespace Student_MVC_Project.Service
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);  
        Task<Student> CreateStudentAsync(Student student);

        Task<Student> UpdateStudentAsync(int id,Student student);
        Task<bool> DeleteStudentAsync(int id);
    }
}
