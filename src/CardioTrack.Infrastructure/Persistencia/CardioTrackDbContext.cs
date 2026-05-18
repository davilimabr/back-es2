using System.Reflection;
using CardioTrack.Domain.Medicoes;
using CardioTrack.Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace CardioTrack.Infrastructure.Persistencia;

/// <summary>
/// Contexto do EF Core que mapeia as entidades do dominio para o MySQL. As
/// configuracoes de cada entidade ficam em classes separadas (uma por entidade),
/// reforcando a modularizacao tambem na persistencia.
/// </summary>
public class CardioTrackDbContext : DbContext
{
    public CardioTrackDbContext(DbContextOptions<CardioTrackDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    public DbSet<Medicao> Medicoes => Set<Medicao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
