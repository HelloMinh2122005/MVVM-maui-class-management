using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MVVMplayground.Interfaces;
using MVVMplayground.Model;

namespace MVVMplayground.ViewModel.ClassViewModel;

public partial class ClassAddViewModel : ObservableObject
{
    private readonly IClassResponsitory _classrepo;
    public ClassAddViewModel(IClassResponsitory classrepo)
    {
        _classrepo = classrepo;
        classID = "";
        className = "";
    }


    [ObservableProperty]
    public string classID;

    [ObservableProperty]
    public string className;

    [RelayCommand]
    async Task Save()
    {
        try
        {
            if (ClassName == "" || ClassID == "")
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin.", "OK");
                return;
            }

            await _classrepo.AddClassAsync(new Class {
                ClassID = this.ClassID,
                ClassName = this.ClassName
            });

            await Shell.Current.GoToAsync($"..?saved={ClassID}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Lỗi", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");    
    }
}