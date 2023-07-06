using ECommerce.Shared.Common;

namespace ECommerce.Services.Product;

public class ProductListQuery : PaginationQuery
{
    public string? keyword { get; set; }
}