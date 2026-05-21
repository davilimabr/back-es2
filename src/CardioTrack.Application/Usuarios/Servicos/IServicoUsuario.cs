using CardioTrack.Application.Usuarios.Dtos;

namespace CardioTrack.Application.Usuarios.Servicos;

/// <summary>
/// Casos de uso de conta de usuario: cadastro e autenticacao.
/// </summary>
public interface IServicoUsuario
{
    /// <summary>
    /// Cadastra uma nova conta. Lanca <c>ExcecaoDeValidacao</c> para dados
    /// invalidos e <c>ExcecaoDeConflito</c> quando o e-mail ja esta em uso.
    /// </summary>
    Task<UsuarioResposta> CadastrarAsync(
        CadastrarUsuarioRequisicao requisicao,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Autentica por e-mail e senha, retornando um token JWT. Lanca
    /// <c>ExcecaoDeCredenciaisInvalidas</c> quando as credenciais nao conferem.
    /// </summary>
    Task<AutenticacaoResposta> AutenticarAsync(
        AutenticarUsuarioRequisicao requisicao,
        CancellationToken cancellationToken = default);
}
