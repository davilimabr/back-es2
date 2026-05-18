using CardioTrack.Domain.Usuarios;

namespace CardioTrack.Application.Abstracoes.Persistencia;

/// <summary>
/// Contrato de acesso a persistencia de usuarios. A implementacao concreta vive
/// na camada de infraestrutura, mantendo a aplicacao independente do EF Core.
/// </summary>
public interface IRepositorioUsuario
{
    Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken = default);

    Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> EmailJaCadastradoAsync(string email, CancellationToken cancellationToken = default);
}
