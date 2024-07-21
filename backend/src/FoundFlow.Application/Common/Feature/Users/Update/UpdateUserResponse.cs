using System;

namespace FoundFlow.Application.Common.Feature.Users.Update;

/// <summary>
/// Representa a resposta da solicitação de atualização de dados de um usuário.
/// </summary>
public sealed class UpdateUserResponse
{
    /// <summary>
    /// Cria uma nova instância de `UpdateUserResponse`.
    /// </summary>
    /// <param name="userId">O identificador único do usuário atualizado.</param>
    /// <param name="succeeded">Indica se a atualização do usuário foi bem-sucedida (padrão: true).</param>
    public UpdateUserResponse(Guid userId, bool succeeded = true)
    {
        Succeeded = succeeded;
        UserId = userId;
    }

    /// <summary>
    /// O identificador único (UUID) do usuário atualizado.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid UserId { get; }

    /// <summary>
    /// Indica se a atualização do usuário foi bem-sucedida.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Succeeded { get; }
}
