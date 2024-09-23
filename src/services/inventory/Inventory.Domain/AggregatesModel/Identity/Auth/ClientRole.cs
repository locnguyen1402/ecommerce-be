using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel.Identity;

public class ClientRole(string roleName, string clientId) : AggregateRoot
{
    public string RoleName { get; private set; } = roleName;
    public string ClientId { get; private set; } = clientId;

}
