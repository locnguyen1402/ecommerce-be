using MediatR;

namespace ECommerce.Shared.Common.Infrastructure.Endpoint;

public interface IMinimalEndpoint
{
    void MapEndpoints(IMediator mediator);
}