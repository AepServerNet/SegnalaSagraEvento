using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SagreEventi.Web.Client.Components;

public partial class SyncEventi : ComponentBase
{
    [Parameter]
    public EventCallback ForceRefreshEventCallback { get; set; }

    public bool onLine { get; set; }
    public bool IsBusy { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    [JSInvokable("Connection.StatusChanged")]
    public void OnConnectionStatusChanged(bool isOnline)
    {
        if (onLine != isOnline)
        {
            onLine = isOnline;
            IsBusy = !isOnline;
        }

        StateHasChanged();
    }
    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync("Connection.Initialize", DotNetObjectReference.Create(this));
        }
    }

    public void Dispose()
    {
        JsRuntime.InvokeVoidAsync("Connection.Dispose");
    }

    public async Task Synchronize()
    {
        IsBusy = true;

        await eventiLocalStorage.EseguiSyncWithDatabase();
        await ForceRefreshEventCallback.InvokeAsync();

        IsBusy = false;
    }
}