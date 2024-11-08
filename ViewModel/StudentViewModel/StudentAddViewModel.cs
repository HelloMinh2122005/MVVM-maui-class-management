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
        if (Studentname == "" || Studentid == "" || Studentdob == "")
        {
            await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin.", "OK");
            return;
        }

        Student newStudent = new Student
        {
            ClassID = Classid,
            StudentName = Studentname,
            StudentID = Studentid,
            StudentDOB = Studentdob
        };

        try
        {
            await _istudentResponsitory.AddStudentAsync(newStudent);
            await Shell.Current.GoToAsync($"..?saved={Studentid}");
        }
        catch (Exception ex)
        {
            var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {innerExceptionMessage}", "OK");
        }
    }

    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}