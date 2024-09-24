using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Customers.Requests;
using ECommerce.Inventory.Api.Customers.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Customers.Commands;

public class SetDefaultContactCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        SetDefaultContactRequest request,
        IValidator<SetDefaultContactRequest> validator,
        IContactRepository repository,
        CancellationToken cancellationToken
    ) =>
    {
        if (id != request.Id)
        {
            return Results.BadRequest();
        }

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var spec = new GetContactByIdSpecification(request.Id);

        Contact? contact = await repository
            .FindAsync(spec, cancellationToken);

        if (contact is null)
        {
            return Results.BadRequest($"Contact with id {request.Id} not found");
        }

        // remove default for other contacts
        var customerId = contact.CustomerId;

        var contactDefault = await repository.Query
            .Where(x => x.CustomerId == customerId && x.IsDefault == true && x.Id != contact.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (contactDefault != null)
        {
            contactDefault.RemoveContactDefault();
            repository.Update(contactDefault);
        }

        contact.SetContactDefault();
        repository.Update(contact);

        await repository.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    };
}