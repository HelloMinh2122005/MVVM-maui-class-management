using MVVMplayground.ViewModel.StudentViewModel;

namespace MVVMplayground.View.StudentView;

public partial class StudentEditView : ContentPage
{
	public StudentEditView(StudentEditViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}