namespace CardioTrack.Application.Comum;

/// <summary>
/// Indica que um recurso solicitado nao existe. Traduzida em HTTP 404 pela API.
/// </summary>
public sealed class ExcecaoDeNaoEncontrado : ExcecaoDeAplicacao
{
    public ExcecaoDeNaoEncontrado(string mensagem) : base(mensagem)
    {
    }
}
