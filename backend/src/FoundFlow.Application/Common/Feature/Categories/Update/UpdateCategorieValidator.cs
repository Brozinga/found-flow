using System;
using FluentValidation;
using FoundFlow.Shared.Messages;
using FoundFlow.Shared.Validations;

namespace FoundFlow.Application.Common.Feature.Categories.Update;

public class UpdateCategorieValidator : AbstractValidator<UpdateCategorieRequest>
{
    public UpdateCategorieValidator()
    {
        ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member.Name;

        RuleFor(x => x.Id.ToString())
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.CategoriesValidationIdCategorieRequireMessage)
            .MustBeGuid()
            .WithMessage(ErrorMessages.CategoriesValidationIdCategorieInvalidMessage);

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