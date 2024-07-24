using System;

namespace FoundFlow.Application.Common.Feature.Users.Create;

/// <summary>
/// Representa a resposta da solicitação de criação de um novo usuário.
/// </summary>
public sealed class CreateUserResponse
{
    /// <summary>
    /// Cria uma nova instância de `CreateUserResponse`.
    /// </summary>
    /// <param name="userId">O identificador único do usuário criado.</param>
    /// <param name="succeeded">Indica se a criação do usuário foi bem-sucedida (padrão: true).</param>
    public CreateUserResponse(Guid userId, bool succeeded = true)
    {
        Succeeded = succeeded;
        UserId = userId;
    }

    /// <summary>
    /// O identificador único de um usuário criado <a href="https://www.rfc-editor.org/rfc/rfc4122">(UUID) de acordo com a RFC4122</a>.
    /// </summary>
    /// <example>32bf3b72-db19-498c-ad3a-e2d6edde080f</example>
    public Guid UserId { get; }

    /// <summary>
    /// Indica se a criação do usuário foi bem-sucedida.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Succeeded { get; }
}
