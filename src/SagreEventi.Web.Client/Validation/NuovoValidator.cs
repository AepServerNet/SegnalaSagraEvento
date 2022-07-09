using FluentValidation;
using SagreEventi.Shared.Models;

namespace SagreEventi.Web.Client.Validation;

public class NuovoValidator : AbstractValidator<EventoModel>
{
    public NuovoValidator()
    {
        RuleFor(x => x.NomeEvento)
            .NotEmpty().WithMessage("Il nome dell'evento è obbligatorio")
            .MinimumLength(10).WithMessage("Il nome dell'evento dev'essere di almeno {MinLength} caratteri")
            .MaximumLength(200).WithMessage("Il nome dell'evento dev'essere al massimo {MaxLength} caratteri");

        RuleFor(x => x.CittaEvento)
            .NotEmpty().WithMessage("Il luogo dell'evento è obbligatorio");

        RuleFor(x => x.DataInizioEvento)
            .NotEmpty().WithMessage("La data di inizio dell'evento è obbligatoria");

        RuleFor(x => x.DataFineEvento)
            .NotEmpty().WithMessage("La data di fine dell'evento è obbligatoria");

        RuleFor(x => x.DescrizioneEvento)
            .NotEmpty().WithMessage("La descrizione dell'evento è obbligatoria")
            .MinimumLength(10).WithMessage("La descrizione dell'evento dev'essere di almeno {MinLength} caratteri")
            .MaximumLength(4000).WithMessage("La descrizione dell'evento dev'essere al massimo {MaxLength} caratteri");
    }
}