using CardioTrack.Application.Medicoes.Dtos;

namespace CardioTrack.Application.Relatorios.Dtos;

/// <summary>
/// Historico de medicoes de um usuario, da mais recente para a mais antiga,
/// acompanhado do total de itens no periodo consultado.
/// </summary>
public sealed record HistoricoMedicoesResposta(
    int Total,
    IReadOnlyList<MedicaoResposta> Medicoes);
