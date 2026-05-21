using CardioTrack.Application.Relatorios.Dtos;

namespace CardioTrack.Application.Relatorios.Servicos;

/// <summary>
/// Casos de uso de relatorios: historico das medicoes e dados agregados para os
/// graficos do front-end. O periodo e opcional; quando ausente, considera todo o
/// historico.
/// </summary>
public interface IServicoRelatorio
{
    Task<HistoricoMedicoesResposta> ObterHistoricoAsync(
        Guid usuarioId,
        DateTime? inicio = null,
        DateTime? fim = null,
        CancellationToken cancellationToken = default);

    Task<ResumoMedicoesResposta> ObterResumoAsync(
        Guid usuarioId,
        DateTime? inicio = null,
        DateTime? fim = null,
        CancellationToken cancellationToken = default);
}
