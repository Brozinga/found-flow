using System;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Domain.Interfaces.Repositories;

namespace FoundFlow.Infrastructure.Database.Repositories;

public sealed class UsersRepository(IApplicationDbContext database)
    : RepositoryBase<Users, Guid>(database), IUsersRepository;