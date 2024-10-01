using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Merchants.Queries;
using ECommerce.Api.Merchants.Commands;
using ECommerce.Api.Products.Queries;
using ECommerce.Shared.Common.Constants;

namespace ECommerce.Api.Endpoints;

public class MerchantEndpoints(WebApplication app) : MinimalEndpoint(app, $"/{EndpointConstants.PATH_SEGMENT}/merchants")
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

        Builder.MapGet<GetProductsInShopCollectionQueryHandler>("/shop-collections/{id:Guid}/products");
        Builder.MapPut<AddProductsToShopCollectionCommandHandler>("/shop-collections/{id:Guid}/products/add");
        Builder.MapPut<RemoveProductsFromShopCollectionCommandHandler>("/shop-collections/{id:Guid}/products/remove");
    }
}