using System;
using FluentValidation;
using FoundFlow.Shared.Messages;

namespace FoundFlow.Application.Common.Feature.Users.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.UsersValidationUserNameMessage)
            .MinimumLength(3)
            .WithMessage(ErrorMessages.UsersValidationMinLengthUserNameMessage);

        When(x => !string.IsNullOrEmpty(x.Password), () =>
        {
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .WithMessage(ErrorMessages.UsersValidationMinLengthPasswordMessage)
                .Must(PasswordExtensions.ValidatePasswordComplexity)
                .WithMessage(ErrorMessages.UsersValidationPasswordComplexityMessage);
        });

        When(x => !string.IsNullOrEmpty(x.ConfirmPassword), () =>
        {
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage(ErrorMessages.UsersValidationConfirmPasswordMessage);
        });

        RuleFor(x => x.Notification)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.UsersValidationNotificationMessage);
    }
}