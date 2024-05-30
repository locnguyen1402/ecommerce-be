using MediatR;

namespace ECommerce.Shared.Common.Endpoint;

public interface IMinimalEndpoint
{
    void MapEndpoints(IMediator mediator);
}