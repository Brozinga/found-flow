using System;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Domain.Interfaces.Repositories;

namespace FoundFlow.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUsersRepository UsersRepository { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken);
}