using CardioTrack.Domain.Usuarios;

namespace CardioTrack.Application.Usuarios.Dtos;

/// <summary>
/// Representacao publica de um usuario, sem expor a senha. Usada nas respostas de
/// cadastro e autenticacao.
/// </summary>
public sealed record UsuarioResposta(
    Guid Id,
    string Nome,
    string Sobrenome,
    string NomeCompleto,
    string Email,
    string Telefone,
    DateOnly DataNascimento,
    Sexo Sexo,
    string PaisResidencia,
    DateTime CriadoEm)
{
    public static UsuarioResposta DeDominio(Usuario usuario) => new(
        usuario.Id,
        usuario.Nome,
        usuario.Sobrenome,
        usuario.NomeCompleto,
        usuario.Email,
        usuario.Telefone,
        usuario.DataNascimento,
        usuario.Sexo,
        usuario.PaisResidencia,
        usuario.CriadoEm);
}
