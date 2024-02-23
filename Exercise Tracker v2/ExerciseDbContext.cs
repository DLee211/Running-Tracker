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
            new Exercise { Id = 1, DateStart = new DateTime(2024, 1, 1), DateEnd = new DateTime(2024, 1, 2), Duration = 1, Comments = "Test 1" },
            new Exercise { Id = 2, DateStart = new DateTime(2024, 1, 3), DateEnd = new DateTime(2024, 1, 4), Duration = 2, Comments = "Test 2" },
            new Exercise { Id = 3, DateStart = new DateTime(2024, 1, 5), DateEnd = new DateTime(2024, 1, 6), Duration = 3, Comments = "Test 3" },
            new Exercise { Id = 4, DateStart = new DateTime(2024, 1, 7), DateEnd = new DateTime(2024, 1, 8), Duration = 4, Comments = "Test 4" },
            new Exercise { Id = 5, DateStart = new DateTime(2024, 1, 9), DateEnd = new DateTime(2024, 1, 10), Duration = 7, Comments = "Test 5" },
            new Exercise { Id = 6, DateStart = new DateTime(2024, 1, 11), DateEnd = new DateTime(2024, 1, 12), Duration = 10, Comments = "Test 6" },
            new Exercise { Id = 7, DateStart = new DateTime(2024, 1, 13), DateEnd = new DateTime(2024, 1, 14), Duration = 3, Comments = "Test 7" },
            new Exercise { Id = 8, DateStart = new DateTime(2024, 1, 15), DateEnd = new DateTime(2024, 1, 16), Duration = 4, Comments = "Test 8" },
            new Exercise { Id = 9, DateStart = new DateTime(2024, 1, 17), DateEnd = new DateTime(2024, 1, 18), Duration = 6, Comments = "Test 9" },
            new Exercise { Id = 10, DateStart = new DateTime(2024, 1, 19), DateEnd = new DateTime(2024, 1, 20), Duration = 9, Comments = "Test 10" }
        );
    }
}