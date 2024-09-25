using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Customers.Commands;
using ECommerce.Inventory.Api.Customers.Queries;

namespace ECommerce.Inventory.Api.Endpoints;

public class CustomerEndpoints(WebApplication app) : MinimalEndpoint(app, "/inventory")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Customers");
    }
    public override void MapEndpoints()
    {
        var clientGroup = Builder.MapGroup("/customers");
        var adminGroup = Builder.MapGroup("/admin/customers");

        // Admin
        adminGroup.MapGet<AdminGetCustomersQueryHandler>("/");
        adminGroup.MapPost<AdminCreateCustomerCommandHandler>("/");

        adminGroup.MapPut<UpdateCustomerCommandHandler>("/{id:Guid}");

        adminGroup.MapGet<AdminGetContactsByCustomerIdQueryHandler>("/{id:Guid}/contacts");
        adminGroup.MapPost<AdminCreateContactCommandHandler>("/{id:Guid}/contacts");

        // Client
        clientGroup.MapGet<GetCustomerByIdQueryHandler>("/{id:Guid}");
        clientGroup.MapPut<UpdateCustomerCommandHandler>("/{id:Guid}");

        clientGroup.MapPost<CreateCustomerCommandHandler>("/");
        clientGroup.MapPost<CreateCustomerConfirmOtpCommandHandler>("/confirm-otp");

        clientGroup.MapGet<GetContactsQueryHandler>("/contacts");
        clientGroup.MapPost<CreateContactCommandHandler>("/contacts");
        clientGroup.MapPut<UpdateContactCommandHandler>("/contacts/{id:Guid}");
        clientGroup.MapDelete<DeleteContactCommandHandler>("/contacts/{id:Guid}");
        clientGroup.MapPut<SetDefaultContactCommandHandler>("/contacts/{id:Guid}/default");

        // Builder.MapGet<GetContactByIdQueryHandler>("/contacts/{id:Guid}");
    }
}