using System;
using System.ComponentModel.DataAnnotations.Schema;
using FoundFlow.Domain.Entities.Base;
using FoundFlow.Shared.Extensions;

namespace FoundFlow.Domain.Entities;

public class Transactions : EntityBase<Guid>
{
    protected Transactions()
    {
    }

    public Transactions(
        Guid id,
        Categories category,
        Users user,
        TransactionValueObject transactionValue,
        DateTime creationDate = default,
        DateTime paymentDate = default)
    {
        Id = id;
        CategoryId = category.Id;
        Category = category;
        UserId = user.Id;
        User = user;
        TransactionName = transactionValue.TransactionName;
        Amount = transactionValue.Amount;
        TransactionType = transactionValue.TransactionType.GetDescription();
        PaymentStatus = transactionValue.PaymentStatus.GetDescription();
        CreationDate = creationDate;
        PaymentDate = paymentDate;
    }

    public Transactions(
        Categories category,
        Users user,
        TransactionValueObject transactionValue,
        DateTime creationDate = default,
        DateTime paymentDate = default)
    {
        Id = Guid.NewGuid();
        CategoryId = category.Id;
        Category = category;
        UserId = user.Id;
        User = user;
        TransactionName = transactionValue.TransactionName;
        Amount = transactionValue.Amount;
        TransactionType = transactionValue.TransactionType.GetDescription();
        PaymentStatus = transactionValue.PaymentStatus.GetDescription();
        CreationDate = creationDate;
        PaymentDate = paymentDate;
    }

    /// <summary>
    /// Id de co-relação com a tabela de Categorias (Categories).
    /// </summary>
    /// <example>e281dbd8-e8a8-4b8d-aafd-a54eccc3e7c8</example>
    [ForeignKey("category_id")]
    public Guid CategoryId { get; init; }

    /// <summary>
    /// Id de co-relação com a tabela de Usuários (Users).
    /// </summary>
    /// <example>e281dbd8-e8a8-4b8d-aafd-a54eccc3e7c8</example>
    [ForeignKey("user_id")]
    public Guid UserId { get; init; }

    /// <summary>
    /// Nome da transação.
    /// </summary>
    /// <example>Salário</example>
    public string TransactionName { get; init; }

    /// <summary>
    /// Valor da transação.
    /// </summary>
    /// <example>10.45</example>
    public decimal Amount { get; init; }

    /// <summary>
    /// Indica o tipo da transação. Consulte a documentação do enum <see cref="Enums.TransactionType"/> para os valores possíveis.
    /// </summary>
    /// <example>RECEITA</example>
    /// <example>DESPESA</example>
    public string TransactionType { get; init; }

    /// <summary>
    /// Data da criação.
    /// </summary>
    /// <example>2024-01-01T22:40:32</example>
    public DateTime CreationDate { get; init; }

    /// <summary>
    /// Data de pagamento.
    /// </summary>
    /// <example>2024-01-01T22:40:32</example>
    public DateTime PaymentDate { get; init; }

    /// <summary>
    /// Indica o status do pagamento. Consulte a documentação do enum <see cref="Enums.PaymentType"/> para os valores possíveis.
    /// </summary>
    /// <example>OK</example>
    /// <example>PENDENTE</example>
    /// <example>CANCELADO</example>
    public string PaymentStatus { get; init; }

    /// <summary>
    /// Usuário à qual esta transação pertence. Consulte a documentação de <see cref="Users"/> para mais detalhes.
    /// </summary>
    public virtual Users User { get; init; }

    /// <summary>
    /// Categoria à qual esta transação pertence. Consulte a documentação de <see cref="Categories"/> para mais detalhes.
    /// </summary>
    public virtual Categories Category { get; init; }
}