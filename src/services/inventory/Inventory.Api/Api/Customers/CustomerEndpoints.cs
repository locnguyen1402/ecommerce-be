using ECommerce.Inventory.Api.Customers.Commands;
using ECommerce.Inventory.Api.Customers.Queries;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Inventory.Api.Endpoints;

public class CustomerEndpoints(WebApplication app) : MinimalEndpoint(app, "/inventory/customers")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Customers");
    }
    public override void MapEndpoints()
    {
        // Customers
        Builder.MapPost<CreateCustomerByAdminCommandHandler>("/by-admin");
        Builder.MapPut<UpdateCustomerCommandHandler>("/{id:Guid}");
        Builder.MapPost<CreateCustomerCommandHandler>("/");
        Builder.MapPost<CreateCustomerConfirmOtpCommandHandler>("/confirm-otp");

        Builder.MapGet<GetCustomersByAdminQueryHandler>("/");
        Builder.MapGet<GetCustomerByIdQueryHandler>("/{id:Guid}");

        // Contacts
        Builder.MapPost<CreateContactCommandHandler>("/contacts");
        Builder.MapPost<CreateContactByAdminCommandHandler>("/contacts-by-admin");
        Builder.MapPut<UpdateContactCommandHandler>("/contacts/{id:Guid}");
        Builder.MapPut<SetDefaultContactCommandHandler>("/contacts/{id:Guid}/default");
        Builder.MapDelete<DeleteContactCommandHandler>("/contacts/{id:Guid}");

        Builder.MapGet<GetContactByIdQueryHandler>("/contacts/{id:Guid}");
        Builder.MapGet<GetContactsQueryHandler>("/contacts");
        Builder.MapGet<GetContactsByAdminQueryHandler>("/{customerId:Guid}/contacts");
    }
}