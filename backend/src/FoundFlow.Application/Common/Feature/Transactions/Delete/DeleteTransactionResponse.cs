using System;

namespace FoundFlow.Application.Common.Feature.Transactions.Delete;

public sealed class DeleteTransactionResponse
{
    public DeleteTransactionResponse(Guid transactionId, bool succeeded = true)
    {
        Succeeded = succeeded;
        TransactionId = transactionId;
    }

    public Guid TransactionId { get; }
    public bool Succeeded { get; }
}
