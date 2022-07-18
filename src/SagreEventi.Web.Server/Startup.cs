using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using SagreEventi.Web.Server.Extensions;
using SagreEventi.Web.Server.HostedServices;
using SagreEventi.Web.Server.Models.Services.Application;
using SagreEventi.Web.Server.Models.Services.Infrastructure;

namespace SagreEventi.Web.Server;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });

        services.AddRazorPages();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
        });

        services.AddDbContextPool<AppDbContext>(optionsBuilder =>
        {
            string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                // Info su: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                // Abilito il connection resiliency
                options.EnableRetryOnFailure(3);
            });
        });

        services.AddSingleton<IHostedService, EventiHostedService>();
        services.AddTransient<IEventiService, EfCoreEventiService>();

        services.AddSwaggerServices(Configuration);

        // Options
        services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sagre ed Eventi v1"));
        }

        app.UseHttpsRedirection();
        app.UseBlazorFrameworkFiles();

        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}