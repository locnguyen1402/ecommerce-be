using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Customers.Requests;
using ECommerce.Api.Services;
using ECommerce.Api.Customers.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Commands;

public class CreateContactCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateContactRequest request,
        IValidator<CreateContactRequest> validator,
        ICustomerService customerService,
        IContactRepository contactRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var customer = await customerService.GetCustomerInfoAsync(cancellationToken);

        var isDefault = request.IsDefault;

        var contact = new Contact();
        contact.UpdateInfo(
            request.AddressType,
            request.IsDefault,
            request.Name,
            request.ContactName,
            request.PhoneNumber,
            request.Notes
        );

        contact.AssignToCustomer(customer.Id);

        var address = AddressProjection.ToAddressInfo(request.AddressInfo);
        contact.AssignAddress(address);

        await contactRepository.AddAndSaveChangeAsync(contact, cancellationToken);

        return TypedResults.Ok(new IdResponse(contact.Id.ToString()));
    };
}