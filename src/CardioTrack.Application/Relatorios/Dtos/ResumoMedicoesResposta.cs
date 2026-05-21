namespace CardioTrack.Application.Relatorios.Dtos;

/// <summary>
/// Dados agregados das medicoes de um usuario no periodo, prontos para alimentar
/// os graficos de resumo do front-end. As estatisticas por metrica sao nulas
/// quando nao ha medicoes no periodo.
/// </summary>
public sealed record ResumoMedicoesResposta(
    int TotalMedicoes,
    DateTime? PrimeiraMedicaoEm,
    DateTime? UltimaMedicaoEm,
    EstatisticaResposta? PressaoSistolica,
    EstatisticaResposta? PressaoDiastolica,
    EstatisticaResposta? FrequenciaCardiaca,
    EstatisticaResposta? OxigenacaoSangue,
    EstatisticaResposta? PesoCorporal,
    FrequenciaSintomasResposta Sintomas);
