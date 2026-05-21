using CardioTrack.Domain.Usuarios;

namespace CardioTrack.Application.Usuarios.Dtos;

/// <summary>
/// Dados informados no cadastro de uma nova conta. A confirmacao de senha e
/// validada na aplicacao e nunca chega ao dominio.
/// </summary>
public sealed record CadastrarUsuarioRequisicao(
    string Nome,
    string Sobrenome,
    string Email,
    string Telefone,
    string Senha,
    string ConfirmacaoSenha,
    DateOnly DataNascimento,
    Sexo Sexo,
    string PaisResidencia);
