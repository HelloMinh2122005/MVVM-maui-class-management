using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVMplayground.Data;
using MVVMplayground.Interfaces;
using MVVMplayground.Model;

namespace MVVMplayground.ViewModel.StudentViewModel;

[QueryProperty("Classid", "Classid")]
public partial class StudentAddViewModel : ObservableObject
{
    private readonly IStudentResponsitory _istudentResponsitory;

    [ObservableProperty]
    string _classid;

    [ObservableProperty]
    string _studentname;

    [ObservableProperty]
    string _studentid;

    [ObservableProperty]
    string _studentdob;

    public StudentAddViewModel(IStudentResponsitory _istrepo)
    {
        _classid = "";
        _studentname = "";
        _studentid = "";
        _studentdob = "";
        _istudentResponsitory = _istrepo;
    }

    [RelayCommand]
    async Task Save()
    {
        Student newStudent = new Student
        {
            ClassID = Classid,
            StudentName = Studentname,
            StudentID = Studentid,
            StudentDOB = Studentdob
        };
        await _istudentResponsitory.AddStudentAsync(newStudent);
        await Shell.Current.GoToAsync($"..?saved={newStudent.StudentID}");
    }

    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}