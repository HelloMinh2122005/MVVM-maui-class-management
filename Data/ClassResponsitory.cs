using MVVMplayground.Model;
using MVVMplayground.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MVVMplayground.Data;

public class ClassRepository(ClassDbContext context) : IClassResponsitory
{
    public async Task<IEnumerable<Class>> GetClassesAsync()
    {
        return await context.Classes.ToListAsync();
    }

    public async Task AddClassAsync(Class newClass)
    {
        context.Classes.Add(newClass);
        await context.SaveChangesAsync();
    }

    public async Task DeleteClassAsync(Class deletedClass)
    {
        context.Classes.Remove(deletedClass);
        await context.SaveChangesAsync();
    }

    public async Task UpdateClassAsync(Class classroom)
    {
        context.Classes.Update(classroom);
        await context.SaveChangesAsync();
    }

    public async Task<Class> GetClassByID(string ID)
    {
        return await context.Classes.FindAsync(ID) ?? new Class { ClassID = "" };
    }
}