using System;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;

namespace FoundFlow.Application.Common.Feature.Categories.Create;

/// <summary>
/// Representa uma solicitação para criar uma nova categoria.
/// </summary>
public class CreateCategorieRequest : MediatR.IRequest<Result<CreateCategorieResponse>>, IAuthorize
{
    /// <summary>
    /// O nome da categoria.
    /// </summary>
    /// <example>Trabalho</example>
    /// <example>Alimentação</example>
    /// <example>Transporte</example>
    public string Name { get; set; }

    /// <summary>
    /// A cor da categoria em formato hexadecimal (ex: #RRGGBB).
    /// </summary>
    /// <example>#7DDA58</example>
    /// <example>#FF0000</example>
    /// <example>#0000FF</example>
    public string Color { get; set; }

    /// <summary>
    /// O identificador único do usuário que está criando a categoria. (Ignorado na serialização JSON)
    /// </summary>
    [JsonIgnore]
    public Guid UserId { get; set; }
}