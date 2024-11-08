using System.ComponentModel.DataAnnotations.Schema;
namespace MVVMplayground.Model;

[Table("LOPHOC")]
public class Class
{
    public string ClassID { get; set; } = "";
    public string ClassName { get; set; } = "";

    List<Student> Students { get; set; } = [];
}