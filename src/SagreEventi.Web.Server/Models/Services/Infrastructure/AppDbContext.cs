using Microsoft.EntityFrameworkCore;
using SagreEventi.Shared.Models;

namespace SagreEventi.Web.Server.Models.Services.Infrastructure;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<EventoModel> Eventi { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EventoModel>(entity =>
        {
            entity.ToTable("Eventi");
            entity.HasKey(evento => evento.Id);
        });
    }
}
