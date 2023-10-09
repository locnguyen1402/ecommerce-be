

namespace ECommerce.Products.Api.Controllers;
public class ProductsController : BaseController
{
    private readonly IWorkRestClient _workRestClient;
    private readonly IBookRestClient _bookRestClient;
    private readonly IProductRepository _productRepository;
    public ProductsController(
            ILogger<ProductsController> logger,
            IMapper mapper,
            IWorkRestClient workRestClient,
            IBookRestClient bookRestClient,
            IProductRepository productRepository
        ) : base(logger, mapper)
    {
        _workRestClient = workRestClient;
        _bookRestClient = bookRestClient;
        _productRepository = productRepository;
    }

    [HttpGet()]
    public async Task<IActionResult> GetProducts([FromQuery] ProductListQuery queryRes)
    {
        var query = _productRepository.Query.Include(p => p.ProductCategory).OrderBy(p => p.CreatedAt).AsQueryable();

        if (!queryRes.Keyword.IsNullOrEmpty())
        {
            query = query.Where(p => p.Name.Contains(queryRes.Keyword!));
        }

        var result = await PaginatedList<Product>.CreateFromQuery(query, queryRes.Page, queryRes.PageSize);

        result.ExposeHeader();

        return Ok(result.Items);
    }

    [HttpGet("works")]
    [ProducesResponseType(typeof(List<SearchResultItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductWorks([FromQuery] ProductWorkListQuery query)
    {
        var res = await _workRestClient.GetWorks(query);

        PaginatedList<SearchResultItem>.AttachToHeader(res.PaginationData);

        return Ok(res.Items);
    }

    [HttpGet("trending")]
    [ProducesResponseType(typeof(List<SearchResultItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTrendingProducts([FromQuery] TrendingProductListQuery query)
    {
        var res = await _workRestClient.GetTrendingWorks(query);

        PaginatedList<SearchResultItem>.AttachToHeader(res.PaginationData);

        return Ok(res.Items);
    }

    [HttpGet("books/{id}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductBookDetail(string id)
    {
        var res = await _bookRestClient.GetBookDetail(id);

        if (res is null)
        {
            return NotFound();
        }

        return Ok(res);
    }

    [HttpGet("works/{id}")]
    [ProducesResponseType(typeof(Work), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductWorkDetail(string id)
    {
        var res = await _workRestClient.GetWorkDetail(id);

        if (res is null)
        {
            return NotFound();
        }

        return Ok(res);
    }

    [HttpGet("works/{id}/ratings")]
    [ProducesResponseType(typeof(WorkRatings), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWorkRatings(string id)
    {
        var res = await _workRestClient.GetWorkRatings(id);

        if (res is null)
        {
            return NotFound();
        }

        return Ok(res);
    }

    [HttpGet("works/{id}/books")]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductInWorkBooks(string id, [FromQuery] PaginationQuery query)
    {
        var res = await _workRestClient.GetBooksInWork(id, query);

        PaginatedList<Book>.AttachToHeader(res.PaginationData);

        return Ok(res.Items);
    }

    [HttpGet("works/{id}/books/first")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFirstInWorkBook(string id)
    {
        var res = await _workRestClient.GetFirstInWorkBook(id);

        if (res is null)
        {
            return NotFound();
        }

        return Ok(res);
    }
}