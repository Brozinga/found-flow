using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FoundFlow.Domain.Entities.Base;

[ExcludeFromCodeCoverage]
public abstract class EntityBase<TId> : IEntityBase<TId>
{
    private readonly List<DomainEventBase> _domainEvents = new();

    /// <summary>
    /// Código de identificação.
    /// </summary>
    /// <example>e281dbd8-e8a8-4b8d-aafd-a54eccc3e7c8</example>
    [Key]
    public virtual TId Id { get; set; }

    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    internal void ClearDomainEvents() => _domainEvents.Clear();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
}