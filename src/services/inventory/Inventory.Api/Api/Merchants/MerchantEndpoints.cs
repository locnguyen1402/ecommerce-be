using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Merchants.Queries;
using ECommerce.Inventory.Api.Merchants.Commands;

namespace ECommerce.Inventory.Api.Endpoints;

public class MerchantEndpoints(WebApplication app) : MinimalEndpoint(app, "/inventory/merchants")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Merchants");
    }
    public override void MapEndpoints()
    {
        Builder.MapGet<GetMerchantsQueryHandler>("/");
        Builder.MapPost<CreateMerchantCommandHandler>("/");

        Builder.MapGet<GetMerchantByIdQueryHandler>("/{id:Guid}");
        Builder.MapPut<UpdateMerchantCommandHandler>("/{id:Guid}");

        Builder.MapGet<GetShopCollectionsQueryHandler>("/shop-collections");
        Builder.MapGet<GetShopCollectionOptionsQueryHandler>("/shop-collections/options");
        Builder.MapPost<CreateShopCollectionCommandHandler>("/shop-collections");

        Builder.MapGet<GetShopCollectionByIdQueryHandler>("/shop-collections/{id:Guid}");
        Builder.MapPut<UpdateShopCollectionCommandHandler>("/shop-collections/{id:Guid}");
        Builder.MapPut<AddProductsToShopCollectionCommandHandler>("/shop-collections/{id:Guid}/add-products");
    }
}