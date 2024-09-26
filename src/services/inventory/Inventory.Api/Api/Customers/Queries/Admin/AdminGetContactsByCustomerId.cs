using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Customers.Specifications;
using ECommerce.Inventory.Api.Customers.Responses;

namespace ECommerce.Inventory.Api.Customers.Queries;

public class AdminGetContactsByCustomerIdQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        PagingQuery pagingQuery,
        IContactRepository repository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetContactsByCustomerIdSpecification<ContactResponse>(
            ContactProjection.ToContactResponse()
            , id
            , pagingQuery
            );

        var items = await repository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
