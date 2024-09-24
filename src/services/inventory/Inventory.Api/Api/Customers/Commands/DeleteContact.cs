using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Customers.Requests;
using ECommerce.Inventory.Api.Customers.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Inventory.Api.Customers.Commands;

public class DeleteContactCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        [FromBody] DeleteContactRequest request,
        IValidator<DeleteContactRequest> validator,
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

        // Update the remaining contact to be the default contact
        var customerId = contact.CustomerId;

        var remainingContact = await repository.Query
            .Where(x => x.CustomerId == customerId && x.Id != contact.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (remainingContact != null)
        {
            remainingContact.SetContactDefault();
            repository.Update(remainingContact);
        }

        repository.Delete(contact);

        await repository.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    };
}