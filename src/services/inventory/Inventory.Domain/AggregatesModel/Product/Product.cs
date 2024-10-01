using ECommerce.Shared.Libs.Extensions;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Product(string name, string slug, string? sku, string? description) : EntityWithDiscounts
{
    public string Slug { get; private set; } = slug;
    public string Code { get; private set; } = StringExtensions.ToGenerateRandomCode();
    // TODO: config unique following by business rule of warehouse
    public string Sku { get; private set; } = sku ?? string.Empty;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description ?? string.Empty;
    // public decimal Price => ProductVariants.Min(x => x.Price);
    private readonly List<ShopCollection> _shopCollections = [];
    public IReadOnlyCollection<ShopCollection> ShopCollections => _shopCollections;
    private readonly List<ShopCollectionProduct> _shopCollectionProducts = [];
    public IReadOnlyCollection<ShopCollectionProduct> ShopCollectionProducts => _shopCollectionProducts;
    private readonly HashSet<ProductVariant> _productVariants = [];
    public IReadOnlyCollection<ProductVariant> ProductVariants => _productVariants;
    private readonly List<ProductAttribute> _productAttributes = [];
    public IReadOnlyCollection<ProductAttribute> ProductAttributes => _productAttributes;
    private readonly List<ProductProductAttribute> _productProductAttributes = [];
    public IReadOnlyCollection<ProductProductAttribute> ProductProductAttributes => _productProductAttributes;
    public readonly List<Voucher> _vouchers = [];
    public virtual IReadOnlyCollection<Voucher> Vouchers => _vouchers;
    public readonly List<VoucherProduct> _voucherProducts = [];
    public virtual IReadOnlyCollection<VoucherProduct> VoucherProducts => _voucherProducts;
    public readonly List<OrderItem> _orderItems = [];
    public virtual IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    public readonly List<OrderPromotionItem> _orderPromotionItems = [];
    public virtual IReadOnlyCollection<OrderPromotionItem> OrderPromotionItems => _orderPromotionItems;
    public readonly List<OrderPromotionSubItem> _orderPromotionSubItems = [];
    public virtual IReadOnlyCollection<OrderPromotionSubItem> OrderPromotionSubItems => _orderPromotionSubItems;
    public readonly List<ProductPromotionItem> _productPromotionItems = [];
    public virtual IReadOnlyCollection<ProductPromotionItem> ProductPromotionItems => _productPromotionItems;

    public Guid MerchantId { get; private set; }
    public virtual Merchant Merchant { get; private set; } = null!;

    public void UpdateGeneralInfo(string name, string slug, string? sku, string? description)
    {
        Name = name;
        Slug = slug;
        Description = description ?? string.Empty;
        Sku = sku ?? string.Empty;
    }
    public void AddAttribute(ProductAttribute attribute)
    {
        if (_productAttributes.Any(x => x.Id == attribute.Id))
        {
            return;
        }

        _productAttributes.Add(attribute);
    }
    public void AddOrUpdateAttributes(IEnumerable<ProductAttribute> attributes)
    {
        _productAttributes.RemoveAll(x => !attributes.Any(a => a.Id == x.Id));

        foreach (var attribute in attributes)
        {
            if (!_productAttributes.Any(x => x.Id == attribute.Id))
            {
                _productAttributes.Add(attribute);
            }
        }
    }
    public void AddOrUpdateCollections(IEnumerable<ShopCollection> categories)
    {
        _shopCollections.RemoveAll(x => !categories.Any(a => a.Id == x.Id));

        foreach (var category in categories)
        {
            if (!_shopCollections.Any(x => x.Id == category.Id))
            {
                _shopCollections.Add(category);
            }
        }
    }
    public void AddVariant(int stock, decimal price, string? sku, List<ProductVariantAttributeValue> attributeValues)
    {
        var variant = new ProductVariant(stock, price);

        variant.UpdateGeneralInfo(sku);

        foreach (var value in attributeValues)
        {
            variant.AddOrUpdateAttributeValue(value);
        }

        _productVariants.Add(variant);
    }
    public void RemoveVariant(ProductVariant variant)
    {
        _productVariants.Remove(variant);
    }
    public void RemoveVariants(List<ProductVariant> variants)
    {
        foreach (var variant in variants)
        {
            _productVariants.Remove(variant);
        }
    }

    public void SetMerchant(Guid merchantId)
    {
        MerchantId = merchantId;
    }
}