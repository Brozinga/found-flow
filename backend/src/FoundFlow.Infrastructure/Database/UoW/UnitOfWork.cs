using System;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Domain.Interfaces.Repositories;

namespace FoundFlow.Infrastructure.Database.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _dbContext;

    public UnitOfWork(
        IApplicationDbContext dbContext,
        IUsersRepository usersRepository)
    {
        _dbContext = dbContext;
        UsersRepository = usersRepository;
    }

    public IUsersRepository UsersRepository { get; }

    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed && disposing)
        {
            _dbContext.Dispose();
        }

        this._disposed = true;
    }
}