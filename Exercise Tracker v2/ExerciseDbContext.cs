using Exercise_Tracker_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise_Tracker;

public class ExerciseDbContext : DbContext
{

    private readonly IConfiguration _configuration;

    public ExerciseDbContext(DbContextOptions<ExerciseDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        string connectionString = _configuration.GetConnectionString("ExerciseDbConnection");

        // Use the retrieved connection string with SQL Server
        optionsBuilder.UseSqlServer(connectionString);
    }
    
    public DbSet<Exercise> Exercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.ToTable("Exercises");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DateStart).IsRequired();
            entity.Property(e => e.DateEnd).IsRequired();
            entity.Property(e => e.Duration).IsRequired();
            entity.Property(e => e.Comments).HasMaxLength(1000);
        });
        
        modelBuilder.Entity<Exercise>().HasData(
            new Exercise { Id = 1, DateStart = "2024-01-01", DateEnd = "2024-01-02", Duration = "1:00:00", Comments = "Test 1" },
            new Exercise { Id = 2, DateStart = "2024-01-03", DateEnd = "2024-01-04", Duration = "1:30:00", Comments = "Test 2" },
            new Exercise { Id = 3, DateStart = "2024-01-05", DateEnd = "2024-01-06", Duration = "2:00:00", Comments = "Test 3" },
            new Exercise { Id = 4, DateStart = "2024-01-07", DateEnd = "2024-01-08", Duration = "1:15:00", Comments = "Test 4" },
            new Exercise { Id = 5, DateStart = "2024-01-09", DateEnd = "2024-01-10", Duration = "1:45:00", Comments = "Test 5" },
            new Exercise { Id = 6, DateStart = "2024-01-11", DateEnd = "2024-01-12", Duration = "1:20:00", Comments = "Test 6" },
            new Exercise { Id = 7, DateStart = "2024-01-13", DateEnd = "2024-01-14", Duration = "2:30:00", Comments = "Test 7" },
            new Exercise { Id = 8, DateStart = "2024-01-15", DateEnd = "2024-01-16", Duration = "1:10:00", Comments = "Test 8" },
            new Exercise { Id = 9, DateStart = "2024-01-17", DateEnd = "2024-01-18", Duration = "2:15:00", Comments = "Test 9" },
            new Exercise { Id = 10, DateStart = "2024-01-19", DateEnd = "2024-01-20", Duration = "1:50:00", Comments = "Test 10" }
        );
    }
}