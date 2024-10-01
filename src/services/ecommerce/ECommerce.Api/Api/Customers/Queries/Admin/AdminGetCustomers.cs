using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Customers.Specifications;
using ECommerce.Api.Customers.Responses;
using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Customers.Queries;

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
