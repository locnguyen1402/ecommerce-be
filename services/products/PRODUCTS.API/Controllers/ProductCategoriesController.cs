namespace ECommerce.Products.Api.Controllers;
public class ProductCategoriesController : BaseController
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    public ProductCategoriesController(
            ILogger<ProductCategoriesController> logger,
            IMapper mapper,
            IProductCategoryRepository productCategoryRepository
        ) : base(logger, mapper)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductCategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductCategories([FromQuery] ProductCategoryListQuery queryRes)
    {
        var query = _productCategoryRepository.Query.OrderBy(p => p.CreatedAt).AsQueryable();

        if (!queryRes.Keyword.IsNullOrEmpty())
        {
            query = query.Where(p => p.Name.Contains(queryRes.Keyword!));
        }

        var result = await PaginatedList<ProductCategory>.CreateFromQuery(query, queryRes.Page, queryRes.PageSize);

        result.ExposeHeader();

        return Ok(_mapper.Map<List<ProductCategory>, List<ProductCategoryResponse>>(result.Items));
    }
}