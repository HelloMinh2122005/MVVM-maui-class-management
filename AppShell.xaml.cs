using MVVMplayground.View.ClassView;
using MVVMplayground.View.StudentView;

namespace MVVMplayground;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ClassListView), typeof(ClassListView));
        Routing.RegisterRoute(nameof(ClassAddView), typeof(ClassAddView));
        Routing.RegisterRoute(nameof(ClassEditView), typeof(ClassEditView));

        Routing.RegisterRoute(nameof(StudentListView), typeof(StudentListView));
        Routing.RegisterRoute(nameof(StudentAddView), typeof(StudentAddView));
    }
}
