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

    [ForeignKey("category_id")]
    public Guid CategoryId { get; private set; }

    [ForeignKey("user_id")]
    public Guid UserId { get; private set; }
    public string TransactionName { get; private set; }
    public decimal Amount { get; private set; }

    public string TransactionType { get; private set; }

    public DateTime CreationDate { get; private set; }

    public DateTime PaymentDate { get; private set; }

    public string PaymentStatus { get; private set; }

    public virtual Users User { get; private set; }
    public virtual Categories Category { get; private set; }
}