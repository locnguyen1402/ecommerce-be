using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Customers.Requests;
using Microsoft.EntityFrameworkCore;
using ECommerce.Inventory.Api.Customers.Specifications;
using ECommerce.Inventory.Api.Customers.Responses;

namespace ECommerce.Inventory.Api.Customers.Commands;

public class UpdateContactCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateContactRequest request,
        IValidator<UpdateContactRequest> validator,
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

        var isDefault = request.IsDefault;

        if (isDefault)
        {
            var defaultContact = await repository.Query
                .Where(x => x.CustomerId == contact.CustomerId && x.IsDefault == isDefault)
                .FirstOrDefaultAsync(cancellationToken);

            if (defaultContact != null)
            {
                defaultContact.RemoveContactDefault();
                repository.Update(defaultContact);
            }
        }

        contact.UpdateInfo(
            request.AddressType,
            request.IsDefault,
            request.Name,
            request.ContactName,
            request.PhoneNumber,
            request.Notes
        );

        var address = AddressProjection.ToAddressInfo(request.AddressInfo);
        contact.AssignAddress(address);

        await repository.UpdateAndSaveChangeAsync(contact, cancellationToken);

        return TypedResults.NoContent();
    };
}