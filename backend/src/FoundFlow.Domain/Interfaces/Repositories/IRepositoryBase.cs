using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Domain.Entities.Base;

namespace FoundFlow.Domain.Interfaces.Repositories;

public interface IRepositoryBase<T, in TId>
    where T : IEntityBase<TId>
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    T Update(T entity);
    T Delete(T entity);
    Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken);
    Task<T> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<List<T>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}