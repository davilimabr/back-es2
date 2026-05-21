using CardioTrack.Application.Usuarios.Dtos;
using FluentValidation;

namespace CardioTrack.Application.Usuarios.Validacoes;

/// <summary>
/// Valida o formato das credenciais de login. A conferencia de senha em si fica a
/// cargo do servico, que nao revela qual campo falhou.
/// </summary>
public sealed class AutenticarUsuarioValidador : AbstractValidator<AutenticarUsuarioRequisicao>
{
    public AutenticarUsuarioValidador()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("O e-mail e obrigatorio.")
            .EmailAddress().WithMessage("O e-mail informado e invalido.");

        RuleFor(r => r.Senha)
            .NotEmpty().WithMessage("A senha e obrigatoria.");
    }
}
