using System;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;

namespace FoundFlow.Application.Common.Feature.Categories.Delete;

public class DeleteCategorieRequest : MediatR.IRequest<Result<DeleteCategorieResponse>>, IAuthorize
{
    /// <summary>
    /// Id da categoria.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }
}