using System;
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
        CategoryName = categoryName;
        Color = color;
        CreationDate = creationDate;
    }

    public Categories(
        Users user,
        string categoryName,
        string color,
        DateTime creationDate = default)
    {
        UserId = user.Id;
        User = user;
        CategoryName = categoryName;
        Color = color;
        CreationDate = creationDate;
    }

    [ForeignKey("user_id")]
    public Guid UserId { get; set; }
    public string CategoryName { get; set; }
    public string Color { get; set; }
    public DateTime CreationDate { get; set; }
    public virtual Users User { get; set; }
}