using System;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Domain.Interfaces.Repositories;

namespace FoundFlow.Infrastructure.Database.Repositories;

public sealed class CategoriesRepository : RepositoryBase<Categories, Guid>, ICategoriesRepository
{
    public CategoriesRepository(IApplicationDbContext database)
        : base(database)
    {
    }
}