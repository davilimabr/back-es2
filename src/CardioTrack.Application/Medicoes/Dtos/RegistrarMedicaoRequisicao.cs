namespace CardioTrack.Application.Medicoes.Dtos;

/// <summary>
/// Dados de uma nova medicao de saude cardiaca. Os sintomas chegam como booleanos
/// independentes, mais convenientes ao front-end, e sao combinados na flag
/// <c>Sintoma</c> do dominio pelo servico.
/// </summary>
public sealed record RegistrarMedicaoRequisicao(
    int PressaoSistolica,
    int PressaoDiastolica,
    int FrequenciaCardiaca,
    int OxigenacaoSangue,
    decimal PesoCorporal,
    bool FaltaDeAr,
    bool DorNoPeito,
    bool Tontura,
    DateTime? RegistradaEm = null);
