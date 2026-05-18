using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CardioTrack.Application.Abstracoes.Seguranca;
using CardioTrack.Domain.Usuarios;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CardioTrack.Infrastructure.Seguranca;

/// <summary>
/// Gera tokens JWT assinados com HMAC-SHA256 a partir das opcoes configuradas,
/// embutindo as claims de identificacao do usuario.
/// </summary>
public class GeradorDeToken : IGeradorDeToken
{
    private readonly OpcoesJwt _opcoes;

    public GeradorDeToken(IOptions<OpcoesJwt> opcoes)
    {
        _opcoes = opcoes.Value;
    }

    public TokenGerado Gerar(Usuario usuario)
    {
        var agora = DateTime.UtcNow;
        var expiraEm = agora.AddMinutes(_opcoes.ExpirationMinutes);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Name, usuario.NomeCompleto),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opcoes.SecretKey));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _opcoes.Issuer,
            audience: _opcoes.Audience,
            claims: claims,
            notBefore: agora,
            expires: expiraEm,
            signingCredentials: credenciais);

        var tokenSerializado = new JwtSecurityTokenHandler().WriteToken(token);
        return new TokenGerado(tokenSerializado, expiraEm);
    }
}
