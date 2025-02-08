using System;

namespace FoundFlow.Application.Common.Feature.Transactions.Update;

public sealed class UpdateTransactionResponse(Guid transactionId, bool succeeded = true)
{
    public Guid TransactionId { get; } = transactionId;
    public bool Succeeded { get; } = succeeded;
}
