using System;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoundFlow.Domain.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    public DbSet<Users> Users { get; }
    public DbSet<Categories> Categories { get; }
    public DbSet<Transactions> Transactions { get; }

    Task<int> SaveAsync(CancellationToken cancellationToken);
}
