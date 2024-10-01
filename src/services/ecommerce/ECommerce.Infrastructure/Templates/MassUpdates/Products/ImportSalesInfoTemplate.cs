using System.Text.Json.Serialization;
using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.DocumentProcessing;

namespace ECommerce.Infrastructure.Templates.MassUpdates.Products;

/// <inheritdoc />
public record ImportSalesInfoTemplate
{
    /// <inheritdoc />
    [Column("SKU")]
    public string Sku { get; set; } = string.Empty;

    /// <inheritdoc />
    [Column("PRODUCT_CODE")]
    public string ProductCode { get; set; } = string.Empty;

    /// <inheritdoc />
    [Column("PRODUCT_NAME")]
    public string ProductName { get; set; } = string.Empty;

    /// <inheritdoc />
    [Column("PRODUCT_VARIANT_CODE")]
    public string ProductVariantCode { get; set; } = string.Empty;

    /// <inheritdoc />
    [Column("PRODUCT_VARIANT_ATTRIBUTE_NAME")]
    public string ProductVariantAttributeNames { get; set; } = string.Empty;

    /// <inheritdoc />
    [Column("QUANTITY")]
    public string Quantity { get; set; } = string.Empty;

    /// <inheritdoc />
    [Column("UNIT_PRICE")]
    public string UnitPrice { get; set; } = string.Empty;

    [JsonIgnore]
    private List<ProductVariantAttributeValue> ProductVariantAttributeValues { get; set; } = [];

    public ImportSalesInfoTemplate(
        string sku
        , string unitPrice
        , string quantity
        , string productCode
        , string productName
        , List<ProductVariantAttributeValue> productVariantAttributeValues
    )
    {
        Sku = sku;
        UnitPrice = unitPrice;
        Quantity = quantity;
        ProductCode = productCode;
        ProductName = productName;
        ProductVariantAttributeValues = productVariantAttributeValues;

        ProductVariantAttributeNames = string.Join(", ", productVariantAttributeValues.Select(x => x.Value));
    }
}
