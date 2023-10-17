namespace ECommerce.Products.Domain.AggregatesModels;
public class Category : Entity
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public virtual ICollection<Product> Products { get; private set; }
    public Category(string title) : base()
    {
        Title = title;
        this.Products = new HashSet<Product>();
    }
    public void UpdateGeneralInfo(string title, string? description)
    {
        Title = title;
        Description = description;
    }
}