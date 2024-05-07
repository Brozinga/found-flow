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

    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }
    public bool? NotificationEnabled { get; }
    public bool? Blocked { get; }
    public DateTime CreationDate { get; }

    public virtual IReadOnlyCollection<Transactions> Transactions { get; init; } = new Collection<Transactions>();
    public virtual IReadOnlyCollection<Categories> Categories { get; init; } = new Collection<Categories>();
}