using AutoMapper;
using ECommerce.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Product;

public class ProductController : BaseController
{
    public ProductController(
            ILogger<ProductController> logger,
            IMapper mapper
        ) : base(logger, mapper)
    {
    }

    [HttpGet]
    public string GetProducts()
    {
        return "test ne";
    }
}