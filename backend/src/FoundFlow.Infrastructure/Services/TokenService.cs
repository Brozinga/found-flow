using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoundFlow.Infrastructure.Services;

/// <summary>
/// Serviço responsável por gerar e validar tokens JWT (JSON Web Tokens).
/// </summary>
public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    /// <summary>
    /// Cria uma nova instância de `TokenService`.
    /// </summary>
    /// <param name="jwtSettings">As configurações para a geração e validação de tokens JWT.</param>
    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        ArgumentNullException.ThrowIfNull(jwtSettings);
        _jwtSettings = jwtSettings.Value;
    }

    /// <summary>
    /// Gera um novo token JWT para o usuário especificado.
    /// </summary>
    /// <param name="user">O usuário para o qual o token será gerado.</param>
    /// <returns>Uma tupla contendo o token JWT gerado e sua data de expiração.</returns>
    public (string Token, DateTime? Expires) Generate(Users user)
    {
        JwtSecurityTokenHandler handler = new();
        byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        DateTime? expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Issuer = _jwtSettings.ValidIssuer,
            Audience = _jwtSettings.ValidAudience,
            Expires = expires,
            SigningCredentials = credentials,
        };

        var token = handler.CreateToken(tokenDescriptor);
        return (handler.WriteToken(token), expires);
    }

    /// <summary>
    /// Lê as informações (nome de usuário, e-mail e ID do usuário) contidas em um token JWT.
    /// </summary>
    /// <param name="token">O token JWT a ser lido.</param>
    /// <returns>Uma tupla contendo o nome de usuário, o e-mail e o ID do usuário extraídos do token.</returns>
    public (string UserName, string Email, Guid UserId) Read(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        tokenHandler.ValidateToken(
            token,
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            },
            out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        string userName = jwtToken.Claims.First(x => x.Type == "unique_name").Value;
        string email = jwtToken.Claims.First(x => x.Type == "email").Value;
        var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);

        return (userName, email, userId);
    }

    /// <summary>
    /// Gera as claims (declarações) a serem incluídas no token JWT.
    /// </summary>
    /// <param name="user">O usuário para o qual as claims serão geradas.</param>
    /// <returns>Um objeto `ClaimsIdentity` contendo as claims do usuário.</returns>
    private ClaimsIdentity GenerateClaims(Users user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, _jwtSettings.JwtRegisteredClaimNamesSub));
        ci.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        ci.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

        return ci;
    }
}