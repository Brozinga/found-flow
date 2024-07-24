using FoundFlow.Application.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FoundFlow.Application.Common.Feature.Users.ResetPassword;

public class ResetPasswordRequest : IRequest<Result<ResetPasswordResponse>>
{
    /// <summary>
    /// O endereço de e-mail do usuário que deseja redefinir a senha.
    /// </summary>
    /// <example>luiz.antonio@email.com</example>
    [Required]
    public string Email { get; set; }
}
