using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Inventory.Domain.AggregatesModel.Identity;

namespace ECommerce.Inventory.Data.EntityConfigurations.IdentityEntities;

public class TokenConfiguration : BaseEntityConfiguration<Token>
{
    public override void Configure(EntityTypeBuilder<Token> builder)
    {
        base.Configure(builder);

        builder.HasIndex(
            $"{nameof(Token.Application)}{nameof(Application.Id)}",
            nameof(Token.Status),
            nameof(Token.Subject),
            nameof(Token.Type));

        builder.HasIndex(x => x.ReferenceId)
            .IsUnique();

        builder.Property(t => t.ConcurrencyToken)
            .HasMaxLength(50)
            .IsConcurrencyToken();

        builder.Property(t => t.ReferenceId)
            .HasMaxLength(100);

        builder.Property(t => t.CreationDate)
            .HasColumnName("created_at");

        builder.Property(t => t.ExpirationDate)
            .HasColumnName("expired_at");

        builder.Property(t => t.RedemptionDate)
            .HasColumnName("redeemed_at");

        builder.Property(t => t.RedemptionDate)
            .HasColumnName("redeemed_at");

        builder.Property(t => t.Properties)
            .HasDefaultValueSql("'{}'");

        builder.Property(t => t.Payload)
            .HasDefaultValueSql("''");

        builder.Property(t => t.Status)
            .HasMaxLength(50)
            .HasDefaultValueSql("''");

        builder.Property(t => t.Subject)
            .HasMaxLength(200)
            .HasDefaultValueSql("''");

        builder.Property(t => t.Type)
            .HasMaxLength(50)
            .HasDefaultValueSql("''");

        builder.HasOne(x => x.Application)
            .WithMany(x => x.Tokens)
            .HasForeignKey($"{nameof(Application)}{nameof(Application.Id)}");

        builder.HasOne(x => x.Authorization)
            .WithMany(x => x.Tokens)
            .HasForeignKey($"{nameof(Authorization)}{nameof(Authorization.Id)}");
    }
}
