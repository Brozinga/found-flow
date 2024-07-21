using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using FoundFlow.Domain.Entities.Base;

namespace FoundFlow.Domain.Entities;

public class Users : EntityBase<Guid>
{
    protected Users()
    {
    }

    public Users(
        Guid id,
        string userName,
        string email,
        string password,
        bool? notificationEnabled,
        bool? blocked,
        DateTime creationDate = default)
    {
        Id = id;
        UserName = userName.ToLower(CultureInfo.CurrentCulture);
        Email = email.ToLower(CultureInfo.CurrentCulture);
        Password = password;
        NotificationEnabled = notificationEnabled;
        Blocked = blocked;
        CreationDate = creationDate;
    }

    public Users(
        string userName,
        string email,
        string password,
        bool? notificationEnabled,
        bool? blocked,
        DateTime creationDate = default)
    {
        Id = Guid.NewGuid();
        UserName = userName.ToLower(CultureInfo.CurrentCulture);
        Email = email.ToLower(CultureInfo.CurrentCulture);
        Password = password;
        NotificationEnabled = notificationEnabled;
        Blocked = blocked;
        CreationDate = creationDate;
    }

    /// <summary>
    /// Nome do usuário.
    /// </summary>
    /// <example>Antonio da Silva</example>
    public string UserName { get; }

    /// <summary>
    /// E-mail do usuário.
    /// </summary>
    /// <example>antonio@email.com</example>
    public string Email { get; }

    /// <summary>
    /// Senha do usuário criptografado.
    /// </summary>
    /// <example>$2a$10$5zhIpU7rV/MJdbfMN6L5T.EnF719ITbbCvqUdiQ5Vnpr2IHKyrk/2</example>
    public string Password { get; }

    /// <summary>
    /// Flag para informar se as notificações estão habilitadas ou não.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool? NotificationEnabled { get; }

    /// <summary>
    /// Flag para informar se o usuário está bloqueado ou não.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    public bool? Blocked { get; }

    /// <summary>
    /// Data da criação.
    /// </summary>
    /// <example>2024-01-01T22:40:32</example>
    public DateTime CreationDate { get; }

    /// <summary>
    /// Transações associadas a este usuário.
    /// </summary>
    /// <returns>Uma coleção somente leitura de objetos <see cref="Entities.Transactions"/>.</returns>
    public virtual IReadOnlyCollection<Transactions> Transactions { get; init; } = new Collection<Transactions>();

    /// <summary>
    /// Categorias associadas a este usuário.
    /// </summary>
    /// <returns>Uma coleção somente leitura de objetos <see cref="Entities.Categories"/>.</returns>
    public virtual IReadOnlyCollection<Categories> Categories { get; init; } = new Collection<Categories>();
}