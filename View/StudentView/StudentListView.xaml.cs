using MVVMplayground.ViewModel.StudentViewModel;

namespace MVVMplayground.View.StudentView;

public partial class StudentListView : ContentPage
{
	public StudentListView(StudentListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}