using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Shared.Common.Infrastructure.Services;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Customers.Specifications;

namespace ECommerce.Api.Customers.Commands;

public class DeleteContactCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        IContactRepository repository,
        IIdentityService identityService,
        CancellationToken cancellationToken
    ) =>
    {
        var customerId = identityService.CustomerId;
        if (customerId == null || customerId == Guid.Empty)
        {
            return Results.BadRequest("Customer not found");
        }

        var spec = new GetContactByIdSpecification(id, (Guid)customerId);

        Contact? contact = await repository
            .FindAsync(spec, cancellationToken);

        if (contact is null)
        {
            return Results.BadRequest($"Contact with id {id} not found");
        }

        // if (contact.IsDefault)
        // {
        //     var customerId = contact.CustomerId;
        //     var remainingContact = await repository.Query
        //         .Where(x => x.CustomerId == customerId && x.Id != contact.Id)
        //         .FirstOrDefaultAsync(cancellationToken);
        //     if (remainingContact != null)
        //     {
        //         remainingContact.SetContactDefault();
        //         repository.Update(remainingContact);
        //     }
        // }

        repository.Delete(contact);

        await repository.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    };
}