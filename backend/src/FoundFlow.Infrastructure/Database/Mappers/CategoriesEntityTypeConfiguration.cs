using FoundFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundFlow.Infrastructure.Database.Mappers;

public class CategoriesEntityTypeConfiguration : IEntityTypeConfiguration<Categories>
{
    public void Configure(EntityTypeBuilder<Categories> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnName("category_id")
            .HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(c => c.CategoryName)
            .HasColumnName("category_name")
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(c => c.Color)
            .HasColumnName("color")
            .HasMaxLength(50);
        builder.Property(c => c.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        // Relations
        builder.Property(c => c.UserId).HasColumnName("user_id");
        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}