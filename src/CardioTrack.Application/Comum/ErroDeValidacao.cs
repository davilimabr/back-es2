namespace CardioTrack.Application.Comum;

/// <summary>
/// Detalha uma unica falha de validacao: o campo rejeitado, o valor que o cliente
/// informou e a mensagem que explica o motivo. Serve de item para a resposta de
/// erro de validacao, deixando claro o que corrigir.
/// </summary>
public sealed record ErroDeValidacao(string Campo, object? ValorInformado, string Mensagem);
