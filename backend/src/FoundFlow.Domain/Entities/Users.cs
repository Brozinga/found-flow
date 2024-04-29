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

    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool? NotificationEnabled { get; set; }
    public bool? Blocked { get; set; }
    public DateTime CreationDate { get; set; }

    public virtual IReadOnlyCollection<Transactions> Transactions { get; init; } = new Collection<Transactions>();
    public virtual IReadOnlyCollection<Categories> Categories { get; init; } = new Collection<Categories>();
}