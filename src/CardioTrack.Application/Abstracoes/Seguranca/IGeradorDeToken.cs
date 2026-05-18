using CardioTrack.Domain.Usuarios;

namespace CardioTrack.Application.Abstracoes.Seguranca;

/// <summary>
/// Gera o token JWT de autenticacao a partir de um usuario ja autenticado.
/// </summary>
public interface IGeradorDeToken
{
    TokenGerado Gerar(Usuario usuario);
}
