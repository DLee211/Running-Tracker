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

        services.AddControllers();
        
        services.AddMvc();


        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();


        services.AddSingleton(configuration);
        
        services.AddDbContext<ExerciseDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


        services.AddScoped<IExerciseRepository<Exercise>, ExerciseRepository<Exercise>>();
        services.AddScoped<ExerciseService>();
        
        


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
            endpoints.MapControllers();
            
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Exercise}/{action=Index}/{id?}");
        });
    }
}