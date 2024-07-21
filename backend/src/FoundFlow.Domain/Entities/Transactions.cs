using System;
using System.ComponentModel.DataAnnotations.Schema;
using FoundFlow.Domain.Entities.Base;
using FoundFlow.Domain.Enums;
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
        string transactionName,
        decimal amount,
        TransactionType transactionType,
        PaymentType paymentStatus,
        DateTime creationDate = default,
        DateTime paymentDate = default)
    {
        Id = id;
        CategoryId = category.Id;
        Category = category;
        UserId = user.Id;
        User = user;
        TransactionName = transactionName;
        Amount = amount;
        TransactionType = transactionType.GetDescription();
        CreationDate = creationDate;
        PaymentDate = paymentDate;
        PaymentStatus = paymentStatus.GetDescription();
    }

    public Transactions(
        Categories category,
        Users user,
        string transactionName,
        decimal amount,
        TransactionType transactionType,
        PaymentType paymentStatus,
        DateTime creationDate = default,
        DateTime paymentDate = default)
    {
        Id = Guid.NewGuid();
        CategoryId = category.Id;
        Category = category;
        UserId = user.Id;
        User = user;
        TransactionName = transactionName;
        Amount = amount;
        TransactionType = transactionType.GetDescription();
        CreationDate = creationDate;
        PaymentDate = paymentDate;
        PaymentStatus = paymentStatus.GetDescription();
    }

    public Transactions(
        Guid id,
        Categories category,
        Users user,
        string transactionName,
        decimal amount,
        string transactionType,
        string paymentStatus,
        DateTime creationDate = default,
        DateTime paymentDate = default)
    {
        Id = id;
        CategoryId = category.Id;
        Category = category;
        UserId = user.Id;
        User = user;
        TransactionName = transactionName;
        Amount = amount;
        TransactionType = transactionType;
        CreationDate = creationDate;
        PaymentDate = paymentDate;
        PaymentStatus = paymentStatus;
    }

    /// <summary>
    /// Id de co-relação com a tabela de Categorias (Categories).
    /// </summary>
    /// <example>e281dbd8-e8a8-4b8d-aafd-a54eccc3e7c8</example>
    [ForeignKey("category_id")]
    public Guid CategoryId { get; }

    /// <summary>
    /// Id de co-relação com a tabela de Usuários (Users).
    /// </summary>
    /// <example>e281dbd8-e8a8-4b8d-aafd-a54eccc3e7c8</example>
    [ForeignKey("user_id")]
    public Guid UserId { get; }

    /// <summary>
    /// Nome da transação.
    /// </summary>
    /// <example>Salário</example>
    public string TransactionName { get; }

    /// <summary>
    /// Valor da transação.
    /// </summary>
    /// <example>10.45</example>
    public decimal Amount { get; }

    /// <summary>
    /// Indica o tipo da transação. Consulte a documentação do enum <see cref="Enums.TransactionType"/> para os valores possíveis.
    /// </summary>
    /// <example>RECEITA</example>
    /// <example>DESPESA</example>
    public string TransactionType { get; }

    /// <summary>
    /// Data da criação.
    /// </summary>
    /// <example>2024-01-01T22:40:32</example>
    public DateTime CreationDate { get; }

    /// <summary>
    /// Data de pagamento.
    /// </summary>
    /// <example>2024-01-01T22:40:32</example>
    public DateTime PaymentDate { get; }

    /// <summary>
    /// Indica o status do pagamento. Consulte a documentação do enum <see cref="Enums.PaymentType"/> para os valores possíveis.
    /// </summary>
    /// <example>OK</example>
    /// <example>PENDENTE</example>
    /// <example>CANCELADO</example>
    public string PaymentStatus { get; }

    /// <summary>
    /// Usuário à qual esta transação pertence. Consulte a documentação de <see cref="Users"/> para mais detalhes.
    /// </summary>
    public virtual Users User { get; }

    /// <summary>
    /// Categoria à qual esta transação pertence. Consulte a documentação de <see cref="Categories"/> para mais detalhes.
    /// </summary>
    public virtual Categories Category { get; }
}