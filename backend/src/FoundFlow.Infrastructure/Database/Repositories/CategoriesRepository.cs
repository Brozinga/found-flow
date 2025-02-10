using System;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Domain.Interfaces.Repositories;

namespace FoundFlow.Infrastructure.Database.Repositories;

public sealed class CategoriesRepository(IApplicationDbContext database)
    : RepositoryBase<Categories, Guid>(database), ICategoriesRepository;