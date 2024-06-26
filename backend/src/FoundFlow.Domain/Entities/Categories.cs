﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using FoundFlow.Domain.Entities.Base;

namespace FoundFlow.Domain.Entities;

public class Categories : EntityBase<Guid>
{
    protected Categories()
    {
    }

    public Categories(
        Guid id,
        Users user,
        string categoryName,
        string color,
        DateTime creationDate = default)
    {
        Id = id;
        UserId = user.Id;
        User = user;
        CategoryName = categoryName.ToLower();
        Color = color;
        CreationDate = creationDate;
    }

    public Categories(
        Users user,
        string categoryName,
        string color,
        DateTime creationDate = default)
    {
        Id = Guid.NewGuid();
        UserId = user.Id;
        User = user;
        CategoryName = categoryName.ToLower();
        Color = color;
        CreationDate = creationDate;
    }

    public Guid UserId { get; private set; }
    public string CategoryName { get; private set; }
    public string Color { get; private set; }
    public DateTime CreationDate { get; private set; }
    public virtual Users User { get; private set; }
}