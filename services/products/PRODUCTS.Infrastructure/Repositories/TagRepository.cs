namespace ECommerce.Products.Infrastructure.Repositories;
public class TagRepository : EntityRepository<Tag>, ITagRepository
{
    public TagRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}