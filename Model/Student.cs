using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVMplayground.Model;

[Table("Student")]
public class Student
{
    public string StudentID { get; set; } = Guid.NewGuid().ToString();
    public string StudentName { get; set; } = "";
    public string StudentDOB { get; set; } = "";

    public string ClassID { get; set; } = "";
    public Class Class { get; set; } = null!;
}