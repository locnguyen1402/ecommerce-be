namespace ECommerce.Products.Api.Controllers;
public class ProductsController : BaseController
{
    private readonly IWorkRestClient _workRestClient;
    public ProductsController(
            ILogger<ProductsWorksController> logger,
            IMapper mapper,
            IWorkRestClient workRestClient
        ) : base(logger, mapper)
    {
        _workRestClient = workRestClient;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Work>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts([FromQuery] ProductListQuery query)
    {
        var res = await _workRestClient.GetWorks(query);

        return Ok(res);
    }
}