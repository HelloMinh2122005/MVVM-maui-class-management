using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MVVMplayground.Interfaces;
using MVVMplayground.Model;
using MVVMplayground.View.ClassView;
using MVVMplayground.View.StudentView;
using System.Collections.ObjectModel;

namespace MVVMplayground.ViewModel.ClassViewModel;

public partial class ClassListViewModel : ObservableObject, IQueryAttributable
{
    private readonly IClassResponsitory _iclassrepo;

    private Class currentClass;
    public ClassListViewModel(IClassResponsitory classrepo)
    {
        ClassesList = new ObservableCollection<Class>();
        _iclassrepo = classrepo;
        currentClass = new Class {ClassName = "XX" };
        LoadClassesAsync().ConfigureAwait(false);
    }

    [ObservableProperty]
    ObservableCollection<Class> _classesList;


    [RelayCommand]
    public void Tap1(Class selectedClass)
    {
        currentClass = selectedClass;
    }

    [RelayCommand]
    async Task Tap2(Class selectedClass)
    {
        if (selectedClass == null) return;
        await Shell.Current.GoToAsync($"{nameof(StudentListView)}?Classid={selectedClass.ClassID}");
    }

    [RelayCommand]
    async Task Add()
    {
        await Shell.Current.GoToAsync(nameof(ClassAddView));
    }

    async void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("saved"))
        {
            string classid = query["saved"].ToString() ?? "";
            if (classid == "")
                return;
            Class newClass = await _iclassrepo.GetClassByID(classid);
            if (newClass.ClassID == "") 
                return;
            ClassesList.Add(newClass);
        }
        if (query.ContainsKey("edited"))
        {
            string classid = query["edited"].ToString() ?? "";
            if (classid == "") 
                return;
            Class editedclass = await _iclassrepo.GetClassByID(classid);
            if(editedclass.ClassID == "")
                return;
            int index = ClassesList.IndexOf(editedclass);
            ClassesList[index] = editedclass;   
        }
    }

    [RelayCommand]
    async Task Del()
    {
        if (currentClass.ClassID == "" && currentClass.ClassName == "XX")
        {
            await Shell.Current.DisplayAlert("Thông báo", "Chưa chọn lớp để xóa", "OK");
            return;
        }
        await _iclassrepo.DeleteClassAsync(currentClass);
        ClassesList.Remove(currentClass);
        currentClass.ClassID = "";
        currentClass.ClassName = "XX";
    }

    [RelayCommand]
    async Task Edit()
    {
        if (string.IsNullOrEmpty(currentClass.ClassID))
        {
            await Shell.Current.DisplayAlert("Thông báo", "Chưa chọn lớp để sửa thông tin", "OK");
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(ClassEditView)}?Classid={currentClass.ClassID}");
    }

    public async Task LoadClassesAsync()
    {
        var classes = await _iclassrepo.GetClassesAsync();
        ClassesList.Clear();
        foreach (var classitem in classes)
            ClassesList.Add(classitem);
    }
}