using CardioTrack.Application.Usuarios.Dtos;
using FluentValidation;

namespace CardioTrack.Application.Usuarios.Validacoes;

/// <summary>
/// Valida os dados de cadastro antes que cheguem ao dominio, oferecendo mensagens
/// claras por campo. As regras espelham as invariantes da entidade Usuario e
/// acrescentam a confirmacao de senha, que e exclusiva da aplicacao.
/// </summary>
public sealed class CadastrarUsuarioValidador : AbstractValidator<CadastrarUsuarioRequisicao>
{
    public const int TamanhoMinimoSenha = 8;

    public CadastrarUsuarioValidador()
    {
        RuleFor(r => r.Nome)
            .NotEmpty().WithMessage("O nome e obrigatorio.")
            .MaximumLength(100);

        RuleFor(r => r.Sobrenome)
            .NotEmpty().WithMessage("O sobrenome e obrigatorio.")
            .MaximumLength(100);

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("O e-mail e obrigatorio.")
            .EmailAddress().WithMessage("O e-mail informado e invalido.")
            .MaximumLength(256);

        RuleFor(r => r.Telefone)
            .NotEmpty().WithMessage("O telefone e obrigatorio.")
            .MaximumLength(30);

        RuleFor(r => r.Senha)
            .NotEmpty().WithMessage("A senha e obrigatoria.")
            .MinimumLength(TamanhoMinimoSenha)
            .WithMessage($"A senha deve ter ao menos {TamanhoMinimoSenha} caracteres.");

        RuleFor(r => r.ConfirmacaoSenha)
            .Equal(r => r.Senha)
            .WithMessage("A confirmacao de senha nao corresponde a senha.");

        RuleFor(r => r.DataNascimento)
            .Must(data => data < DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("A data de nascimento deve estar no passado.");

        RuleFor(r => r.Sexo)
            .IsInEnum().WithMessage("O sexo informado e invalido.");

        RuleFor(r => r.PaisResidencia)
            .NotEmpty().WithMessage("O pais de residencia e obrigatorio.")
            .MaximumLength(100);
    }
}
