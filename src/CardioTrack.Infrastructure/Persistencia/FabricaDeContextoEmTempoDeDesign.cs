using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CardioTrack.Infrastructure.Persistencia;

public class FabricaDeContextoEmTempoDeDesign : IDesignTimeDbContextFactory<CardioTrackDbContext>
{
    public CardioTrackDbContext CreateDbContext(string[] args)
    {
        var configuracao = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var stringConexao = configuracao.GetConnectionString("CARDIOTRACK_CONNECTION");

        var opcoes = new DbContextOptionsBuilder<CardioTrackDbContext>()
            .UseMySql(stringConexao, new MySqlServerVersion(new Version(8, 0, 0)))
            .Options;

        return new CardioTrackDbContext(opcoes);
    }
}
