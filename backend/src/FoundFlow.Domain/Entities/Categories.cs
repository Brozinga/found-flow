using System;
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

    /// <summary>
    /// Id de co-relação com a tabela de usuários (Users).
    /// </summary>
    /// <example>e281dbd8-e8a8-4b8d-aafd-a54eccc3e7c8</example>
    public Guid UserId { get; }

    /// <summary>
    /// Nome da categoria.
    /// </summary>
    /// <example>Casa</example>
    public string CategoryName { get; }

    /// <summary>
    /// Hexadecimal com o valor da cor.
    /// </summary>
    /// <example>#FFFFFF</example>
    public string Color { get; }

    /// <summary>
    /// Data da criação.
    /// </summary>
    /// <example>2024-01-01T22:40:32</example>
    public DateTime CreationDate { get; }

    /// <summary>
    /// Usuário à qual esta categoria pertence. Consulte a documentação de <see cref="Users"/> para mais detalhes.
    /// </summary>
    public virtual Users User { get; }
}