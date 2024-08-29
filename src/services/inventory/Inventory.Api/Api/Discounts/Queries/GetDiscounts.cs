using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Discounts.Specifications;
using ECommerce.Inventory.Api.Discounts.Responses;

namespace ECommerce.Inventory.Api.Discounts.Queries;

public class GetDiscountsQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        PagingQuery pagingQuery,
        IDiscountRepository discountRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetDiscountsSpecification<DiscountResponse>(
            DiscountProjection.ToDiscountResponse()
            , keyword
            , pagingQuery
            );

        var items = await discountRepository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
