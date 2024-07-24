using System;

namespace FoundFlow.Application.Common.Feature.Transactions.Delete;

/// <summary>
/// Representa a resposta da solicitação de exclusão de uma transação.
/// </summary>
public sealed class DeleteTransactionResponse
{
    /// <summary>
    /// Cria uma nova instância de `DeleteTransactionResponse`.
    /// </summary>
    /// <param name="transactionId">O identificador único da transação excluída.</param>
    /// <param name="succeeded">Indica se a exclusão da transação foi bem-sucedida (padrão: true).</param>
    public DeleteTransactionResponse(Guid transactionId, bool succeeded = true)
    {
        Succeeded = succeeded;
        TransactionId = transactionId;
    }

    /// <summary>
    /// O identificador único de uma transação excluída <a href="https://www.rfc-editor.org/rfc/rfc4122">(UUID) de acordo com a RFC4122</a>.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid TransactionId { get; }

    /// <summary>
    /// Indica se a exclusão da transação foi bem-sucedida.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Succeeded { get; }
}
