namespace ECommerce.Shared.Common.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> _logger;
    protected readonly IMapper _mapper;
    protected BaseController(ILogger<BaseController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }
}