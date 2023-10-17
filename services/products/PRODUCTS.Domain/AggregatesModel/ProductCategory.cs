namespace ECommerce.Products.Domain.AggregatesModels;
public class ProductCategory : Entity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public virtual ICollection<Product> Products { get; private set; }
    public ProductCategory(string name) : base()
    {
        Name = name;
        this.Products = new HashSet<Product>();
    }
    public void UpdateGeneralInfo(string name, string? description)
    {
        Name = name;
        Description = description;
    }
}