using System;

namespace FoundFlow.Application.Common.Feature.Categories.Create;

/// <summary>
/// Representa a resposta da solicitação de criação de uma nova categoria.
/// </summary>
public sealed class CreateCategorieResponse
{
    /// <summary>
    /// Cria uma nova instância de CreateCategorieResponse.
    /// </summary>
    /// <param name="categorieId">O identificador único da categoria criada.</param>
    /// <param name="succeeded">Indica se a criação da categoria foi bem-sucedida (padrão: true).</param>
    public CreateCategorieResponse(Guid categorieId, bool succeeded = true)
    {
        Succeeded = succeeded;
        CategorieId = categorieId;
    }

    /// <summary>
    /// O identificador único da categoria criada.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid CategorieId { get; }

    /// <summary>
    /// Indica se a criação da categoria foi bem-sucedida.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Succeeded { get; }
}
