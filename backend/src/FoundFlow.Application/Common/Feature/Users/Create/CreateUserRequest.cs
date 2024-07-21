using System;
using FoundFlow.Application.Models;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.Create;

/// <summary>
/// Representa uma solicitação para criar um novo usuário.
/// </summary>
public class CreateUserRequest : IRequest<Result<CreateUserResponse>>
{
    /// <summary>
    /// O nome de usuário desejado.
    /// </summary>
    /// <example>Luiz Antônio</example>
    /// <example>Maria Silva</example>
    public string UserName { get; set; }

    /// <summary>
    /// O endereço de e-mail do usuário.
    /// </summary>
    /// <example>luiz.antonio@email.com</example>
    /// <example>maria.silva@email.com</example>
    public string Email { get; set; }

    /// <summary>
    /// A senha para o novo cadastro.
    /// </summary>
    /// <example>BomD1@</example>
    /// <example>S3nh4F0rt3!</example>
    public string Password { get; set; }

    /// <summary>
    /// A confirmação da senha, que deve ser idêntica à senha informada.
    /// </summary>
    /// <example>BomD1@</example>
    /// <example>S3nh4F0rt3!</example>
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Indica se o usuário deseja receber notificações.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Notification { get; set; }
}