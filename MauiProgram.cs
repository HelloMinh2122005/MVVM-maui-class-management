using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVVMplayground.Data;
using MVVMplayground.Interfaces;
using MVVMplayground.View.ClassView;
using MVVMplayground.View.StudentView;
using MVVMplayground.ViewModel.ClassViewModel;
using MVVMplayground.ViewModel.StudentViewModel;

namespace MVVMplayground
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<ClassListView>();
            builder.Services.AddSingleton<ClassListViewModel>();

            builder.Services.AddSingleton<ClassAddView>();
            builder.Services.AddSingleton<ClassAddViewModel>();

            builder.Services.AddSingleton<ClassEditView>();
            builder.Services.AddSingleton<ClassEditViewModel>();

            builder.Services.AddSingleton<StudentListView>();
            builder.Services.AddSingleton<StudentListViewModel>();

            builder.Services.AddSingleton<StudentAddView>();
            builder.Services.AddSingleton<StudentAddViewModel>();

            builder.Services.AddSingleton<StudentEditView>();
            builder.Services.AddSingleton<StudentEditViewModel>();

            builder.Services.AddScoped<IClassResponsitory, ClassRepository>();
            builder.Services.AddScoped<IStudentResponsitory, StudentResponsitory>();

            builder.Services.AddScoped<ClassDbContext>(provider =>
            {
                var dbPath = ClassDbContext.GetDataPath();
                var optionsBuilder = new DbContextOptionsBuilder<ClassDbContext>();
                optionsBuilder.UseSqlite($"Filename={dbPath}"); 
                return new ClassDbContext(optionsBuilder.Options);
            });

            return builder.Build();
        }
    }
}