using System;

namespace FoundFlow.Application.Common.Feature.Categories.Delete;

/// <summary>
/// Representa a resposta da solicitação de exclusão de uma categoria.
/// </summary>
/// <remarks>
/// Cria uma nova instância de DeleteCategorieResponse.
/// </remarks>
/// <param name="categorieId">O identificador único da categoria excluída.</param>
/// <param name="succeeded">Indica se a exclusão da categoria foi bem-sucedida (padrão: true).</param>
public sealed class DeleteCategorieResponse(Guid categorieId, bool succeeded = true)
{
    /// <summary>
    /// O identificador único da categoria excluída <a href="https://www.rfc-editor.org/rfc/rfc4122">(UUID) de acordo com a RFC4122</a>.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid CategorieId { get; } = categorieId;

    /// <summary>
    /// Indica se a exclusão da categoria foi bem-sucedida.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Succeeded { get; } = succeeded;
}
