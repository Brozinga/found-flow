using System;

namespace FoundFlow.Application.Common.Feature.Categories.Delete;

/// <summary>
/// Representa a resposta da solicitação de exclusão de uma categoria.
/// </summary>
public sealed class DeleteCategorieResponse
{
    /// <summary>
    /// Cria uma nova instância de DeleteCategorieResponse.
    /// </summary>
    /// <param name="categorieId">O identificador único da categoria excluída.</param>
    /// <param name="succeeded">Indica se a exclusão da categoria foi bem-sucedida (padrão: true).</param>
    public DeleteCategorieResponse(Guid categorieId, bool succeeded = true)
    {
        Succeeded = succeeded;
        CategorieId = categorieId;
    }

    /// <summary>
    /// O identificador único (UUID) da categoria excluída.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid CategorieId { get; }

    /// <summary>
    /// Indica se a exclusão da categoria foi bem-sucedida.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Succeeded { get; }
}
