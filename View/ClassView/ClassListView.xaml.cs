using MVVMplayground.ViewModel.ClassViewModel;

namespace MVVMplayground.View.ClassView;

public partial class ClassListView : ContentPage
{
	public ClassListView(ClassListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}