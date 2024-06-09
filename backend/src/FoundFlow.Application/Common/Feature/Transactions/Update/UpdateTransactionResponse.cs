using System;

namespace FoundFlow.Application.Common.Feature.Transactions.Update;

public sealed class UpdateTransactionResponse
{
    public UpdateTransactionResponse(Guid transactionId, bool succeeded = true)
    {
        Succeeded = succeeded;
        TransactionId = transactionId;
    }

    public Guid TransactionId { get; }
    public bool Succeeded { get; }
}
