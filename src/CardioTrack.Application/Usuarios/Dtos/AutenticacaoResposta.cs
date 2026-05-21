namespace CardioTrack.Application.Usuarios.Dtos;

/// <summary>
/// Resultado de um login bem-sucedido: o token JWT, seu vencimento e os dados do
/// usuario autenticado.
/// </summary>
public sealed record AutenticacaoResposta(
    string Token,
    DateTime ExpiraEm,
    UsuarioResposta Usuario);
