using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Customers.Requests;
using ECommerce.Api.Services;

namespace ECommerce.Api.Customers.Commands;

public class CreateCustomerCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateCustomerRequest request,
        IValidator<CreateCustomerRequest> validator,
        ICustomerRepository customerRepository,
        IAccountService accountService,
        ILogger<CreateCustomerCommandHandler> logger,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var phoneNumber = request.PhoneNumber;

        var existedCustomer = await customerRepository.FindAsync(x => x.UserName != null && x.UserName == phoneNumber, cancellationToken);

        if (existedCustomer != null)
        {
            return TypedResults.Ok(new IdResponse(existedCustomer.Id.ToString()));
        }

        var customer = new Customer(
            phoneNumber
            , null
            , null
            , null
            , null
            , phoneNumber
        );

        customer.SetUserName(phoneNumber);

        try
        {
            var userId = await accountService.RegisterAccountAsync(phoneNumber, cancellationToken);
            customer.SetRefUser(userId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Register account failed");
            return TypedResults.BadRequest("Register account failed");
        }

        await customerRepository.AddAndSaveChangeAsync(customer, cancellationToken);

        return TypedResults.Ok(new IdResponse(customer.Id.ToString()));
    };
}