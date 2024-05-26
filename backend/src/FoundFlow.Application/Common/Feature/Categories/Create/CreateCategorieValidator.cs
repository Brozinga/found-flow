using System;
using FluentValidation;
using FoundFlow.Shared.Messages;

namespace FoundFlow.Application.Common.Feature.Categories.Create;

public class CreateCategorieValidator : AbstractValidator<CreateCategorieRequest>
{
    public CreateCategorieValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.CategoriesValidationRequiredNameMessage)
            .MinimumLength(3)
            .WithMessage(ErrorMessages.CategoriesValidationNameMinimumLengthMessage);

        RuleFor(x => x.Color)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.CategoriesValidationColorRequiredMessage)
            .Matches("^#([A-Fa-f0-9]{6})$")
            .WithMessage(ErrorMessages.CategoriesValidationColorFormatMessage);
    }
}