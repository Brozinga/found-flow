using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Enums;

namespace FoundFlow.Application.Common.Feature.Transactions.Create;

public class CreateTransactionRequest : MediatR.IRequest<Result<CreateTransactionResponse>>, IAuthorize
{
    /// <summary>
    /// Título da transação.
    /// </summary>
    /// <example>Salário</example>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// O identificador único da categoria associada <a href="https://www.rfc-editor.org/rfc/rfc4122">(UUID) de acordo com a RFC4122</a>.
    /// </summary>
    /// <example>714c91a5-cf12-4705-be64-68a3b007d033</example>
    [Required]
    public Guid CategorieId { get; set; }

    /// <summary>
    /// Valor da transação.
    /// </summary>
    /// <example>164.32</example>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// Tipo da transação. Consulte <see cref="Domain.Enums.TransactionType"/> para os valores possíveis.
    /// </summary>
    /// <example>RECEITA</example>
    /// <example>DESPESA</example>
    [Required]
    public TransactionType TransactionType { get; set; }

    /// <summary>
    /// Data do pagamento da despesa ou data do crédito de receita no formato <a href="https://www.w3.org/TR/NOTE-datetime">(ISO 8601)</a>.
    /// </summary>
    /// <example>2024-02-10T12:37:56</example>
    [Required]
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Status do pagamento da despesa ou do crédito de receita. Consulte <see cref="Domain.Enums.PaymentType"/> para os valores possíveis.
    /// </summary>
    /// <example>OK</example>
    /// <example>PENDENTE</example>
    /// <example>CANCELADO</example>
    [Required]
    public PaymentType PaymentStatus { get; set; }

    /// <summary>
    /// O identificador único (UUID) do usuário que está solicitando a criação. (Ignorado na serialização JSON)
    /// </summary>
    [JsonIgnore]
    public Guid UserId { get; set; }
}