using System;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Domain.Interfaces.Repositories;

namespace FoundFlow.Infrastructure.Database.UoW;

public class UnitOfWork(
    IApplicationDbContext dbContext,
    IUsersRepository usersRepository,
    ICategoriesRepository categoriesRepository,
    ITransactionsRepository transactionsRepository)
    : IUnitOfWork
{
    public IUsersRepository UsersRepository { get; } = usersRepository;
    public ICategoriesRepository CategoriesRepository { get; } = categoriesRepository;
    public ITransactionsRepository TransactionsRepository { get; } = transactionsRepository;

    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveAsync(cancellationToken);
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
            dbContext.Dispose();
        }

        this._disposed = true;
    }
}