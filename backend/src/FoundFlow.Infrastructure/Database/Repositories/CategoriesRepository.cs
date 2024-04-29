using System;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces.Repositories;

namespace FoundFlow.Infrastructure.Database.Repositories;

public sealed class CategoriesRepository : RepositoryBase<Users, Guid>, ICategoriesRepository
{
    public CategoriesRepository(ApplicationDbContext database)
        : base(database)
    {
    }
}