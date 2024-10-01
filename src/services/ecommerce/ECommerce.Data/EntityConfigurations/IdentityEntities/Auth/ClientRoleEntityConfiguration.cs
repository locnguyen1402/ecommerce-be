using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ECommerce.Shared.Common.Infrastructure.EntityConfigurations;
using ECommerce.Domain.AggregatesModel.Identity;

namespace ECommerce.Data.EntityConfigurations.IdentityEntities;

public class ClientRoleConfiguration : BaseEntityConfiguration<ClientRole>
{
    public override void Configure(EntityTypeBuilder<ClientRole> builder)
    {
        base.Configure(builder);

        // Primary key
        builder.HasKey(x => new { x.ClientId, x.RoleName });
    }
}
