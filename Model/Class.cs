using System.ComponentModel.DataAnnotations.Schema;
namespace MVVMplayground.Model;

[Table("Class")]
public class Class
{
    public string ClassID { get; set; } = Guid.NewGuid().ToString();
    public string ClassName { get; set; } = "";

    public List<Student> Students { get; set; } = [];
}