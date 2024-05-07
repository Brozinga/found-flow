using System;
using FluentValidation;
using FoundFlow.Shared.Messages;

namespace FoundFlow.Application.Common.Feature.Users.Create;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member.Name;

        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.UsersCreateValidationUserNameMessage)
            .MinimumLength(3)
            .WithMessage(ErrorMessages.UsersCreateValidationMinLengthUserNameMessage);

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage(ErrorMessages.UsersLoginValidationEmailMessage);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.UsersCreateValidationPasswordMessage)
            .MinimumLength(6)
            .WithMessage(ErrorMessages.UsersCreateValidationMinLengthPasswordMessage)
            .Must(PasswordExtensions.ValidatePasswordComplexity)
            .WithMessage(ErrorMessages.UsersCreateValidationPasswordComplexityMessage);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage(ErrorMessages.UsersCreateValidationConfirmPasswordMessage);

        RuleFor(x => x.Notification)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.UsersCreateValidationNotificationMessage);
    }
}