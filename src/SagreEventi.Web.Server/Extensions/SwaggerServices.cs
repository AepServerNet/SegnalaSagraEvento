using System.Reflection;
using Microsoft.OpenApi.Models;

namespace SagreEventi.Web.Server.Extensions;

public static class SwaggerServices
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Sagre ed Eventi",
                Version = "v1",
                Description = "Application API that keeps track of parties and food and wine events reported by users",

                Contact = new OpenApiContact
                {
                    Name = "Angelo Pirola",
                    Email = "angelo@aepserver.it",
                    Url = new Uri("https://about.me/AngeloPirola"),
                },

                License = new OpenApiLicense
                {
                    Name = "Licenza MIT",
                    Url = new Uri("https://it.wikipedia.org/wiki/Licenza_MIT"),
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            config.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}