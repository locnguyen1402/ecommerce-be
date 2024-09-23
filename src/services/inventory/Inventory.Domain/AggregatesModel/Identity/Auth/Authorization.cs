using ECommerce.Shared.Common.Infrastructure.Data;
using OpenIddict.EntityFrameworkCore.Models;

namespace ECommerce.Inventory.Domain.AggregatesModel.Identity;

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
public sealed class Authorization : OpenIddictEntityFrameworkCoreAuthorization<Guid, Application, Token>, IEntity<Guid>
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
{
}
