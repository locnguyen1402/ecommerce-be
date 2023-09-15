namespace ECommerce.Products.Api.Controllers;
public class ProductsController : BaseController
{
    private readonly IWorkRestClient _workRestClient;
    private readonly IBookRestClient _bookRestClient;
    public ProductsController(
            ILogger<ProductsController> logger,
            IMapper mapper,
            IWorkRestClient workRestClient,
            IBookRestClient bookRestClient
        ) : base(logger, mapper)
    {
        _workRestClient = workRestClient;
        _bookRestClient = bookRestClient;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<SearchResultItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts([FromQuery] ProductListQuery query)
    {
        var res = await _workRestClient.GetWorks(query);

        return Ok(res);
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
}