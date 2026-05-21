using CardioTrack.Application.Medicoes.Dtos;

namespace CardioTrack.Application.Medicoes.Servicos;

/// <summary>
/// Casos de uso de registro de medicoes de saude cardiaca.
/// </summary>
public interface IServicoMedicao
{
    /// <summary>
    /// Registra uma medicao para o usuario informado. Lanca
    /// <c>ExcecaoDeValidacao</c> para dados invalidos e
    /// <c>ExcecaoDeNaoEncontrado</c> quando o usuario nao existe.
    /// </summary>
    Task<MedicaoResposta> RegistrarAsync(
        Guid usuarioId,
        RegistrarMedicaoRequisicao requisicao,
        CancellationToken cancellationToken = default);
}
