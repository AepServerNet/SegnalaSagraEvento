using Microsoft.EntityFrameworkCore;
using SagreEventi.Shared.Models;
using SagreEventi.Web.Server.Models.Services.Infrastructure;

namespace SagreEventi.Web.Server.Models.Services.Application;

public class EfCoreEventiService : IEventiService
{
    private readonly ILogger<EfCoreEventiService> logger;
    private readonly AppDbContext dbContext;

    public EfCoreEventiService(ILogger<EfCoreEventiService> logger, AppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    public async Task<List<EventoModel>> GetEventi(DateTime since)
    {
        return await dbContext.Eventi.Where(x => x.DataOraUltimaModifica > since).ToListAsync();
    }

    public async Task<List<EventoModel>> GetEventiScaduti(DateTime dataOdierna)
    {
        List<EventoModel> listaEventi = await dbContext.Eventi.Where(x => dataOdierna > x.DataFineEvento).ToListAsync();

        return listaEventi;
    }

    public async Task UpdateEventi(List<EventoModel> eventi)
    {
        foreach (var todoitem in eventi)
        {
            var listaEventi = await dbContext.Eventi.Where(x => x.Id == todoitem.Id).FirstOrDefaultAsync();
            if (listaEventi == null)
            {
                if (!todoitem.EventoConcluso)
                {
                    dbContext.Eventi.Add(todoitem);
                }
            }
            else
            {
                if (listaEventi.DataOraUltimaModifica < todoitem.DataOraUltimaModifica)
                {
                    dbContext.Entry(listaEventi).CurrentValues.SetValues(todoitem);
                }
            }
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateEvento(EventoModel evento)
    {
        var avvenimento = await dbContext.Eventi.FindAsync(evento.Id);

        avvenimento.NomeEvento = evento.NomeEvento;
        avvenimento.CittaEvento = evento.CittaEvento;
        avvenimento.DataInizioEvento = evento.DataInizioEvento;
        avvenimento.DataFineEvento = evento.DataFineEvento;
        avvenimento.DescrizioneEvento = evento.DescrizioneEvento;
        avvenimento.EventoConcluso = true;

        await dbContext.SaveChangesAsync();
    }
}