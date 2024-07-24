using System;

namespace FoundFlow.Application.Common.Feature.Transactions.Create;

public sealed class CreateTransactionResponse
{
    /// <summary>
    /// Cria uma nova instância de `CreateTransactionResponse`.
    /// </summary>
    /// <param name="transactionId">O identificador único da transação criada.</param>
    /// <param name="succeeded">Indica se a criação da transação foi bem-sucedida (padrão: true).</param>
    public CreateTransactionResponse(Guid transactionId, bool succeeded = true)
    {
        Succeeded = succeeded;
        TransactionId = transactionId;
    }

    /// <summary>
    /// O identificador único da transação criada <a href="https://www.rfc-editor.org/rfc/rfc4122">(UUID) de acordo com a RFC4122</a>.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid TransactionId { get; }

    /// <summary>
    /// Indica se a criação da transação foi bem-sucedida.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Succeeded { get; }
}
