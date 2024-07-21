using FoundFlow.Application.Models;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.Login;

/// <summary>
/// Representa uma solicitação de login de usuário.
/// </summary>
public class LoginRequest : IRequest<Result<LoginResponse>>
{
    /// <summary>
    /// O endereço de e-mail do usuário.
    /// </summary>
    /// <example>luiz.antonio@email.com</example>
    public string Email { get; set; }

    /// <summary>
    /// A senha do usuário.
    /// </summary>
    /// <example>BomD1@</example>
    public string Password { get; set; }
}