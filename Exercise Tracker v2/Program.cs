using Exercise_Tracker;

var host = CreateHostBuilder(args).Build();

// Clear and create the database
using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ExerciseDbContext>();
        context.Database.EnsureDeleted(); // Ensure that the database is deleted
        context.Database.EnsureCreated(); // Ensure that the database is created based on the current model
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while creating the database.");
        Console.WriteLine(ex.Message);
    }
}

host.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });