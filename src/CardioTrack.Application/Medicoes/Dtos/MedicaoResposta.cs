using CardioTrack.Domain.Medicoes;

namespace CardioTrack.Application.Medicoes.Dtos;

/// <summary>
/// Representacao publica de uma medicao registrada, com os sintomas ja
/// decompostos em booleanos.
/// </summary>
public sealed record MedicaoResposta(
    Guid Id,
    Guid UsuarioId,
    int PressaoSistolica,
    int PressaoDiastolica,
    int FrequenciaCardiaca,
    int OxigenacaoSangue,
    decimal PesoCorporal,
    SintomasResposta Sintomas,
    bool PossuiSintomas,
    DateTime RegistradaEm,
    DateTime CriadaEm)
{
    public static MedicaoResposta DeDominio(Medicao medicao) => new(
        medicao.Id,
        medicao.UsuarioId,
        medicao.PressaoSistolica,
        medicao.PressaoDiastolica,
        medicao.FrequenciaCardiaca,
        medicao.OxigenacaoSangue,
        medicao.PesoCorporal,
        SintomasResposta.DeFlags(medicao.Sintomas),
        medicao.PossuiSintomas,
        medicao.RegistradaEm,
        medicao.CriadaEm);
}
