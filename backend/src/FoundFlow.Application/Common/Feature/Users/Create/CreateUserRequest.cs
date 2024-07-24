using System;
using System.ComponentModel.DataAnnotations;
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
    [Required]
    public string UserName { get; set; }

    /// <summary>
    /// O endereço de e-mail do usuário.
    /// </summary>
    /// <example>luiz.antonio@email.com</example>
    [Required]
    public string Email { get; set; }

    /// <summary>
    /// A senha para o novo cadastro.
    /// </summary>
    /// <example>S3nh4F0rt3!</example>
    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    /// <summary>
    /// A confirmação da senha, que deve ser idêntica à senha informada.
    /// </summary>
    /// <example>S3nh4F0rt3!</example>
    [Required]
    [MinLength(6)]
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Indica se o usuário deseja receber notificações.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool Notification { get; set; }
}