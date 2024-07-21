using System;

namespace FoundFlow.Application.Common.Feature.Transactions.Create;

/// <summary>
/// Representa a resposta da solicitação de criação de uma nova transação.
/// </summary>
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
    /// O identificador único (UUID) da transação criada.
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
