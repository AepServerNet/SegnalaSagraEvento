using Microsoft.AspNetCore.Components;
using SagreEventi.Shared.Models;

namespace SagreEventi.Web.Client.Pages;

public partial class Index : ComponentBase
{
    public List<EventoModel> ListaEventi { get; set; }
    public int EventiDaSincronizzare { get; set; }
    public bool RefreshApp { get; set; } = false;

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