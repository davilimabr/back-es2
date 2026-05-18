namespace CardioTrack.Application.Abstracoes.Seguranca;

/// <summary>
/// Resultado da geracao de um token JWT: o token em si e o momento de expiracao.
/// </summary>
public sealed record TokenGerado(string Token, DateTime ExpiraEm);
