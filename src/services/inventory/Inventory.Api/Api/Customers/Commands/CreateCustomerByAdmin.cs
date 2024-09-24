using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Customers.Requests;

namespace ECommerce.Inventory.Api.Customers.Commands;

public class CreateCustomerByAdminCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateCustomerByAdminRequest request,
        IValidator<CreateCustomerByAdminRequest> validator,
        ICustomerRepository customerRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var fullName = request.FullName;
        var lastName = string.Empty;

        var customer = new Customer(
                fullName
                , lastName
                , request.PhoneNumber
                , request.BirthDate
                , request.Gender
                , request.Email
                , request.PhoneNumber
            );

        await customerRepository.AddAndSaveChangeAsync(customer, cancellationToken);

        return TypedResults.Ok(new IdResponse(customer.Id.ToString()));
    };
}