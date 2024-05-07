using System;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoundFlow.Domain.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    public DbSet<Users> Users { get; }
    public DbSet<Categories> Categories { get; }
    public DbSet<Transactions> Transactions { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        where TEntity : class;

    Task<int> SaveAsync(CancellationToken cancellationToken);
}
