using System;
using FluentValidation;
using FoundFlow.Shared.Messages;

namespace FoundFlow.Application.Common.Feature.Users.Create;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.UsersValidationUserNameMessage)
            .MinimumLength(3)
            .WithMessage(ErrorMessages.UsersValidationMinLengthUserNameMessage);

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

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage(ErrorMessages.UsersValidationConfirmPasswordMessage);

        RuleFor(x => x.Notification)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.UsersValidationNotificationMessage);
    }
}