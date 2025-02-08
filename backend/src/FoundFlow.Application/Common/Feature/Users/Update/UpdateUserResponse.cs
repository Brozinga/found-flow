using System;

namespace FoundFlow.Application.Common.Feature.Users.Update;

/// <summary>
/// Representa a resposta da solicitação de atualização de dados de um usuário.
/// </summary>
/// <remarks>
/// Cria uma nova instância de `UpdateUserResponse`.
/// </remarks>
/// <param name="userId">O identificador único do usuário atualizado.</param>
/// <param name="succeeded">Indica se a atualização do usuário foi bem-sucedida (padrão: true).</param>
public sealed class UpdateUserResponse(Guid userId, bool succeeded = true)
{
    /// <summary>
    /// O identificador único de um usuário atualizado <a href="https://www.rfc-editor.org/rfc/rfc4122">(UUID) de acordo com a RFC4122</a>.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid UserId { get; } = userId;

    /// <summary>
    /// Indica se a atualização do usuário foi bem-sucedida.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Succeeded { get; } = succeeded;
}
