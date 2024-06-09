using FluentValidation;
using FoundFlow.Shared.Messages;
using FoundFlow.Shared.Validations;

namespace FoundFlow.Application.Common.Feature.Transactions.Create;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.Title)
        .NotEmpty()
        .NotNull()
        .WithMessage(ErrorMessages.TransactionsValidationTitleRequireMessage);

        RuleFor(x => x.Amount)
        .GreaterThan(0)
        .WithMessage(ErrorMessages.TransactionsValidationAmountRequireMessage);

        RuleFor(x => x.CategorieId.ToString())
        .NotEmpty()
        .NotNull()
        .WithMessage(ErrorMessages.CategoriesValidationIdCategorieRequireMessage)
        .MustBeGuid()
        .WithMessage(ErrorMessages.CategoriesValidationIdCategorieInvalidMessage);

        RuleFor(x => x.PaymentDate.ToString("yyyy-MM-ddTHH:mm:ss"))
        .NotNull()
        .WithMessage(ErrorMessages.TransactionValidationPaymentDateRequireMessage)
        .NotEqual("0001-01-01T00:00:00")
        .WithMessage(ErrorMessages.TransactionValidationPaymentDateFormatMessage);
    }
}