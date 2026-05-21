using CardioTrack.Application.Abstracoes.Persistencia;
using CardioTrack.Application.Abstracoes.Seguranca;
using CardioTrack.Application.Comum;
using CardioTrack.Application.Usuarios.Dtos;
using CardioTrack.Domain.Usuarios;
using FluentValidation;

namespace CardioTrack.Application.Usuarios.Servicos;

/// <summary>
/// Orquestra os casos de uso de conta de usuario, coordenando validacao, hashing
/// de senha, persistencia e geracao de token. Nao contem regras de negocio puras:
/// estas vivem na entidade <see cref="Usuario"/>.
/// </summary>
public sealed class ServicoUsuario : IServicoUsuario
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IServicoDeHashDeSenha _hashDeSenha;
    private readonly IGeradorDeToken _geradorDeToken;
    private readonly IValidator<CadastrarUsuarioRequisicao> _validadorCadastro;
    private readonly IValidator<AutenticarUsuarioRequisicao> _validadorAutenticacao;

    public ServicoUsuario(
        IRepositorioUsuario repositorio,
        IUnidadeDeTrabalho unidadeDeTrabalho,
        IServicoDeHashDeSenha hashDeSenha,
        IGeradorDeToken geradorDeToken,
        IValidator<CadastrarUsuarioRequisicao> validadorCadastro,
        IValidator<AutenticarUsuarioRequisicao> validadorAutenticacao)
    {
        _repositorio = repositorio;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _hashDeSenha = hashDeSenha;
        _geradorDeToken = geradorDeToken;
        _validadorCadastro = validadorCadastro;
        _validadorAutenticacao = validadorAutenticacao;
    }

    public async Task<UsuarioResposta> CadastrarAsync(
        CadastrarUsuarioRequisicao requisicao,
        CancellationToken cancellationToken = default)
    {
        await _validadorCadastro.ValidarOuLancarAsync(requisicao, cancellationToken);

        if (await _repositorio.EmailJaCadastradoAsync(requisicao.Email, cancellationToken))
        {
            throw new ExcecaoDeConflito("Ja existe uma conta cadastrada com este e-mail.");
        }

        var senhaHash = _hashDeSenha.GerarHash(requisicao.Senha);

        var usuario = new Usuario(
            requisicao.Nome,
            requisicao.Sobrenome,
            requisicao.Email,
            requisicao.Telefone,
            senhaHash,
            requisicao.DataNascimento,
            requisicao.Sexo,
            requisicao.PaisResidencia);

        await _repositorio.AdicionarAsync(usuario, cancellationToken);
        await _unidadeDeTrabalho.SalvarAlteracoesAsync(cancellationToken);

        return UsuarioResposta.DeDominio(usuario);
    }

    public async Task<AutenticacaoResposta> AutenticarAsync(
        AutenticarUsuarioRequisicao requisicao,
        CancellationToken cancellationToken = default)
    {
        await _validadorAutenticacao.ValidarOuLancarAsync(requisicao, cancellationToken);

        var usuario = await _repositorio.ObterPorEmailAsync(requisicao.Email, cancellationToken);
        if (usuario is null || !_hashDeSenha.Verificar(requisicao.Senha, usuario.SenhaHash))
        {
            throw new ExcecaoDeCredenciaisInvalidas();
        }

        var token = _geradorDeToken.Gerar(usuario);

        return new AutenticacaoResposta(
            token.Token,
            token.ExpiraEm,
            UsuarioResposta.DeDominio(usuario));
    }
}
