using System.ComponentModel;

namespace FoundFlow.Domain.Enums;

public enum TransactionType
{
    [Description("RECEITA")]
    RECEITA,
    [Description("DESPESA")]
    DESPESA
}