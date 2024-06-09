using FoundFlow.Application.Models;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.ResetPassword;
public class ResetPasswordRequest : IRequest<Result<ResetPasswordResponse>>
{
    /// <summary>
    /// E-mail.
    /// </summary>
    /// <example>luiz.antonio@email.com</example>
    public string Email { get; set; }
}
