using MVVMplayground.ViewModel.ClassViewModel;

namespace MVVMplayground.View.ClassView;

public partial class ClassEditView : ContentPage
{
	public ClassEditView(ClassEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}