namespace CardioTrack.Application.Relatorios.Dtos;

/// <summary>
/// Contagem de ocorrencias de cada sintoma no periodo, util para graficos de
/// frequencia. <c>ComAlgumSintoma</c> e <c>SemSintomas</c> totalizam as medicoes.
/// </summary>
public sealed record FrequenciaSintomasResposta(
    int FaltaDeAr,
    int DorNoPeito,
    int Tontura,
    int ComAlgumSintoma,
    int SemSintomas);
