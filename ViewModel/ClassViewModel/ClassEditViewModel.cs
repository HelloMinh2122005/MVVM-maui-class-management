using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MVVMplayground.Model;
using MVVMplayground.Interfaces;

namespace MVVMplayground.ViewModel.ClassViewModel;

[QueryProperty("Classid", "Classid")]
public partial class ClassEditViewModel : ObservableObject
{
    private readonly IClassResponsitory _iclassrepo;

    public ClassEditViewModel(IClassResponsitory iclass)
    {
        _iclassrepo = iclass;
        classid = "";
    }

    [ObservableProperty]
    string classid;

    [ObservableProperty]
    string classname = "";

    [RelayCommand]
    async Task Save()
    {
        Class cls = await _iclassrepo.GetClassByID(Classid);
        cls.ClassName = Classname;
        await _iclassrepo.UpdateClassAsync(cls);
        await Shell.Current.GoToAsync($"..?edited={Classid}");
    }

    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}