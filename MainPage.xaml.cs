using MVVMplayground.View;
using MVVMplayground.View.ClassView;
using MVVMplayground.Data;

namespace MVVMplayground;

public partial class MainPage : ContentPage
{ 
    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ClassDbContext.PickDatabasePathIfNotExist();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ClassListView));        
    }
}
