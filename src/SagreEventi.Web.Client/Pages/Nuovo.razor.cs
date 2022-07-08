using Microsoft.AspNetCore.Components;
using SagreEventi.Shared.Models;

namespace SagreEventi.Web.Client.Pages;

public partial class Nuovo : ComponentBase
{
    EventoModel modelEvento = new();

    public async Task HandleValidSubmit()
    {
        await eventiLocalStorage.SalvaEvento(modelEvento);

        modelEvento = new();
        StateHasChanged();

        navigationManager.NavigateTo("/", false);
    }

    public void Cancel()
    {
        modelEvento = new();
        StateHasChanged();

        navigationManager.NavigateTo("/", false);
    }
}
