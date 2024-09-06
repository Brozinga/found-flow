using System;
using System.Linq;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Infrastructure.Database.SortTypes;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using Microsoft.AspNetCore.Http;

namespace FoundFlow.Infrastructure.Database.Queries;

public class Query
{
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Query(ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    private Guid GetUserId()
    {
        string token = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
        ArgumentNullException.ThrowIfNull(token);

        var (_, _, userId) = _tokenService.Read(token);
        return userId;
    }

    [Authorize]
    [UseFiltering]
    [UseSorting(typeof(CategoriesSortInputType))]
    public IQueryable<Categories> GetCategories([Service] ApplicationDbContext context)
    {
        var userId = GetUserId();
        return context.Categories.Where(c => c.UserId == userId);
    }

    [Authorize]
    [UseProjection]
    [UseFiltering]
    [UseSorting(typeof(TransactionsSortInputType))]
    public IQueryable<Transactions> GetTransactions([Service] ApplicationDbContext context)
    {
        var userId = GetUserId();
        return context.Transactions.Where(t => t.UserId == userId);
    }

    [Authorize]
    [UseProjection]
    [UseFiltering]
    [UseSorting(typeof(UsersSortInputType))]
    public IQueryable<Users> GetUsers([Service] ApplicationDbContext context)
    {
        var userId = GetUserId();
        return context.Users.Where(u => u.Id == userId);
    }
}