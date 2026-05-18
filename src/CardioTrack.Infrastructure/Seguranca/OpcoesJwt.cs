using System.ComponentModel.DataAnnotations;

namespace CardioTrack.Infrastructure.Seguranca;

/// <summary>
/// Configuracoes do token JWT, vinculadas a secao "Jwt" do appsettings.
/// </summary>
public class OpcoesJwt
{
    public const string Secao = "Jwt";

    [Required]
    public string Issuer { get; set; } = string.Empty;

    [Required]
    public string Audience { get; set; } = string.Empty;

    /// <summary>Chave secreta usada para assinar o token (minimo de 32 bytes).</summary>
    [Required]
    [MinLength(32)]
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>Validade do token, em minutos.</summary>
    [Range(1, int.MaxValue)]
    public int ExpirationMinutes { get; set; } = 480;
}
