using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVMplayground.Interfaces;
using MVVMplayground.Model;

namespace MVVMplayground.ViewModel.StudentViewModel;

[QueryProperty(nameof(StudentPara), "StudentPara")]
public partial class StudentEditViewModel : ObservableObject
{
    private readonly IStudentResponsitory _istudentResponsitory;

    [ObservableProperty]
    public Student _studentPara;
    

    public StudentEditViewModel(IStudentResponsitory studentResponsitory, Student st)
    {
        _studentPara = st;
        _istudentResponsitory = studentResponsitory;
    }

    [RelayCommand]
    async Task Save()
    {
        if (StudentPara.StudentName == "" || StudentPara.StudentDOB == "")
        {
            await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin.", "OK");
            return;
        }

        try
        {
            await _istudentResponsitory.UpdateStudentAsync(StudentPara);
            await Shell.Current.GoToAsync($"..?edited={StudentPara.StudentID}");
        }
        catch (Exception ex)
        {
            var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {innerExceptionMessage}", "OK");
        }
    }

    [RelayCommand]
    async Task Cancel() => await Shell.Current.GoToAsync("..");
}