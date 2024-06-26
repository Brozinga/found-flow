﻿using System;
using FluentValidation;
using FoundFlow.Shared.Messages;
using FoundFlow.Shared.Validations;

namespace FoundFlow.Application.Common.Feature.Categories.Delete;

public class DeleteCategorieValidator : AbstractValidator<DeleteCategorieRequest>
{
    public DeleteCategorieValidator()
    {
        RuleFor(x => x.Id.ToString())
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.CategoriesValidationIdCategorieRequireMessage)
            .MustBeGuid()
            .WithMessage(ErrorMessages.CategoriesValidationIdCategorieInvalidMessage);
    }
}