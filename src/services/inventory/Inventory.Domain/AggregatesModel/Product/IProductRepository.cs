using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public interface IProductRepository : IEntityRepository<Product>
{
}