namespace CardioTrack.Application.Comum;

/// <summary>
/// Indica que a operacao conflita com o estado atual do sistema, como tentar
/// cadastrar um e-mail ja existente. Traduzida em HTTP 409 pela API.
/// </summary>
public sealed class ExcecaoDeConflito : ExcecaoDeAplicacao
{
    public ExcecaoDeConflito(string mensagem) : base(mensagem)
    {
    }
}
