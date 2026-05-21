namespace CardioTrack.Application.Comum;

/// <summary>
/// Indica falha na autenticacao por e-mail ou senha incorretos. A mensagem e
/// deliberadamente generica para nao revelar se o e-mail existe. Traduzida em
/// HTTP 401 pela API.
/// </summary>
public sealed class ExcecaoDeCredenciaisInvalidas : ExcecaoDeAplicacao
{
    public ExcecaoDeCredenciaisInvalidas()
        : base("E-mail ou senha invalidos.")
    {
    }
}
