using Exercise_Tracker_v2.Controllers;
using Exercise_Tracker_v2.Models;
using Exercise_Tracker.Service;
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
        // Add framework services.
        services.AddControllers();
        
        services.AddMvc();

        // Bind the configuration from appsettings.json
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        // Register the IConfiguration instance which AppSettings binds against.
        services.AddSingleton(configuration);
        
        // Add DbContext
        services.AddDbContext<ExerciseDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Add repository and service classes
        services.AddScoped<IExerciseRepository<Exercise>, ExerciseRepository<Exercise>>();
        services.AddScoped<ExerciseService>();
        
        

        // Add controllers
        services.AddControllersWithViews();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect("/Exercise/Index");
            }
            else
            {
                await next();
            }
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            // Attribute routing
            endpoints.MapControllers();

            // Conventional routing (optional)
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Exercise}/{action=Index}/{id?}");
        });
    }
}