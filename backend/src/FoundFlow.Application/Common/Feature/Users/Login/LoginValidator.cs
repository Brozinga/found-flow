using System;
using FluentValidation;
using FoundFlow.Shared.Messages;

namespace FoundFlow.Application.Common.Feature.Users.Login;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage(ErrorMessages.UsersValidationEmailMessage);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.UsersValidationPasswordMessage)
            .MinimumLength(6)
            .WithMessage(ErrorMessages.UsersValidationMinLengthPasswordMessage)
            .Must(PasswordExtensions.ValidatePasswordComplexity)
            .WithMessage(ErrorMessages.UsersValidationPasswordComplexityMessage);
    }
}