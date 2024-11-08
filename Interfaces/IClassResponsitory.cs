using MVVMplayground.Model;

namespace MVVMplayground.Interfaces;

public interface IClassResponsitory 
{
    Task<IEnumerable<Class>> GetClassesAsync();
    Task AddClassAsync(Class newClass);
    Task DeleteClassAsync(Class deletedClass);
    Task UpdateClassAsync(Class editedClass);
    Task<Class> GetClassByID(string id);
}