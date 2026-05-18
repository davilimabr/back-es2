namespace CardioTrack.Application.Abstracoes.Persistencia;

/// <summary>
/// Coordena a persistencia das alteracoes feitas pelos repositorios em uma
/// unica transacao logica, isolando a camada de aplicacao do EF Core.
/// </summary>
public interface IUnidadeDeTrabalho
{
    /// <summary>Confirma no banco as alteracoes pendentes.</summary>
    Task<int> SalvarAlteracoesAsync(CancellationToken cancellationToken = default);
}
