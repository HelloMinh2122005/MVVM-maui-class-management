using Microsoft.EntityFrameworkCore;
using MVVMplayground.Interfaces;
using MVVMplayground.Model;

namespace MVVMplayground.Data;

public class StudentResponsitory : IStudentResponsitory
{
    private readonly ClassDbContext context;

    public StudentResponsitory(ClassDbContext _context)
    {
        context = _context;
    }

    public async Task AddStudentAsync(Student student)
    {
        context.Students.Add(student);
        await context.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(Student student)
    {
        context.Students.Remove(student);
        await context.SaveChangesAsync();   
    }

    public async Task<Student> GetStudentByIDAsync(string studentID)
    {
        return await context.Students.FindAsync(studentID) ?? new Student { StudentID = "", StudentName = "XXX" };
    }

    public async Task<IEnumerable<Student>> GetStudentsAsync(string classID)
    {
        return await context.Students.Where(st => st.ClassID == classID).ToListAsync();
    }

    public async Task UpdateStudentAsync(Student student)
    {
        context.Students.Update(student);
        await context.SaveChangesAsync();
    }
}