using FoundFlow.Application.Models;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.Login;

public class LoginRequest : IRequest<Result<LoginResponse>>
{
    /// <summary>
    /// E-mail.
    /// </summary>
    /// <example>luiz.antonio@email.com</example>
    public string Email { get; set; }

    /// <summary>
    /// Senha.
    /// </summary>
    /// <example>BomD1@</example>
    public string Password { get; set; }
}