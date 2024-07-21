using System;
using FoundFlow.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoundFlow.Application.Common.Feature.Users.Login;

/// <summary>
/// Armazena informações sobre tentativas de login malsucedidas para um endereço de e-mail específico.
/// </summary>
/// <example>
/// new BlockInfo
/// {
///     Id = ObjectId.GenerateNewId(),
///     EmailKey = "usuario@example.com",
///     Attempts = 3,
///     BlockedSince = DateTime.UtcNow
/// }
/// </example>
public class BlockInfo : IManagerModel
{
    /// <summary>
    /// O identificador único (ObjectId) do registro no banco de dados.
    /// </summary>
    /// <example>ObjectId("605616e6a7b57a0e04000000")</example>
    [BsonId]
    public ObjectId Id { get; set; }

    /// <summary>
    /// A chave única que identifica o usuário pelas tentativas de login (geralmente o e-mail).
    /// </summary>
    /// <example>usuario@example.com</example>
    public string EmailKey { get; set; }

    /// <summary>
    /// O número de tentativas de login malsucedidas consecutivas.
    /// </summary>
    /// <example>3</example>
    public int Attempts { get; set; } = 0;

    /// <summary>
    /// A data e hora em que o usuário foi bloqueado devido a tentativas excessivas de login.
    /// Se o usuário não estiver bloqueado, o valor será null.
    /// </summary>
    /// <example>2024-07-20T12:39:00Z</example>
    public DateTime? BlockedSince { get; set; }
}