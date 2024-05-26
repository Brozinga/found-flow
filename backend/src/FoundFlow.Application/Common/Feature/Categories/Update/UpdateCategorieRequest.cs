using System;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;

namespace FoundFlow.Application.Common.Feature.Categories.Update;

public class UpdateCategorieRequest : MediatR.IRequest<Result<UpdateCategorieResponse>>, IAuthorize
{
    /// <summary>
    /// Id da categoria.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome da categoria.
    /// </summary>
    /// <example>Trabalho</example>
    public string Name { get; set; }

    /// <summary>
    /// Cor em hexadecimal.
    /// </summary>
    /// <example>#7DDA58</example>
    public string Color { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }
}