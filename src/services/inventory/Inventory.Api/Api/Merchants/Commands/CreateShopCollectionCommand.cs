using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Merchants.Requests;
using ECommerce.Inventory.Api.Merchants.Specifications;

namespace ECommerce.Inventory.Api.Merchants.Commands;

public class CreateShopCollectionCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateShopCollectionRequest request,
        IValidator<CreateShopCollectionRequest> validator,
        IShopCollectionRepository shopCollectionRepository,
        IMerchantRepository merchantRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var spec = new GetMerchantByIdSpecification(request.MerchantId);
        var merchant = await merchantRepository.FindAsync(spec, cancellationToken);

        if (merchant == null)
        {
            return Results.NotFound("Merchant not found");
        }

        var shopCollection = new ShopCollection(request.Name, request.Slug, request.ParentId);

        shopCollection.SetMerchant(merchant.Id);

        await shopCollectionRepository.AddAndSaveChangeAsync(shopCollection, cancellationToken);

        return TypedResults.Ok(new IdResponse(shopCollection.Id.ToString()));
    };
}