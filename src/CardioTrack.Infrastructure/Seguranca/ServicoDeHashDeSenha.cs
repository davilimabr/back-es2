using CardioTrack.Application.Abstracoes.Seguranca;

namespace CardioTrack.Infrastructure.Seguranca;

/// <summary>
/// Implementacao de <see cref="IServicoDeHashDeSenha"/> usando BCrypt, que ja
/// incorpora o salt no proprio hash gerado.
/// </summary>
public class ServicoDeHashDeSenha : IServicoDeHashDeSenha
{
    public string GerarHash(string senha) =>
        BCrypt.Net.BCrypt.HashPassword(senha);

    public bool Verificar(string senha, string hash) =>
        BCrypt.Net.BCrypt.Verify(senha, hash);
}
