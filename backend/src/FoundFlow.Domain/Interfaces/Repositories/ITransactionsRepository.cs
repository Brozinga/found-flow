using System;
using FoundFlow.Domain.Entities;

namespace FoundFlow.Domain.Interfaces.Repositories;

public interface ITransactionsRepository : IRepositoryBase<Users, Guid>;