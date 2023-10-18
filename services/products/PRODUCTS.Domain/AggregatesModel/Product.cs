namespace ECommerce.Products.Domain.AggregatesModels;
public class Product : Entity
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public int Price { get; private set; }
    public List<Tag> Tags { get; set; } = new();
    public List<ProductTag> ProductTags { get; set; } = new();
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public Product(string title, string? description) : base()
    {
        Title = title;
        Description = description;
    }

    public void UpdateGeneralInfo(string title, string? description)
    {
        Title = title;
        Description = description;
    }

    public void ChangePrice(int price)
    {
        Price = price;
    }

    public void AssignToCategory(Guid categoryId)
    {
        CategoryId = categoryId;
    }
    public void AddTags(List<Tag> tags)
    {
        Tags.AddRange(tags);
        Tags = Tags.DistinctBy(t => t.Id).ToList();
    }
}