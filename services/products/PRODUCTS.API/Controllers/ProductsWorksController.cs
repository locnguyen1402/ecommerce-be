namespace ECommerce.Products.Api.Controllers;
public class ProductsWorksController : BaseController
{
    private readonly IWorkRestClient _workRestClient;
    public ProductsWorksController(
            ILogger<ProductsWorksController> logger,
            IMapper mapper,
            IWorkRestClient workRestClient
        ) : base(logger, mapper)
    {
        _workRestClient = workRestClient;
    }

    [HttpGet("{id}")]
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