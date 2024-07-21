using System.ComponentModel;

namespace FoundFlow.Domain.Enums;

/// <summary>
/// Indica o status do pagamento.
/// </summary>
public enum PaymentType
{
    /// <summary>
    /// Pagamento realizado com sucesso.
    /// </summary>
    [Description("OK")]
    OK,

    /// <summary>
    /// Pagamento ainda não processado ou aguardando confirmação.
    /// </summary>
    [Description("PENDENTE")]
    PENDENTE,

    /// <summary>
    /// Pagamento cancelado pelo usuário ou sistema.
    /// </summary>
    [Description("CANCELADO")]
    CANCELADO
}