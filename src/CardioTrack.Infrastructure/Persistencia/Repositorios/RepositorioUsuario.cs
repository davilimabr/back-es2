using CardioTrack.Application.Abstracoes.Persistencia;
using CardioTrack.Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace CardioTrack.Infrastructure.Persistencia.Repositorios;

/// <summary>
/// Repositorio de usuarios sobre o EF Core. A confirmacao das alteracoes fica a
/// cargo da <see cref="IUnidadeDeTrabalho"/>, nao do repositorio.
/// </summary>
public class RepositorioUsuario : IRepositorioUsuario
{
    private readonly CardioTrackDbContext _contexto;

    public RepositorioUsuario(CardioTrackDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken = default) =>
        await _contexto.Usuarios.AddAsync(usuario, cancellationToken);

    public Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalizado = email.Trim().ToLowerInvariant();
        return _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == normalizado, cancellationToken);
    }

    public Task<bool> EmailJaCadastradoAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalizado = email.Trim().ToLowerInvariant();
        return _contexto.Usuarios.AnyAsync(u => u.Email == normalizado, cancellationToken);
    }
}
