using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SagreEventi.Shared.Models;
using SagreEventi.Web.Server.Models.Services.Infrastructure;

namespace SagreEventi.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]

public class EventiController : ControllerBase
{
    private readonly ILogger<EventiController> logger;
    private readonly AppDbContext appDbContext;

    public EventiController(ILogger<EventiController> logger, AppDbContext appDbContext)
    {
        this.logger = logger;
        this.appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<List<EventoModel>> GetEventi([FromQuery] DateTime since)
    {
        return await appDbContext.Eventi.Where(x => x.DataOraUltimaModifica > since).ToListAsync();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEventi(List<EventoModel> eventi)
    {
        foreach (var todoitem in eventi)
        {
            var listaEventi = await appDbContext.Eventi.Where(x => x.Id == todoitem.Id).FirstOrDefaultAsync();
            if (listaEventi == null)
            {

                if (!todoitem.EventoConcluso)
                {
                    appDbContext.Eventi.Add(todoitem);

                }
            }
            else
            {
                if (listaEventi.DataOraUltimaModifica < todoitem.DataOraUltimaModifica)
                {
                    appDbContext.Entry(listaEventi).CurrentValues.SetValues(todoitem);
                }
            }
        }

        await appDbContext.SaveChangesAsync();
        return Ok();
    }
}