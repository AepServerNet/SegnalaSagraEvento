using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SagreEventi.Web.Client.Services;

namespace SagreEventi.Web.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddHttpClient("httpClient01", a => a.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
        builder.Services.AddScoped(a => a.GetRequiredService<IHttpClientFactory>().CreateClient("httpClient01"));

        builder.Services.AddScoped<EventiLocalStorage>();
        builder.Services.AddBlazoredLocalStorage();

        await builder.Build().RunAsync();
    }
}