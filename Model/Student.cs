using System.ComponentModel.DataAnnotations.Schema;

namespace MVVMplayground.Model;

[Table("HOCSINH")]
public class Student
{
    public string StudentID { get; set; } = "";
    public string StudentName { get; set; } = "";
    public string StudentDOB { get; set; } = "";

    public string ClassID { get; set; } = "";
    public Class Class { get; set; } = new Class();
}