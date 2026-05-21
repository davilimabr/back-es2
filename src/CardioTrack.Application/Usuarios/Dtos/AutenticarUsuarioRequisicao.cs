namespace CardioTrack.Application.Usuarios.Dtos;

/// <summary>
/// Credenciais informadas no login por e-mail e senha.
/// </summary>
public sealed record AutenticarUsuarioRequisicao(string Email, string Senha);
