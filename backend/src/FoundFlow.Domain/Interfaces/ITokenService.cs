using System;
using FoundFlow.Domain.Entities;

namespace FoundFlow.Domain.Interfaces;

public interface ITokenService
{
    public (string Token, DateTime? Expires) Generate(Users user);
    (string UserName, string Email, Guid UserId) Read(string token);
}