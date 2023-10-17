namespace ECommerce.Products.Domain.AggregatesModels;
public class Product : Entity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int Price { get; private set; }
    public string Tags { get; private set; } = string.Empty;
    public Guid ProductCategoryId { get; private set; }
    public ProductCategory ProductCategory { get; private set; } = null!;
    public Product(string name, string? description) : base()
    {
        Name = name;
        Description = description;
    }

    public void UpdateGeneralInfo(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public void ChangePrice(int price)
    {
        Price = price;
    }

    public void AssignToCategory(Guid categoryId)
    {
        ProductCategoryId = categoryId;
    }

    public void AddTag(string tag)
    {
        if (!tag.IsNullOrEmpty())
        {
            var tags = BuildTagList(this.Tags);

            tags.Add(tag);

            this.Tags = StringifyTagList(tags);
        }
    }
    public void AddTags(List<string> tags)
    {
        if (!tags.IsNullOrEmpty())
        {
            this.Tags = StringifyTagList(Enumerable.Union(BuildTagList(this.Tags), tags).ToList());
        }
    }
    public static List<string> BuildTagList(string tags)
    {
        var tagList = new List<string>();

        if (!tags.IsNullOrEmpty())
        {
            tagList = tags.Split("/").ToList();
        }

        return tagList;
    }
    public static string StringifyTagList(List<string> tags)
    {
        return string.Join("/", tags);
    }
}