using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;

namespace FoundFlow.Application.Common.Feature.Categories.Update;

/// <summary>
/// Representa uma solicitação para atualizar uma categoria existente.
/// </summary>
public class UpdateCategorieRequest : MediatR.IRequest<Result<UpdateCategorieResponse>>, IAuthorize
{
    /// <summary>
    /// O identificador único da categoria a ser atualizada <a href="https://www.rfc-editor.org/rfc/rfc4122">(UUID) de acordo com a RFC4122</a>.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// O novo nome da categoria.
    /// </summary>
    /// <example>Trabalho Remoto</example>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// A nova cor da categoria em formato hexadecimal (ex: #RRGGBB).
    /// </summary>
    /// <example>#7DDA58</example>
    [Required]
    public string Color { get; set; }

    /// <summary>
    /// O identificador único (UUID) do usuário que está solicitando a atualização. (Ignorado na serialização JSON)
    /// </summary>
    [JsonIgnore]
    public Guid UserId { get; set; }
}