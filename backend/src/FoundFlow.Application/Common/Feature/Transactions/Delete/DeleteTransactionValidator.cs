using FluentValidation;
using FoundFlow.Shared.Messages;
using FoundFlow.Shared.Validations;

namespace FoundFlow.Application.Common.Feature.Transactions.Delete;

public class DeleteTransactionValidator : AbstractValidator<DeleteTransactionRequest>
{
    public DeleteTransactionValidator()
    {
        RuleFor(x => x.Id.ToString())
        .NotEmpty()
        .NotNull()
        .WithMessage(ErrorMessages.TransactionsValidationIdTransactionRequireMessage)
        .MustBeGuid()
        .WithMessage(ErrorMessages.TransactionsValidationIdTransactionRequireMessage);
    }
}