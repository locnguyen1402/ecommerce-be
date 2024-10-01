using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Customers.Requests;
using ECommerce.Api.Customers.Specifications;
using ECommerce.Api.Services;

namespace ECommerce.Api.Customers.Commands;

public class CreateCustomerConfirmOtpCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateCustomerConfirmOtpRequest request,
        IValidator<CreateCustomerConfirmOtpRequest> validator,
        ICustomerRepository customerRepository,
        IAccountService accountService,
        ILogger<CreateCustomerConfirmOtpCommandHandler> logger,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var (phoneNumber, code) = (request.PhoneNumber, request.Code);

        var spec = new GetCustomerByUserNameSpecification(phoneNumber);

        var customer = await customerRepository.FindAsync(spec, cancellationToken);

        if (customer == null)
        {
            return Results.NotFound($"Customer with {phoneNumber} not found");
        }

        try
        {
            await accountService.RegisterAccountConfirmOtpAsync(phoneNumber, code, cancellationToken);

            customer.UpdatePhoneNumber(phoneNumber);
            await customerRepository.UpdateAndSaveChangeAsync(customer, cancellationToken);

            logger.LogInformation("Confirm otp with phoneNumber: {phoneNumber} successfully", phoneNumber);

            return TypedResults.Ok(new IdResponse(customer.Id.ToString()));
        }
        catch (Exception ex)
        {
            logger.LogError("Confirm otp from identity has error: {exception}", ex);
            throw new Exception("Failed to confirm otp");
        }
    };
}