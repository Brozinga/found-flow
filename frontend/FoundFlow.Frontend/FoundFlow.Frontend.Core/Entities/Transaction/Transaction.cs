namespace FoundFlow.Frontend.Core.Entities.Transaction;

public class Transaction
{
    public Guid CategoryId { get; init; }
    
    public string TransactionName { get; init; }
    
    public decimal Amount { get; init; }
    
    /// <example>RECEITA</example>
    /// <example>DESPESA</example>
    public string TransactionType { get; init; }
    
    /// <example>OK</example>
    /// <example>PENDENTE</example>
    /// <example>CANCELADO</example>
    public string PaymentStatus { get; init; }
    
    public DateTime CreationDate { get; init; }

    public DateTime? PaymentDate { get; init; }
}