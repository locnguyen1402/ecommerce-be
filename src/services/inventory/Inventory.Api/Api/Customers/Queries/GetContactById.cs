using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Customers.Specifications;
using ECommerce.Inventory.Api.Customers.Responses;

namespace ECommerce.Inventory.Api.Customers.Queries;

public class GetContactByIdQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        IContactRepository contactRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetContactByIdSpecification<ContactResponse>(id, ContactProjection.ToContactResponse());

        var contact = await contactRepository.FindAsync(spec, cancellationToken);

        if (contact is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(contact);
    };
}
