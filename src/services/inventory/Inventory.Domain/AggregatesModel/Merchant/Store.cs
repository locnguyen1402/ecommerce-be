using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Store(string name, string slug, Guid merchantId) : Entity
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public string? StoreNumber { get; private set; }
    public string? Description { get; private set; }
    public string? PhoneNumber { get; private set; }
    public bool IsActive { get; private set; }

    // TODO: Using value object for address later
    public string? StoreAddress { get; private set; }
    public Guid MerchantId { get; private set; } = merchantId;
    public virtual Merchant Merchant { get; private set; } = null!;

    public void Update(string name, string? description, string? phoneNumber)
    {
        Name = name;
        Description = description;
        PhoneNumber = phoneNumber;
    }
}