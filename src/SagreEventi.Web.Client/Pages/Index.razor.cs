using Microsoft.AspNetCore.Components;
using SagreEventi.Shared.Models;

namespace SagreEventi.Web.Client.Pages;

public partial class Index : ComponentBase
{
    List<EventoModel> ListaEventi { get; set; }
    int EventiDaSincronizzare { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await RefreshListaEventi();
    }

    public async Task RefreshListaEventi()
    {
        ListaEventi = await eventiLocalStorage.GetListaEventi();
        EventiDaSincronizzare = await eventiLocalStorage.GetEventiDaSincronizzare();

        StateHasChanged();
    }
}