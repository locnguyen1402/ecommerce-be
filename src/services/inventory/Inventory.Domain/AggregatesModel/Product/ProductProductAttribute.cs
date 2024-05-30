using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductProductAttribute(Guid productId, Guid productAttributeId)
{
    public Guid ProductId { get; init; } = productId;
    public Product Product { get; init; } = null!;
    public Guid ProductAttributeId { get; private set; } = productAttributeId;
    public ProductAttribute ProductAttribute { get; private set; } = null!;
}
