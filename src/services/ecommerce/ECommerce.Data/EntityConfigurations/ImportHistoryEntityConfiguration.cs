using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Data.EntityConfigurations;

public class ImportHistoryEntityConfiguration : BaseEntityConfiguration<ImportHistory>
{
    public override void Configure(EntityTypeBuilder<ImportHistory> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Remarks)
            .HasMaxLength(500)
            .HasDefaultValueSql("''");

        builder
            .Property(t => t.Notes)
            .HasMaxLength(500)
            .HasDefaultValueSql("''");

        builder.Property(p => p.DocumentType)
           .IsRequired(true)
           .HasConversion<string>()
           .HasMaxLength(50)
           .HasDefaultValueSql($"'{ImportDocumentType.UNSPECIFIED}'");

        builder.Property(p => p.Status)
           .IsRequired(true)
           .HasConversion<string>()
           .HasMaxLength(20)
           .HasDefaultValueSql($"'{ImportStatus.UNSPECIFIED}'");

        builder
           .Property(t => t.Document)
           .HasColumnType("jsonb")
           .HasDefaultValueSql("'{}'");

        builder
            .Property(t => t.Logs)
            .HasColumnType("jsonb")
            .HasDefaultValueSql("'[]'");

        builder.Property(p => p.Events)
            .HasColumnType("jsonb")
            .HasDefaultValueSql("'[]'");
    }
}