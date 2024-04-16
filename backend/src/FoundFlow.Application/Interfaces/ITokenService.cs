using System;
using FoundFlow.Domain.Entities;

namespace FoundFlow.Application.Interfaces;

public interface ITokenService
{
    public (string Token, DateTime? Expires) Generate(Users user);
}