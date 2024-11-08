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
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentID);
            entity.Property(e => e.StudentID).HasColumnName("ID");
            entity.Property(e => e.StudentName).HasColumnName("NAME");
            entity.Property(e => e.StudentDOB).HasColumnName("DOB");
            entity.Property(e => e.ClassID).HasColumnName("IDLOP");

            entity.HasOne(e => e.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(e => e.ClassID);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassID);
            entity.Property(e => e.ClassID).HasColumnName("ID");
            entity.Property(e => e.ClassName).HasColumnName("NAME");

            entity.HasMany(e => e.Students)
                .WithOne(s => s.Class)
                .HasForeignKey(s => s.ClassID);
        });
    }
}