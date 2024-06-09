using System;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;

namespace FoundFlow.Application.Common.Feature.Transactions.Delete;

public class DeleteTransactionRequest : MediatR.IRequest<Result<DeleteTransactionResponse>>, IAuthorize
{
    /// <summary>
    /// Id da transação.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }
}