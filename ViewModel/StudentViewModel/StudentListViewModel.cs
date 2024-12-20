﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVMplayground.Interfaces;
using MVVMplayground.Model;
using MVVMplayground.View.StudentView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMplayground.ViewModel.StudentViewModel;

[QueryProperty("Classid","Classid")]
public partial class StudentListViewModel : ObservableObject, IQueryAttributable
{
    private readonly IStudentResponsitory _istudentResponsitory;

    private Student currentStudent;

    public StudentListViewModel(IStudentResponsitory studentResponsitory)
    {
        _istudentResponsitory = studentResponsitory;
        StudentList = new ObservableCollection<Student>();
        currentStudent = new Student { StudentName = "XX" };
        _classid = "";
        LoadStudentAsync().ConfigureAwait(false);
    }

    [ObservableProperty]
    string _classid;

    [ObservableProperty]
    ObservableCollection<Student> _studentList;

    async void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Classid"))
        {
            Classid = query["Classid"].ToString() ?? "";
            await LoadStudentAsync();
        }
        if (query.ContainsKey("saved") || query.ContainsKey("edited"))
        {
            await LoadStudentAsync();
        }
    }

    [RelayCommand]
    public void Tap1(Student student)
    {
        currentStudent = student;
    }

    [RelayCommand]
    async Task Add()
    {
        await Shell.Current.GoToAsync($"{nameof(StudentAddView)}?Classid={Classid}");
    }

    [RelayCommand]
    async Task Del()
    {
        if (currentStudent.StudentID == "" && currentStudent.StudentName == "XX")
        {
            await Shell.Current.DisplayAlert("Thông báo", "Chưa chọn học sinh để xóa", "OK");
            return;
        }
        await _istudentResponsitory.DeleteStudentAsync(currentStudent);
        StudentList.Remove(currentStudent);
        currentStudent.ClassID = "";
        currentStudent.StudentName = "XX";
    }

    [RelayCommand]
    async Task Edit()
    {
        if (currentStudent.StudentID == "" && currentStudent.StudentName == "XX")
        {
            await Shell.Current.DisplayAlert("Thông báo", "Chưa chọn học sinh để sửa", "OK");
            return;
        }

        await Shell.Current.GoToAsync(nameof(StudentEditView), new Dictionary<string, object>
        {
            { "StudentPara", currentStudent }
        });
    }

    public async Task LoadStudentAsync()
    {
        StudentList.Clear();
        var students = await _istudentResponsitory.GetStudentsAsync(Classid);
        foreach (Student student in students)
            StudentList.Add(student);
    }
}