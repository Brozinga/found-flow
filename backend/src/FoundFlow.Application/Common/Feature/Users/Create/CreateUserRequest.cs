using System;
using FoundFlow.Application.Models;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.Create;

public class CreateUserRequest : IRequest<Result<CreateUserResponse>>
{
    /// <summary>
    /// Nome do usuário.
    /// </summary>
    /// <example>Luiz Antônio</example>
    public string UserName { get; set; }

    /// <summary>
    /// Email do usuário.
    /// </summary>
    /// <example>luiz.antonio@email.com</example>
    public string Email { get; set; }

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
}