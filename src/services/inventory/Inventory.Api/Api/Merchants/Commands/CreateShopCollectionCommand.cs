using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Requests;
using ECommerce.Inventory.Api.Services;
using ECommerce.Shared.Libs.Extensions;

namespace ECommerce.Inventory.Api.Merchants.Commands;

public class CreateShopCollectionCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateShopCollectionRequest request,
        IMerchantService merchantService,
        IValidator<CreateShopCollectionRequest> validator,
        IShopCollectionRepository shopCollectionRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var merchantId = await merchantService.GetMerchantIdAsync(cancellationToken);

        var slug = string.IsNullOrEmpty(request.Slug) ?  request.Name.ToGenerateRandomSlug() : request.Slug;
        var shopCollection = new ShopCollection(request.Name, slug, request.Description, request.ParentId);

        shopCollection.SetMerchant(merchantId);

        await shopCollectionRepository.AddAndSaveChangeAsync(shopCollection, cancellationToken);

        return TypedResults.Ok(new IdResponse(shopCollection.Id.ToString()));
    };
}