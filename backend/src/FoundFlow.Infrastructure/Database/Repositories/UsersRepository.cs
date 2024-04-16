using System;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces.Repositories;

namespace FoundFlow.Infrastructure.Database.Repositories;

public sealed class UsersRepository : RepositoryBase<Users, Guid>, IUsersRepository
{
    public UsersRepository(ApplicationDbContext database)
        : base(database)
    {
    }
}