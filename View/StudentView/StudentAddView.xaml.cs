using MVVMplayground.ViewModel.StudentViewModel;

namespace MVVMplayground.View.StudentView;

public partial class StudentAddView : ContentPage
{
	public StudentAddView(StudentAddViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}