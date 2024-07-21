using System.ComponentModel;

namespace FoundFlow.Domain.Enums;

/// <summary>
/// Tipo da transação.
/// </summary>
public enum TransactionType
{
    /// <summary>Transação de entrada.</summary>
    [Description("RECEITA")]
    RECEITA,

    // <summary>Transação de saída.</summary>
    [Description("DESPESA")]
    DESPESA
}