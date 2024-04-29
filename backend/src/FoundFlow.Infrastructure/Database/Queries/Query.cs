using System.Linq;
using FoundFlow.Domain.Entities;
using FoundFlow.Infrastructure.Database.SortTypes;
using HotChocolate;
using HotChocolate.Data;

namespace FoundFlow.Infrastructure.Database.Queries;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting(typeof(CategoriesSortInputType))]
    public IQueryable<Categories> GetCategories([Service] ApplicationDbContext context) => context.Categories;

    [UseProjection]
    [UseFiltering]
    [UseSorting(typeof(TransactionsSortInputType))]
    public IQueryable<Transactions> GetTransactions([Service] ApplicationDbContext context) => context.Transactions;

    [UseProjection]
    [UseFiltering]
    [UseSorting(typeof(UsersSortInputType))]
    public IQueryable<Users> GetUsers([Service] ApplicationDbContext context) => context.Users;
}