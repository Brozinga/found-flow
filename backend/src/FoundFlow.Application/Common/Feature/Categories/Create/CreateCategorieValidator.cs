using System;
using FluentValidation;
using FoundFlow.Shared.Messages;

namespace FoundFlow.Application.Common.Feature.Categories.Create;

public class CreateCategorieValidator : AbstractValidator<CreateCategorieRequest>
{
    public CreateCategorieValidator()
    {
        ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member.Name;

        RuleFor(x => x.CategorieName)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.CategoriesCreateValidationRequiredNameMessage)
            .MinimumLength(3)
            .WithMessage(ErrorMessages.CategoriesCreateValidationNameMinimumLengthMessage);

        RuleFor(x => x.Color)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.CategoriesCreateValidationColorRequiredMessage)
            .Matches("^#([A-Fa-f0-9]{6})$")
            .WithMessage(ErrorMessages.CategoriesCreateValidationColorFormatMessage);
    }
}