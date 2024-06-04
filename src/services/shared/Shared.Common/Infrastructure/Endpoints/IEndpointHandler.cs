namespace ECommerce.Shared.Common.Infrastructure.Endpoint;

public interface IEndpointHandler
{
    Delegate Handle { get; }
}