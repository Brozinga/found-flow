using System;

namespace FoundFlow.Application.Common.Feature.Categories.Create;

/// <summary>
/// Representa a resposta da solicitação de criação de uma nova categoria.
/// </summary>
/// <remarks>
/// Cria uma nova instância de CreateCategorieResponse.
/// </remarks>
/// <param name="categorieId">O identificador único da categoria criada.</param>
/// <param name="succeeded">Indica se a criação da categoria foi bem-sucedida (padrão: true).</param>
public sealed class CreateCategorieResponse(Guid categorieId, bool succeeded = true)
{
    /// <summary>
    /// O identificador único de uma categoria criada <a href="https://www.rfc-editor.org/rfc/rfc4122">(UUID) de acordo com a RFC4122</a>.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid CategorieId { get; } = categorieId;

    /// <summary>
    /// Indica se a criação da categoria foi bem-sucedida.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Succeeded { get; } = succeeded;
}
