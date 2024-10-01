using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Products.Specifications;
using ECommerce.Api.Products.Responses;

namespace ECommerce.Api.Products.Queries;

public class GetProductAttributesQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        PagingQuery pagingQuery,
        IProductAttributeRepository productAttributeRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetProductAttributesSpecification<ProductAttributeResponse>(
            ProductAttributeProjection.ToProductAttributeResponse()
            , keyword
            , pagingQuery
            );

        var items = await productAttributeRepository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
