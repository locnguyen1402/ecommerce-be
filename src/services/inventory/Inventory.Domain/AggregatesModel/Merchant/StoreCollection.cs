using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class StoreCollection(Guid storeId, string name, string slug, Guid categoryId) : Entity
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public Guid CategoryId { get; private set; } = categoryId;
    public virtual Category Category { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public Guid StoreId { get; private set; } = storeId;
    public virtual Store Store { get; private set; } = null!;
    public readonly List<StoreCollectionProduct> _storeCollectionProducts = [];
    public virtual IReadOnlyCollection<StoreCollectionProduct> StoreCollectionProducts => _storeCollectionProducts;

    public void Update(string name)
    {
        Name = name;
    }
}