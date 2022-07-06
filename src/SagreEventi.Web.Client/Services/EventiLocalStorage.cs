using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace SagreEventi.Shared.Models.Services;

public class EventiLocalStorage
{
    private readonly HttpClient httpClient;
    private readonly ILocalStorageService localStorageService;

    const string EventiLocalStore = "EventiLocalStore";

    public EventiLocalStorage(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        this.httpClient = httpClient;
        this.localStorageService = localStorageService;
    }

    public async Task<EventiStore> GetEventiStore()
    {
        var eventoStore = await localStorageService.GetItemAsync<EventiStore>(EventiLocalStore);

        if (eventoStore == null)
        {
            eventoStore = new EventiStore();
        }

        return eventoStore;
    }

    public async Task SalvaToDoItem(EventoModel eventoModel)
    {
        var eventiStore = await GetEventiStore();

        eventoModel.DataOraUltimaModifica = DateTime.Now;

        if (string.IsNullOrEmpty(eventoModel.Id))
        {
            eventoModel.Id = Guid.NewGuid().ToString();
            eventiStore.ListaEventi.Add(eventoModel);
        }
        else
        {
            if (eventiStore.ListaEventi.Where(x => x.Id == eventoModel.Id).Any())
            {
                eventiStore.ListaEventi[eventiStore.ListaEventi.FindIndex(ind => ind.Id == eventoModel.Id)] = eventoModel;
            }
            else
            {
                eventiStore.ListaEventi.Add(eventoModel);
            }
        }

        await localStorageService.SetItemAsync(EventiLocalStore, eventiStore);
    }

    public async Task EseguiSync()
    {
        var EventoStore = await GetEventiStore();
        DateTime DataOraUltimoSyncServer = EventoStore.DataOraUltimoSyncServer;

        var ListaEventiDaSincronizzare = EventoStore.ListaEventi.Where(x => x.DataOraUltimaModifica > EventoStore.DataOraUltimoSyncServer);

        if (ListaEventiDaSincronizzare.Count() > 0)
        {
            (await httpClient.PutAsJsonAsync("api/todolist/updatefromclient", ListaEventiDaSincronizzare)).EnsureSuccessStatusCode();

            //A questo punto quelli cancellati non servono più
            EventoStore.ListaEventi.RemoveAll(x => x.EventoConcluso);
        }

        var json = await httpClient.GetFromJsonAsync<List<EventoModel>>($"api/todolist/getalltodoitems?since={DataOraUltimoSyncServer:o}");

        foreach (var itemjson in json)
        {

            var itemlocale = EventoStore.ListaEventi.Where(x => x.Id == itemjson.Id).FirstOrDefault();

            if (itemlocale == null)
            {
                if (itemjson.EventoConcluso)
                {
                }
                else
                {
                    EventoStore.ListaEventi.Add(itemjson);
                }
            }
            else
            {
                if (itemjson.EventoConcluso)
                {
                    EventoStore.ListaEventi.Remove(itemlocale);
                }
                else
                {
                    EventoStore.ListaEventi[EventoStore.ListaEventi.FindIndex(ind => ind.Id == itemjson.Id)] = itemjson;
                }
            }
        }

        if (json.Count() > 0)
        {
            EventoStore.DataOraUltimoSyncServer = json.Max(x => x.DataOraUltimaModifica);
        }

        await localStorageService.SetItemAsync<EventiStore>(EventiLocalStore, EventoStore);
    }

    public async Task<List<EventoModel>> GetListaEventi()
    {
        var eventiStore = await GetEventiStore();

        return eventiStore.ListaEventi.Where(x => x.EventoConcluso == false).OrderBy(x => x.NomeEvento).ToList();
    }

    public async Task<int> GetEventiDaSincronizzare()
    {
        var eventiStore = await GetEventiStore();

        return eventiStore.ListaEventi.Where(x => x.DataOraUltimaModifica > eventiStore.DataOraUltimoSyncServer).Count();
    }
}