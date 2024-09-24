using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Infrastructure.Services;
using ECommerce.Inventory.Api.Customers.Specifications;
using ECommerce.Inventory.Api.Customers.Responses;

namespace ECommerce.Inventory.Api.Customers.Queries;

public class GetCustomersByAdminQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        IIdentityService identityService,
        PagingQuery pagingQuery,
        ICustomerRepository repository,
        CancellationToken cancellationToken
    ) =>
    {
        var customerId = identityService.CustomerId;

        if (customerId != null && customerId == Guid.Empty)
        {
            return Results.BadRequest("Invalid customer id");
        }

        var spec = new GetCustomersSpecification<CustomerResponse>(
            CustomerProjection.ToCustomerResponse()
            , keyword
            , pagingQuery
            );

        var items = await repository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
