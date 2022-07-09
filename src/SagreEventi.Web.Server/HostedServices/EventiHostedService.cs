using SagreEventi.Shared.Models;
using SagreEventi.Web.Server.Models.Services.Application;

namespace SagreEventi.Web.Server.HostedServices;

public class EventiHostedService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly ILogger logger;

    private Timer timer;

    public EventiHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<EventiHostedService> logger)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        timer = new Timer(
            async state =>
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var dataOdierna = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);

                    using var serviceScope = serviceScopeFactory.CreateScope();
                    var eventiService = serviceScope.ServiceProvider.GetRequiredService<IEventiService>();

                    List<EventoModel> listaEventi = await eventiService.GetEventiScaduti(dataOdierna);
                    int numRecord = listaEventi.Count;

                    if (numRecord > 0)
                    {
                        foreach (var item in listaEventi)
                        {
                            await eventiService.UpdateEvento(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Esecuzione fallita");
                }
            },

            state: null,
            dueTime: TimeSpan.Zero,         // delay per la prima esecuzione
            period: TimeSpan.FromHours(1)); // ripetizione ogni 1 ore

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        timer.Change(Timeout.Infinite, Timeout.Infinite);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        timer.Dispose();
    }
}