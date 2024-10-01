using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Customers.Specifications;

namespace ECommerce.Api.Customers.Commands;

public class AdminDeleteContactCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        Guid contactId,
        IContactRepository repository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetContactByIdSpecification(contactId, id);

        Contact? contact = await repository
            .FindAsync(spec, cancellationToken);

        if (contact is null)
        {
            return Results.BadRequest($"Contact with id {contactId} not found");
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