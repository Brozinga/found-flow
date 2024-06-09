using FluentValidation;
using FoundFlow.Shared.Messages;

namespace FoundFlow.Application.Common.Feature.Users.ResetPassword;
public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty()
        .NotNull()
        .EmailAddress()
        .WithMessage(ErrorMessages.UsersValidationEmailMessage);
    }
}
