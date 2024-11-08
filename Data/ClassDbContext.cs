using Microsoft.EntityFrameworkCore;
using MVVMplayground.Model;

namespace MVVMplayground.Data;

public class ClassDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Preferences.Get("DATABASE_PATH", "");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
    
    public static string GetDataPath()
    {
        return Preferences.Get("DATABASE_PATH", "");
    }

    public static async Task PickDatabasePathIfNotExist()
    {
        if (!string.IsNullOrEmpty(Preferences.Get("DATABASE_PATH", ""))) return;

        var fileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new[] { ".db", ".sqlite", ".sqlite3" } }
            });

        var dbFile = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Chọn file cơ sở dữ liệu SQLite",
            FileTypes = fileType
        });

        while (dbFile == null)
        {
            if (App.Current?.MainPage == null) return;
            await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn file cơ sở dữ liệu SQLite", "OK");

            dbFile = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Chọn file cơ sở dữ liệu SQLite",
                FileTypes = fileType
            });
        }

        Preferences.Set("DATABASE_PATH", dbFile.FullPath);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().Property(st => st.StudentID).HasColumnName("ID");
        modelBuilder.Entity<Student>().Property(st => st.StudentName).HasColumnName("NAME");
        modelBuilder.Entity<Student>().Property(st => st.StudentDOB).HasColumnName("DOB");
        modelBuilder.Entity<Student>().Property(st => st.ClassID).HasColumnName("IDLOP");

        modelBuilder.Entity<Class>().Property(cl => cl.ClassID).HasColumnName("ID");
        modelBuilder.Entity<Class>().Property(cl => cl.ClassName).HasColumnName("NAME");
    }
}