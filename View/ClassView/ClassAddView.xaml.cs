using MVVMplayground.ViewModel.ClassViewModel;

namespace MVVMplayground.View.ClassView;

public partial class ClassAddView : ContentPage
{
    public ClassAddView(ClassAddViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}