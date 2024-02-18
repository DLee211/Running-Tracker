using Exercise_Tracker_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise_Tracker;

public class ExerciseDbContext : DbContext
{

    private readonly IConfiguration _configuration;

    public ExerciseDbContext(DbContextOptions<ExerciseDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        IConfiguration section = _configuration.GetSection("ConnectionStrings");

        string connectionString = section["ExerciseDbConnection"];
        
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
    }
}