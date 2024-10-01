using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Customers.Specifications;
using ECommerce.Api.Customers.Responses;
using ECommerce.Domain.AggregatesModel;

using ECommerce.Shared.Common.Infrastructure.Services;

namespace ECommerce.Api.Customers.Queries;

public class GetContactsQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        IIdentityService identityService,
        PagingQuery pagingQuery,
        IContactRepository repository,
        CancellationToken cancellationToken
    ) =>
    {
        var customerId = identityService.CustomerId;

        if (customerId != null && customerId == Guid.Empty)
        {
            return Results.BadRequest("Invalid customer id");
        }

        var spec = new GetContactsByCustomerIdSpecification<ContactResponse>(
            ContactProjection.ToContactResponse()
            , customerId!.Value
            , pagingQuery
            );

        var items = await repository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
