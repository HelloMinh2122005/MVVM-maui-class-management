using MVVMplayground.Model;

namespace MVVMplayground.Interfaces;

public interface IStudentResponsitory
{
    Task<IEnumerable<Student>> GetStudentsAsync(string classID);
    Task AddStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(Student student);
    Task<Student> GetStudentByIDAsync(string studentID);
}
