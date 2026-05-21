using CardioTrack.Application.Abstracoes.Persistencia;
using CardioTrack.Application.Comum;
using CardioTrack.Application.Medicoes.Dtos;
using CardioTrack.Domain.Medicoes;
using FluentValidation;

namespace CardioTrack.Application.Medicoes.Servicos;

/// <summary>
/// Orquestra o registro de medicoes, garantindo que pertencam a um usuario
/// existente. As regras de faixa dos valores ficam na entidade <see cref="Medicao"/>.
/// </summary>
public sealed class ServicoMedicao : IServicoMedicao
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IRepositorioMedicao _repositorioMedicao;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IValidator<RegistrarMedicaoRequisicao> _validador;

    public ServicoMedicao(
        IRepositorioUsuario repositorioUsuario,
        IRepositorioMedicao repositorioMedicao,
        IUnidadeDeTrabalho unidadeDeTrabalho,
        IValidator<RegistrarMedicaoRequisicao> validador)
    {
        _repositorioUsuario = repositorioUsuario;
        _repositorioMedicao = repositorioMedicao;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _validador = validador;
    }

    public async Task<MedicaoResposta> RegistrarAsync(
        Guid usuarioId,
        RegistrarMedicaoRequisicao requisicao,
        CancellationToken cancellationToken = default)
    {
        await _validador.ValidarOuLancarAsync(requisicao, cancellationToken);

        var usuario = await _repositorioUsuario.ObterPorIdAsync(usuarioId, cancellationToken)
            ?? throw new ExcecaoDeNaoEncontrado("Usuario nao encontrado.");

        var sintomas = SintomasResposta.ParaFlags(
            requisicao.FaltaDeAr,
            requisicao.DorNoPeito,
            requisicao.Tontura);

        var medicao = usuario.RegistrarMedicao(
            requisicao.PressaoSistolica,
            requisicao.PressaoDiastolica,
            requisicao.FrequenciaCardiaca,
            requisicao.OxigenacaoSangue,
            requisicao.PesoCorporal,
            sintomas,
            requisicao.RegistradaEm);

        await _repositorioMedicao.AdicionarAsync(medicao, cancellationToken);
        await _unidadeDeTrabalho.SalvarAlteracoesAsync(cancellationToken);

        return MedicaoResposta.DeDominio(medicao);
    }
}
