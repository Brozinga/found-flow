using System;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;

namespace FoundFlow.Application.Common.Feature.Users.Update;

/// <summary>
/// Representa uma solicitação para atualizar os dados de um usuário existente.
/// </summary>
public class UpdateUserRequest : MediatR.IRequest<Result<UpdateUserResponse>>, IAuthorize
{
    /// <summary>
    /// O novo nome de usuário desejado.
    /// </summary>
    /// <example>Luiz Antônio</example>
    public string UserName { get; set; }

    /// <summary>
    /// A nova senha para o usuário.
    /// </summary>
    /// <example>NovaSenha123!</example>
    public string Password { get; set; }

    /// <summary>
    /// A confirmação da nova senha, que deve ser idêntica à senha informada.
    /// </summary>
    /// <example>NovaSenha123!</example>
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Indica se o usuário deseja receber notificações.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Notification { get; set; }

    /// <summary>
    /// O identificador único (UUID) do usuário que está sendo atualizado. (Ignorado na serialização JSON)
    /// </summary>
    [JsonIgnore]
    public Guid UserId { get; set; }
}