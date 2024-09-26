using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Customers.Requests;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Customers.Commands;

public class UpdateCustomerCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateCustomerRequest request,
        IValidator<UpdateCustomerRequest> validator,
        ICustomerRepository customerRepository,
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

        var customer = await customerRepository.Query
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (customer == null)
        {
            return Results.NotFound("Customer not found");
        }

        customer.UpdateName(request.FirstName, request.LastName);
        customer.UpdateGeneralInfo(
            request.BirthDate
            , request.Gender
            , request.Email
            , request.PhoneNumber
        );

        await customerRepository.UpdateAndSaveChangeAsync(customer, cancellationToken);

        return TypedResults.NoContent();
    };
}