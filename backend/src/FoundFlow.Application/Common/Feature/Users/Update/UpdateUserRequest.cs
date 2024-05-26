using System;
using System.Text.Json.Serialization;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;

namespace FoundFlow.Application.Common.Feature.Users.Update;

public class UpdateUserRequest : MediatR.IRequest<Result<UpdateUserResponse>>, IAuthorize
{
    /// <summary>
    /// Nome do usuário.
    /// </summary>
    /// <example>Luiz Antônio</example>
    public string UserName { get; set; }

    /// <summary>
    /// Senha para um novo cadastro.
    /// </summary>
    /// <example>BomD1@</example>
    public string Password { get; set; }

    /// <summary>
    /// Confirmação de senha, precisa ser a mesma informação contida na Senha.
    /// </summary>
    /// <example>BomD1@</example>
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Ativar ou desativar o recebimento de notificação.
    /// </summary>
    /// <example>true</example>
    public bool Notification { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }
}