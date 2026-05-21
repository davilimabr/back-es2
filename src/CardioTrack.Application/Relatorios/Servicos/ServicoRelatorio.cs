using CardioTrack.Application.Abstracoes.Persistencia;
using CardioTrack.Application.Medicoes.Dtos;
using CardioTrack.Application.Relatorios.Dtos;
using CardioTrack.Domain.Medicoes;

namespace CardioTrack.Application.Relatorios.Servicos;

/// <summary>
/// Monta os relatorios a partir das medicoes persistidas. O historico apenas
/// projeta os registros; o resumo calcula em memoria as estatisticas e a
/// frequencia de sintomas usadas nos graficos.
/// </summary>
public sealed class ServicoRelatorio : IServicoRelatorio
{
    private readonly IRepositorioMedicao _repositorio;

    public ServicoRelatorio(IRepositorioMedicao repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<HistoricoMedicoesResposta> ObterHistoricoAsync(
        Guid usuarioId,
        DateTime? inicio = null,
        DateTime? fim = null,
        CancellationToken cancellationToken = default)
    {
        var medicoes = await _repositorio.ListarPorUsuarioAsync(usuarioId, inicio, fim, cancellationToken);
        var respostas = medicoes.Select(MedicaoResposta.DeDominio).ToList();
        return new HistoricoMedicoesResposta(respostas.Count, respostas);
    }

    public async Task<ResumoMedicoesResposta> ObterResumoAsync(
        Guid usuarioId,
        DateTime? inicio = null,
        DateTime? fim = null,
        CancellationToken cancellationToken = default)
    {
        var medicoes = await _repositorio.ListarPorUsuarioAsync(usuarioId, inicio, fim, cancellationToken);

        if (medicoes.Count == 0)
        {
            return new ResumoMedicoesResposta(
                TotalMedicoes: 0,
                PrimeiraMedicaoEm: null,
                UltimaMedicaoEm: null,
                PressaoSistolica: null,
                PressaoDiastolica: null,
                FrequenciaCardiaca: null,
                OxigenacaoSangue: null,
                PesoCorporal: null,
                Sintomas: new FrequenciaSintomasResposta(0, 0, 0, 0, 0));
        }

        return new ResumoMedicoesResposta(
            TotalMedicoes: medicoes.Count,
            PrimeiraMedicaoEm: medicoes.Min(m => m.RegistradaEm),
            UltimaMedicaoEm: medicoes.Max(m => m.RegistradaEm),
            PressaoSistolica: Estatistica(medicoes, m => m.PressaoSistolica),
            PressaoDiastolica: Estatistica(medicoes, m => m.PressaoDiastolica),
            FrequenciaCardiaca: Estatistica(medicoes, m => m.FrequenciaCardiaca),
            OxigenacaoSangue: Estatistica(medicoes, m => m.OxigenacaoSangue),
            PesoCorporal: Estatistica(medicoes, m => m.PesoCorporal),
            Sintomas: ContarSintomas(medicoes));
    }

    private static EstatisticaResposta Estatistica(
        IReadOnlyList<Medicao> medicoes,
        Func<Medicao, decimal> seletor)
    {
        var valores = medicoes.Select(seletor).ToList();
        return new EstatisticaResposta(
            Media: Math.Round(valores.Average(), 2, MidpointRounding.AwayFromZero),
            Minimo: valores.Min(),
            Maximo: valores.Max());
    }

    private static FrequenciaSintomasResposta ContarSintomas(IReadOnlyList<Medicao> medicoes) => new(
        FaltaDeAr: medicoes.Count(m => m.Sintomas.HasFlag(Sintoma.FaltaDeAr)),
        DorNoPeito: medicoes.Count(m => m.Sintomas.HasFlag(Sintoma.DorNoPeito)),
        Tontura: medicoes.Count(m => m.Sintomas.HasFlag(Sintoma.Tontura)),
        ComAlgumSintoma: medicoes.Count(m => m.PossuiSintomas),
        SemSintomas: medicoes.Count(m => !m.PossuiSintomas));
}
