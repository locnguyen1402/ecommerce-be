namespace ECommerce.Products.Api.Controllers;
public class ProductTagsController : BaseController
{
    private readonly ITagRepository _tagRepository;
    private readonly IMediator _mediator;
    public ProductTagsController(
            ILogger<ProductTagsController> logger,
            IMapper mapper,
            ITagRepository tagRepository,
            IMediator mediator
        ) : base(logger, mapper)
    {
        _tagRepository = tagRepository;
        _mediator = mediator;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(List<ProductTagOptionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProductTags()
    {
        var result = await _tagRepository.Query.ToListAsync();

        return Ok(_mapper.Map<List<Tag>, List<ProductTagOptionResponse>>(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Tag), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTag(CreateTagRequest request)
    {

        var result = await _mediator.Send(request);

        return Ok(_mapper.Map<Tag, ProductTagOptionResponse>(result));
    }
}