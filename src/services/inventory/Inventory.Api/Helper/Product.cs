using ECommerce.Inventory.Api.Products.Requests;
using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Utilities;

public static class ProductUtils
{
    public static void UpdateVariantsInProduct(Product product, List<UpdateProductVariantRequest> variants)
    {
        var existingVariantIds = variants.Where(x => x.Id != null).Select(x => x.Id).ToList();

        var variantsToRemove = product.ProductVariants.Where(x => !existingVariantIds.Contains(x.Id)).ToList();

        product.RemoveVariants(variantsToRemove);

        foreach (var variant in variants)
        {
            UpdateVariantInProduct(product, variant);
        }
    }

    public static void UpdateVariantInProduct(Product product, UpdateProductVariantRequest variant)
    {
        if (variant.Id == null)
        {
            var attributeValues = new List<ProductVariantAttributeValue>();

            foreach (var value in variant.Values)
            {
                attributeValues.Add(new(value.ProductAttributeId, value.Value, value.AttributeValueId));
            }

            product.AddVariant(variant.Stock, variant.Price, attributeValues);
        }
        else
        {
            var existingVariant = product.ProductVariants.FirstOrDefault(x => x.Id == variant.Id);

            if (existingVariant == null)
            {
                throw new InvalidOperationException($"{nameof(UpdateVariantInProduct)}: Variant not found");
            }

            existingVariant.UpdateStock(variant.Stock);
            existingVariant.UpdatePrice(variant.Price);

            var existingValues = existingVariant.ProductVariantAttributeValues.ToList();
            var valuesToRemove = existingValues.Where(x => !variant.Values.Any(v => v.ProductAttributeId == x.ProductAttributeId)).ToList();

            foreach (var value in valuesToRemove)
            {
                existingVariant.RemoveAttributeValue(value.ProductAttributeId);
            }

            if (variant.Values.Count != 0)
            {
                foreach (var value in variant.Values)
                {
                    existingVariant.AddOrUpdateAttributeValue(value.ProductAttributeId, value.Value, value.AttributeValueId);
                }
            }
        }
    }
}