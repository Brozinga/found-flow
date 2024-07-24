using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;

namespace FoundFlow.Application.Common.Feature.Categories.Create;

public class CreateCategorieRequest : MediatR.IRequest<Result<CreateCategorieResponse>>, IAuthorize
{
    /// <summary>
    /// O nome da categoria.
    /// </summary>
    /// <example>Transporte</example>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// A cor da categoria em formato hexadecimal (ex: #RRGGBB).
    /// </summary>
    /// <example>#7DDA58</example>
    [Required]
    public string Color { get; set; }

    /// <summary>
    /// O identificador único do usuário que está criando a categoria. (Ignorado na serialização JSON)
    /// </summary>
    [JsonIgnore]
    public Guid UserId { get; set; }
}