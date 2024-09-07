using ECommerce.Inventory.Data;
using ECommerce.Inventory.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ECommerce.Inventory.DbMigrator.Seeds
{
    public sealed class Seed_Release_001
    {
        public static async Task SeedAsync(
            IServiceProvider services,
            InventoryDbContext dbContext,
            ILogger<InventoryDbContext> logger)
        {
            await InitAttributes(services);
            await InitAttributeValues(services);
        }

        private static async Task InitAttributes(IServiceProvider serviceProvider)
        {
            var attributes = new ProductAttribute[] {
                new("color"),
                new("size"),
                new("material"),
                new("weight")
            };

            await InitAttributes(serviceProvider, attributes);
        }

        private static async Task InitAttributes(IServiceProvider serviceProvider, params ProductAttribute[] attributes)
        {
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

            foreach (var attribute in attributes)
            {
                if (await dbContext.ProductAttributes.AnyAsync(t => t.Name == attribute.Name))
                    continue;

                var newAttribute = new ProductAttribute(attribute.Name);
                newAttribute.SetPredefined();

                dbContext.ProductAttributes.Add(newAttribute);

                var result = await dbContext.SaveChangesAsync();
            }
        }

        private static async Task InitAttributeValues(IServiceProvider serviceProvider)
        {
            var values = new AttributeValueInfo[] {
                // Values for 'color' attribute
                new("Red", "color"),
                new("Blue", "color"),
                new("Green", "color"),
                new("Black", "color"),
                new("White", "color"),

                // Values for 'size' attribute
                new("Small", "size"),
                new("Medium", "size"),
                new("Large", "size"),
                new("X-Large", "size"),

                // Values for 'material' attribute
                new("Cotton", "material"),
                new("Polyester", "material"),
                new("Leather", "material"),
                new("Silk", "material"),

                // Values for 'weight' attribute
                new("Light", "weight"),
                new("Medium", "weight"),
                new("Heavy", "weight"),
                new("Ultra-Light", "weight"),
            };
            await InitAttributeValues(serviceProvider, values);
        }

        private static async Task InitAttributeValues(IServiceProvider serviceProvider, params AttributeValueInfo[] values)
        {
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

            var attributeNames = values.Select(t => t.AttributeName).Distinct();

            var attributes = await dbContext.ProductAttributes.Where(t => attributeNames.Contains(t.Name)).ToListAsync();

            foreach (var value in values)
            {
                var attribute = attributes.FirstOrDefault(t => t.Name == value.AttributeName);

                if (await dbContext.AttributeValues.AnyAsync(t => t.Value == value.Value &&
                        attribute != null && t.ProductAttributeId == attribute.Id))
                    continue;

                var newValue = new AttributeValue(value.Value);

                if (attribute != null)
                    newValue.SetAttribute(attribute.Id);

                dbContext.AttributeValues.Add(newValue);

                await dbContext.SaveChangesAsync();
            }
        }

        internal class AttributeValueInfo(string value, string attributeName)
        {
            public string Value { get; set; } = value;
            public string AttributeName { get; set; } = attributeName;
        }
    }
}
