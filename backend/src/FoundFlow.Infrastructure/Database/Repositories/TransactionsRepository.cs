using System;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Domain.Interfaces.Repositories;

namespace FoundFlow.Infrastructure.Database.Repositories;

public sealed class TransactionsRepository(IApplicationDbContext database)
    : RepositoryBase<Transactions, Guid>(database), ITransactionsRepository;