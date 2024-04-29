using System;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces.Repositories;

namespace FoundFlow.Infrastructure.Database.Repositories;

public sealed class TransactionsRepository : RepositoryBase<Users, Guid>, ITransactionsRepository
{
    public TransactionsRepository(ApplicationDbContext database)
        : base(database)
    {
    }
}