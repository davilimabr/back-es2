using FluentValidation.Results;

namespace CardioTrack.Application.Comum;

/// <summary>
/// Reune as falhas de validacao de uma requisicao, agrupadas por campo. Traduzida
/// em HTTP 400 pela API, alimentando uma resposta de erros amigavel ao cliente.
/// </summary>
public sealed class ExcecaoDeValidacao : ExcecaoDeAplicacao
{
    public ExcecaoDeValidacao(IEnumerable<ValidationFailure> falhas)
        : base("Um ou mais campos sao invalidos.")
    {
        Erros = falhas
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                grupo => grupo.Key,
                grupo => grupo.Select(f => f.ErrorMessage).Distinct().ToArray());
    }

    /// <summary>Mensagens de erro indexadas pelo nome do campo.</summary>
    public IReadOnlyDictionary<string, string[]> Erros { get; }
}
