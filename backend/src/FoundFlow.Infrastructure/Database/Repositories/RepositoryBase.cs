using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Domain.Entities.Base;
using FoundFlow.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoundFlow.Infrastructure.Database.Repositories;

public abstract class RepositoryBase<T, TId> : IRepositoryBase<T, TId>
    where T : class, IEntityBase<TId>
{
    protected readonly ApplicationDbContext _database;
    protected readonly DbSet<T> _table;

    protected RepositoryBase(ApplicationDbContext database)
    {
        _database = database;
        _table = database.Set<T>();
    }

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        Task<List<T>> result = _table.ToListAsync(cancellationToken);
        return result;
    }

    public Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        Task<T> result = _table.FirstOrDefaultAsync(et => et.Id.Equals(id), cancellationToken);
        return result;
    }

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        return Task.Run(
            () =>
        {
            EntityEntry<T> result = _table.Add(entity);
            return result.Entity;
        },
            cancellationToken);
    }

    public T Update(T entity)
    {
        EntityEntry<T> result = _table.Update(entity);
        return result.Entity;
    }

    public T Delete(T entity)
    {
        EntityEntry<T> result = _table.Remove(entity);
        return result.Entity;
    }

    public Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken)
    {
        return Task.Run(
            () =>
        {
            T entity = _table.FirstOrDefault(et => et.Id.Equals(id));
            return entity != null;
        },
            cancellationToken);
    }

    public Task<T> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        Task<T> result = _table.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        return result;
    }

    public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        Task<List<T>> result = _table.Where(predicate).ToListAsync(cancellationToken);
        return result;
    }

    public Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        Task<int> result = _table.Where(predicate).CountAsync();
        return result;
    }

    public Task<List<T>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var entities = _table
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return entities.ToListAsync(cancellationToken);
    }
}