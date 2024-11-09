using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVMplayground.Interfaces;
using MVVMplayground.Model;

namespace MVVMplayground.ViewModel.StudentViewModel;

[QueryProperty("Studentid", "Studentid")]
[QueryProperty("Classid", "Classid")]
public partial class StudentEditViewModel : ObservableObject
{
    private readonly IStudentResponsitory _istudentResponsitory;

    [ObservableProperty]
    public string _studentid;
    
    [ObservableProperty]
    public string _studentname;

    [ObservableProperty]
    public string _classid;

    [ObservableProperty]
    public string _studentdob;

    public StudentEditViewModel(IStudentResponsitory studentResponsitory)
    {
        _studentid = "";
        _studentname = "";
        _classid = "";
        _studentdob = "";
        _istudentResponsitory = studentResponsitory;
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
            await _istudentResponsitory.UpdateStudentAsync(newStudent);
            await Shell.Current.GoToAsync($"..?edited={Studentid}");
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