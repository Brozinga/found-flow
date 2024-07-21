using FoundFlow.Application.Models;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.ResetPassword;

/// <summary>
/// Representa uma solicitação para redefinir a senha de um usuário.
/// </summary>
public class ResetPasswordRequest : IRequest<Result<ResetPasswordResponse>>
{
    /// <summary>
    /// O endereço de e-mail do usuário que deseja redefinir a senha.
    /// </summary>
    /// <example>luiz.antonio@email.com</example>
    public string Email { get; set; }
}
