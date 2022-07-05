namespace SagreEventi.Shared.Models;

public class EventiStore
{
    public EventiStore()
    {
        DataOraUltimoSyncServer = DateTime.MinValue;

        ListaEventi = new List<EventoModel>();
    }

    public List<EventoModel> ListaEventi { get; set; }
    public DateTime DataOraUltimoSyncServer { get; set; }
}