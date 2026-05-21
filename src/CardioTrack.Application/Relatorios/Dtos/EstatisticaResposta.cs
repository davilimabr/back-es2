namespace CardioTrack.Application.Relatorios.Dtos;

/// <summary>
/// Resumo estatistico de uma metrica no periodo: media, minimo e maximo. A media
/// vem arredondada para duas casas, adequada a exibicao direta no front-end.
/// </summary>
public sealed record EstatisticaResposta(decimal Media, decimal Minimo, decimal Maximo);
