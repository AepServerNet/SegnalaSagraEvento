using SagreEventi.Shared.Models;

namespace SagreEventi.Web.Server.Models.Services.Application;

public interface IEventiService
{
    Task<List<EventoModel>> GetEventi(DateTime since);
    Task UpdateEventi(List<EventoModel> eventi);
    Task<List<EventoModel>> GetEventiScaduti(DateTime dataOdierna);
    Task UpdateEvento(EventoModel evento);
}