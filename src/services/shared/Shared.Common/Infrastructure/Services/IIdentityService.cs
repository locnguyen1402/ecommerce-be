using ECommerce.Shared.Common.Enums;

namespace ECommerce.Shared.Common.Infrastructure.Services;

public interface IIdentityService
{
    Guid? MerchantId { get; }
    Guid? CustomerId { get; }
    Guid UserId { get; }
    string FirstName { get; }
    string LastName { get; }

    string FullName { get; }
    string UserName { get; }

    string Email { get; }
    bool EmailConfirmed { get; }

    string PhoneNumber { get; }
    bool PhoneNumberConfirmed { get; }

    DateOnly? BirthDate { get; }

    Gender Gender { get; }

    IReadOnlyCollection<string> Roles { get; }
    IReadOnlyCollection<string> Permissions { get; }

    string AccessToken { get; }

    bool TryParseClaim(string type, out string value);
    bool HasClaim(string type, string value);

    bool IsInRole(string role);
    bool HasPermission(string permission);
    TService? GetService<TService>();
}
