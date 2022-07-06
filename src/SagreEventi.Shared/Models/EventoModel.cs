namespace SagreEventi.Shared.Models;
public class EventoModel
{
    //public int Id { get; set; }
    public string Id { get; set; }
    public string NomeEvento { get; set; }
    public string DescrizioneEvento { get; set; }
    public string CittaEvento { get; set; }
    public DateTime DataOraEvento { get; set; }
    public bool EventoConcluso { get; set; }
    public DateTime DataOraUltimaModifica { get; set; }
}
