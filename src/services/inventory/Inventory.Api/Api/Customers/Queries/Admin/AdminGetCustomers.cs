using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Customers.Specifications;
using ECommerce.Inventory.Api.Customers.Responses;
using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Customers.Queries;

public class AdminGetCustomersQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        PagingQuery pagingQuery,
        ICustomerRepository repository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetCustomersSpecification<CustomerResponse>(
            CustomerProjection.ToCustomerResponse()
            , keyword
            , pagingQuery
            );

        var items = await repository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
