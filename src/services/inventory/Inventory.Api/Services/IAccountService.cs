namespace ECommerce.Inventory.Api.Services;

public interface IAccountService
{
    Task<Guid> RegisterAccountAsync(
        string phoneNumber,
        CancellationToken cancellationToken
    );

    Task<Guid> RegisterAccountConfirmOtpAsync(
        string phoneNumber,
        string code,
        CancellationToken cancellationToken
    );
}