namespace CardioTrack.Application.Comum;

/// <summary>
/// Base das excecoes geradas pelos casos de uso da aplicacao. A camada de API
/// traduz cada tipo no codigo HTTP adequado, mantendo os servicos agnosticos ao
/// protocolo.
/// </summary>
public abstract class ExcecaoDeAplicacao : Exception
{
    protected ExcecaoDeAplicacao(string mensagem) : base(mensagem)
    {
    }
}
