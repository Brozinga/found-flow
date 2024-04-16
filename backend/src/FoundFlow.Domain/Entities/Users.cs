using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        UserName = userName;
        Email = email;
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
        UserName = userName;
        Email = email;
        Password = password;
        NotificationEnabled = notificationEnabled;
        Blocked = blocked;
        CreationDate = creationDate;
    }

    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool? NotificationEnabled { get; private set; }
    public bool? Blocked { get; private set; }
    public DateTime CreationDate { get; private set; }

    public virtual IReadOnlyCollection<Transactions> Transactions { get; init; } = new Collection<Transactions>();
    public virtual IReadOnlyCollection<Categories> Categories { get; init; } = new Collection<Categories>();
}