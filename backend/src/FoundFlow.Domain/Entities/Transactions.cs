using System;
using System.ComponentModel.DataAnnotations.Schema;
using FoundFlow.Domain.Entities.Base;
using FoundFlow.Domain.Enums;
using FoundFlow.Shared.Extensions;
using HotChocolate.Data;

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
        DateTime creationDate = default,
        DateTime paymentDate = default,
        string paymentStatus = null)
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
        PaymentStatus = paymentStatus;
    }

    public Transactions(
        Categories category,
        Users user,
        string transactionName,
        decimal amount,
        TransactionType transactionType,
        DateTime creationDate = default,
        DateTime paymentDate = default,
        string paymentStatus = null)
    {
        CategoryId = category.Id;
        Category = category;
        UserId = user.Id;
        User = user;
        TransactionName = transactionName;
        Amount = amount;
        TransactionType = transactionType.GetDescription();
        CreationDate = creationDate;
        PaymentDate = paymentDate;
        PaymentStatus = paymentStatus;
    }

    public Transactions(
        Categories category,
        Users user,
        string transactionName,
        decimal amount,
        string transactionType,
        DateTime creationDate = default,
        DateTime paymentDate = default,
        string paymentStatus = null)
    {
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
    public Guid CategoryId { get; set; }

    [ForeignKey("user_id")]
    public Guid UserId { get; set; }
    public string TransactionName { get; set; }
    public decimal Amount { get; set; }

    public string TransactionType { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentStatus { get; set; }

    public virtual Users User { get; set; }
    public virtual Categories Category { get; set; }
}