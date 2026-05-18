namespace CardioTrack.Application.Abstracoes.Seguranca;

/// <summary>
/// Encapsula a geracao e a verificacao de hash de senha, mantendo o algoritmo
/// (BCrypt) na camada de infraestrutura.
/// </summary>
public interface IServicoDeHashDeSenha
{
    string GerarHash(string senha);

    bool Verificar(string senha, string hash);
}
