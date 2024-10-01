using Microsoft.AspNetCore.Identity;

using ECommerce.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Api.Services;

public class AccountService(
    //IServiceProvider serviceProvider,
    UserManager<User> userManager,
    //IOptions<AppSettings> appSettings,
    //ICustomerRepository customerRepository,
    ILogger<AccountService> logger
) : IAccountService
{
    public async Task<Guid> RegisterAccountAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Register new account with phoneNumber {phoneNumber}", phoneNumber);

        // TODO: Implement method to publish event
        // var publisher = serviceProvider.GetRequiredService<IPublishEndpoint>();

        var userExists = await userManager.FindByNameAsync(phoneNumber);

        if (userExists != null)
        {
            if (userExists.PhoneNumberConfirmed)
            {
                logger.LogError("Account already exists");

                throw new Exception("Account already exists");
            }
            else
            {
                var resendCode = await userManager.GenerateChangePhoneNumberTokenAsync(userExists, phoneNumber);

                logger.LogInformation("Generate with phoneNumber {phoneNumber} with resend code ({code})", phoneNumber, resendCode);

                // TODO: Implement method to send sms

                return userExists.Id;
            }
        }

        User user = new(phoneNumber);

        user.SetPhoneNumber(phoneNumber);

        user.UpdatePersonalInfo(
            phoneNumber ?? string.Empty,
            string.Empty,
            null,
            Gender.UNSPECIFIED
        );

        var result = await userManager.CreateAsync(user, phoneNumber!);

        if (!result.Succeeded)
        {
            logger.LogError("Error register user with phoneNumber {phoneNumber}", phoneNumber);

            throw new Exception("Error Register Account");
        }

        logger.LogInformation("User created a new account with password.");

        var code = await userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber!);

        logger.LogInformation("Generate with phoneNumber {phoneNumber} with code ({code})", phoneNumber, code);

        // TODO: Implement method to send sms

        return user.Id;
    }

    public async Task<Guid> RegisterAccountConfirmOtpAsync(string phoneNumber, string code, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start to register new account confirm otp");

        var user = await userManager.FindByNameAsync(phoneNumber);
        if (user == null)
        {
            logger.LogError("User with phoneNumber {phoneNumber} not found", phoneNumber);
            throw new Exception("User not found");
        }

        var verifyToken = await userManager.ChangePhoneNumberAsync(user, user.PhoneNumber!, code);

        if (!verifyToken.Succeeded)
            throw new Exception("Invalid code");

        user.ConfirmPhoneNumber();

        await userManager.AddToRoleAsync(user, "Customer");

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            logger.LogError("Error confirm otp with phoneNumber ({phoneNumber}) with exception: {err}", phoneNumber, result.Errors);
            throw new Exception($"Error confirm otp with phoneNumber {phoneNumber}");
        }

        logger.LogInformation("End to register new account confirm otp");

        return user.Id;
    }
}