using FluentValidation;

namespace CardioTrack.Application.Comum;

/// <summary>
/// Atalhos para validar uma requisicao com FluentValidation e converter o
/// resultado em <see cref="ExcecaoDeValidacao"/>, evitando repetir esse fluxo em
/// cada caso de uso.
/// </summary>
public static class ValidadorExtensoes
{
    public static async Task ValidarOuLancarAsync<T>(
        this IValidator<T> validador,
        T instancia,
        CancellationToken cancellationToken = default)
    {
        var resultado = await validador.ValidateAsync(instancia, cancellationToken);
        if (!resultado.IsValid)
        {
            throw new ExcecaoDeValidacao(resultado.Errors);
        }
    }
}
