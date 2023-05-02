using CMSPlus.Domain;
using CMSPlus.Application;
using CMSPlus.Infrastructure;

namespace CMSPlus.Presentation;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");


        services.AddSingleton(env);
        services.AddSingleton(Configuration);
        services.AddInfrastructure(connectionString);
        services.AddMigrations(connectionString);
        
        services.AddPresentation();
        services.AddAutoMapper();

        services.AddServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}