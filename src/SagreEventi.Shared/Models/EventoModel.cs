using System.ComponentModel.DataAnnotations;

namespace SagreEventi.Shared.Models;
public class EventoModel
{
    //Campo: Id
    public string Id { get; set; }

    //Campo: Nome Evento
    [Required(ErrorMessage = "Il nome dell'evento è obbligatorio")]
    [Display(Name = "Nome Evento")]
    [DataType(DataType.Text)]
    public string NomeEvento { get; set; }

    //Campo: Luogo Evento
    [Required(ErrorMessage = "Il luogo dell'evento è obbligatorio")]
    [Display(Name = "Luogo Evento")]
    [DataType(DataType.Text)]
    public string CittaEvento { get; set; }

    //Campo: Data Inizio Evento
    [Required(ErrorMessage = "La data di inizio dell'evento è obbligatoria")]
    [Display(Name = "Data Inizio Evento")]
    [DataType(DataType.Date)]
    public Nullable<DateTime> DataInizioEvento { get; set; }

    //Campo: Data Fine Evento
    [Required(ErrorMessage = "La data di fine dell'evento è obbligatoria")]
    [Display(Name = "Data Fine Evento")]
    [DataType(DataType.Date)]
    public Nullable<DateTime> DataFineEvento { get; set; }

    //Campo: Descrizione Evento
    [Required(ErrorMessage = "La descrizione dell'evento è obbligatoria")]
    [Display(Name = "Descrizione Evento")]
    [DataType(DataType.Text)]
    public string DescrizioneEvento { get; set; }

    //Campo: Evento concluso - Indica se l'evento è terminato
    public bool EventoConcluso { get; set; }

    //Campo: Data e ora di ultima modifica (utilizzato dal servizio applicativo)
    public DateTime DataOraUltimaModifica { get; set; }
}
