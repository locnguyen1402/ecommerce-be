using ECommerce.Shared.Common;

namespace ECommerce.Services.Product;

public class Product : Entity
{
    public string Name { get; set; }

    public Product(string name) : base()
    {
        Name = name;
    }
}