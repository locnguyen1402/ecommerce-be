using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Customers.Requests;

namespace ECommerce.Inventory.Api.Customers.Commands;

public class AdminCreateCustomerCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        AdminCreateCustomerRequest request,
        IValidator<AdminCreateCustomerRequest> validator,
        ICustomerRepository customerRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (!string.IsNullOrEmpty(request.PhoneNumber)
            && await customerRepository.AnyAsync(x => x.PhoneNumber == request.PhoneNumber, cancellationToken))
        {
            return Results.Conflict("Phone number already exists");
        }

        var customer = new Customer(
            request.FirstName
            , request.LastName
            , request.BirthDate
            , request.Gender
            , request.Email
            , request.PhoneNumber
        );

        await customerRepository.AddAndSaveChangeAsync(customer, cancellationToken);

        return TypedResults.Ok(new IdResponse(customer.Id.ToString()));
    };
}