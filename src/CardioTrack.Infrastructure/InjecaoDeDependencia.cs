using CardioTrack.Application.Abstracoes.Persistencia;
using CardioTrack.Application.Abstracoes.Seguranca;
using CardioTrack.Infrastructure.Persistencia;
using CardioTrack.Infrastructure.Persistencia.Repositorios;
using CardioTrack.Infrastructure.Seguranca;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CardioTrack.Infrastructure;

/// <summary>
/// Registra os servicos da camada de infraestrutura (EF Core, repositorios,
/// unidade de trabalho, hashing e geracao de token) no container de injecao de
/// dependencia. A camada de API apenas chama este metodo.
/// </summary>
public static class InjecaoDeDependencia
{
    public static IServiceCollection AdicionarInfraestrutura(
        this IServiceCollection servicos,
        IConfiguration configuracao)
    {
        var stringConexao = configuracao.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "A connection string 'DefaultConnection' nao foi configurada.");

        servicos.AddDbContext<CardioTrackDbContext>(opcoes =>
            opcoes.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao)));

        servicos.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
        servicos.AddScoped<IRepositorioMedicao, RepositorioMedicao>();
        servicos.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();

        servicos.AddOptions<OpcoesJwt>()
            .Bind(configuracao.GetSection(OpcoesJwt.Secao))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        servicos.AddSingleton<IServicoDeHashDeSenha, ServicoDeHashDeSenha>();
        servicos.AddSingleton<IGeradorDeToken, GeradorDeToken>();

        return servicos;
    }
}
