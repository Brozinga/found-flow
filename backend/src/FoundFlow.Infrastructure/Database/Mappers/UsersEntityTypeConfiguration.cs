using FoundFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundFlow.Infrastructure.Database.Mappers;

public class UsersEntityTypeConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnName("user_id")
            .HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(u => u.UserName)
            .HasColumnName("user_name")
            .IsRequired()
            .HasMaxLength(150);
        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(150);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Password)
            .HasColumnName("password")
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(u => u.NotificationEnabled).HasColumnName("notification_enabled");
        builder.Property(u => u.Blocked).HasColumnName("blocked");
        builder.Property(u => u.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired()
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
    }
}