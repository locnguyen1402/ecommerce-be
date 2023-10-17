namespace ECommerce.Products.Api.Controllers;
public class ProductCategoriesController : BaseController
{
    private readonly ICategoryRepository _productCategoryRepository;
    private readonly IMediator _mediator;
    public ProductCategoriesController(
            ILogger<ProductCategoriesController> logger,
            IMapper mapper,
            ICategoryRepository productCategoryRepository,
            IMediator mediator
        ) : base(logger, mapper)
    {
        _productCategoryRepository = productCategoryRepository;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductCategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductCategories([FromQuery] ProductCategoryListQuery queryRes)
    {
        var query = _productCategoryRepository.Query.OrderBy(p => p.CreatedAt).AsQueryable();

        if (!queryRes.Keyword.IsNullOrEmpty())
        {
            query = query.Where(p => p.Title.Contains(queryRes.Keyword!));
        }

        var result = await PaginatedList<Category>.CreateFromQuery(query, queryRes.Page, queryRes.PageSize);

        result.ExposeHeader();

        return Ok(_mapper.Map<List<Category>, List<ProductCategoryResponse>>(result.Items));
    }
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProductCategory(Guid id, [FromBody] UpdateProductCategoryRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(request);

        return NoContent();
    }
}