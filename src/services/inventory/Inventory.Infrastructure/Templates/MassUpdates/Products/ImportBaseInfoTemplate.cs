using ECommerce.Shared.Common.DocumentProcessing;

namespace ECommerce.Inventory.Infrastructure.Templates.MassUpdates.Products;

/// <inheritdoc />
public record ImportBaseInfoTemplate
{
    /// <inheritdoc />
    [Column("SKU")]
    public string Sku { get; set; } = string.Empty;

    /// <inheritdoc />
    [Column("PRODUCT_NAME")]
    public string ProductName { get; set; } = string.Empty;

    /// <inheritdoc />
    [Column("PRODUCT_CODE")]
    public string ProductCode { get; set; } = string.Empty;

    /// <inheritdoc />
    [Column("DESCRIPTION")]
    public string Description { get; set; } = string.Empty;

    public ImportBaseInfoTemplate(string sku, string productName, string description, string productCode)
    {
        Sku = sku;
        ProductName = productName;
        Description = description;
        ProductCode = productCode;
    }
}

