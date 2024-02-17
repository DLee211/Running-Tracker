using Microsoft.EntityFrameworkCore;

namespace Exercise_Tracker;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configure the DbContext
        services.AddDbContext<ExerciseDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        
        // add services
        

    }

}