using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Infrastructure.Database.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FoundFlow.Infrastructure.Database;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Users> Users => Set<Users>();
    public DbSet<Categories> Categories => Set<Categories>();
    public DbSet<Transactions> Transactions => Set<Transactions>();

    public virtual Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return this.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsersEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoriesEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionsEntityTypeConfiguration());
    }
}