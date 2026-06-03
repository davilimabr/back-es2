using FluentValidation.Results;

namespace CardioTrack.Application.Comum;

/// <summary>
/// Reune as falhas de validacao de uma requisicao. Traduzida em HTTP 400 pela API,
/// alimentando uma resposta que indica, para cada campo, o valor informado e a
/// mensagem do erro. Valores de campos sensiveis (senha) sao omitidos para nao
/// vazarem na resposta.
/// </summary>
public sealed class ExcecaoDeValidacao : ExcecaoDeAplicacao
{
    private const string ValorOmitido = "***";

    public ExcecaoDeValidacao(IEnumerable<ValidationFailure> falhas)
        : base("Um ou mais campos sao invalidos.")
    {
        Erros = falhas
            .Select(f => new ErroDeValidacao(
                f.PropertyName,
                EhCampoSensivel(f.PropertyName) ? ValorOmitido : f.AttemptedValue,
                f.ErrorMessage))
            .ToArray();
    }

    /// <summary>Uma entrada por falha, com campo, valor informado e mensagem.</summary>
    public IReadOnlyList<ErroDeValidacao> Erros { get; }

    private static bool EhCampoSensivel(string campo) =>
        campo.Contains("senha", StringComparison.OrdinalIgnoreCase);
}
