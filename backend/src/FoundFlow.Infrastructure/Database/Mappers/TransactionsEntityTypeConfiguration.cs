using FoundFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundFlow.Infrastructure.Database.Mappers;

public class TransactionsEntityTypeConfiguration : IEntityTypeConfiguration<Transactions>
{
    public void Configure(EntityTypeBuilder<Transactions> builder)
    {
        builder.ToTable("transactions");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("transaction_id")
            .HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(t => t.TransactionName)
            .HasColumnName("transaction_name")
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(t => t.Amount)
            .HasColumnName("amount")
            .IsRequired();
        builder.Property(t => t.TransactionType)
            .HasColumnName("transaction_type")
            .HasMaxLength(50);
        builder.Property(t => t.PaymentStatus)
            .HasColumnName("payment_status")
            .HasMaxLength(50);
        builder.Property(t => t.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired()
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
        builder.Property(t => t.PaymentDate)
            .HasColumnName("payment_date");

        // Relationships
        builder.Property(t => t.CategoryId)
            .HasColumnName("category_id");

        builder.HasOne(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.UserId)
            .HasColumnName("user_id");
        builder.HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}