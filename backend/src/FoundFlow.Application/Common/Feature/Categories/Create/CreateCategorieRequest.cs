using System;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;

namespace FoundFlow.Application.Common.Feature.Categories.Create;

public class CreateCategorieRequest : MediatR.IRequest<Result<CreateCategorieResponse>>, IAuthorize
{
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