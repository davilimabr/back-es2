using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CardioTrack.Infrastructure.Persistencia;

/// <summary>
/// Fabrica usada apenas em tempo de design pelas ferramentas do EF Core (por
/// exemplo, ao gerar migrations), permitindo criar o contexto sem subir a API.
/// A connection string vem da variavel de ambiente CARDIOTRACK_CONNECTION ou de
/// um padrao coerente com o docker-compose de desenvolvimento.
/// </summary>
public class FabricaDeContextoEmTempoDeDesign : IDesignTimeDbContextFactory<CardioTrackDbContext>
{
    private const string StringConexaoPadrao =
        "Server=localhost;Port=3306;Database=cardiotrack;User=cardiotrack;Password=cardiotrack;";

    public CardioTrackDbContext CreateDbContext(string[] args)
    {
        var stringConexao = Environment.GetEnvironmentVariable("CARDIOTRACK_CONNECTION")
            ?? StringConexaoPadrao;

        var opcoes = new DbContextOptionsBuilder<CardioTrackDbContext>()
            .UseMySql(stringConexao, new MySqlServerVersion(new Version(8, 0, 0)))
            .Options;

        return new CardioTrackDbContext(opcoes);
    }
}
