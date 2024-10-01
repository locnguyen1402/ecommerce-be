using OpenIddict.EntityFrameworkCore.Models;

using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Domain.AggregatesModel.Identity;

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
public sealed class Token : OpenIddictEntityFrameworkCoreToken<Guid, Application, Authorization>, IEntity<Guid>
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
{
}
