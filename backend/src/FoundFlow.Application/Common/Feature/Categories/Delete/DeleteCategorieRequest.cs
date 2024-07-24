using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;

namespace FoundFlow.Application.Common.Feature.Categories.Delete;

public class DeleteCategorieRequest : MediatR.IRequest<Result<DeleteCategorieResponse>>, IAuthorize
{
    /// <summary>
    /// O identificador único da categoria a ser excluída <a href="https://www.rfc-editor.org/rfc/rfc4122">(UUID) de acordo com a RFC4122</a>.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// O identificador único (UUID) do usuário que está solicitando a exclusão. (Ignorado na serialização JSON)
    /// </summary>
    [JsonIgnore]
    public Guid UserId { get; set; }
}