﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FoundFlow.Application.Interfaces;
using FoundFlow.Domain.Entities;
using FoundFlow.Shared.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoundFlow.Application.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        ArgumentNullException.ThrowIfNull(jwtSettings);
        _jwtSettings = jwtSettings.Value;
    }

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