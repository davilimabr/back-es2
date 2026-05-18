using CardioTrack.Application.Abstracoes.Persistencia;

namespace CardioTrack.Infrastructure.Persistencia;

/// <summary>
/// Implementacao de <see cref="IUnidadeDeTrabalho"/> sobre o
/// <see cref="CardioTrackDbContext"/>: delega a confirmacao das alteracoes ao
/// SaveChanges do EF Core.
/// </summary>
public class UnidadeDeTrabalho : IUnidadeDeTrabalho
{
    private readonly CardioTrackDbContext _contexto;

    public UnidadeDeTrabalho(CardioTrackDbContext contexto)
    {
        _contexto = contexto;
    }

    public Task<int> SalvarAlteracoesAsync(CancellationToken cancellationToken = default) =>
        _contexto.SaveChangesAsync(cancellationToken);
}
