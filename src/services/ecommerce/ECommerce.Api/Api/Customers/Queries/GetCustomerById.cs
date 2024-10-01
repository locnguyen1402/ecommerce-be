using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Customers.Specifications;
using ECommerce.Api.Customers.Responses;

namespace ECommerce.Api.Customers.Queries;

public class GetCustomerByIdQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        ICustomerRepository customerRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetCustomerByIdSpecification<CustomerResponse>(id, CustomerProjection.ToCustomerResponse());

        var customer = await customerRepository.FindAsync(spec, cancellationToken);

        if (customer is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(customer);
    };
}
