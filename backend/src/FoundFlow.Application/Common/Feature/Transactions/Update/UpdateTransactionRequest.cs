using System;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Enums;

namespace FoundFlow.Application.Common.Feature.Transactions.Update;

public class UpdateTransactionRequest : MediatR.IRequest<Result<UpdateTransactionResponse>>, IAuthorize
{
    /// <summary>
    /// Id da transação.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Titulo da transacao.
    /// </summary>
    /// <example>Salário</example>
    public string Title { get; set; }

    /// <summary>
    /// Id da categoria associada.
    /// </summary>
    /// <example>714c91a5-cf12-4705-be64-68a3b007d033</example>
    public Guid CategorieId { get; set; }

    /// <summary>
    /// Valor da transação.
    /// </summary>
    /// <example>164.32</example>
    public decimal Amount { get; set; }

    /// <summary>
    /// Tipo da transação, pode ser RECEITA, ou DESPESA.
    /// </summary>
    /// <example>RECEITA</example>
    public TransactionType TransactionType { get; set; }

    /// <summary>
    /// Data do pagamento da despesa ou data do crédito de receita.
    /// </summary>
    /// <example>2024-02-10T00:00:00</example>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Se o pagamento da despesa ou o crédito da receita já está - OK ou PENDENTE.
    /// </summary>
    /// <example>OK</example>
    public PaymentType PaymentStatus { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }
}