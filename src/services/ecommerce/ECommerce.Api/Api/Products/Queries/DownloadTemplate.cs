using ECommerce.Shared.Common.DocumentProcessing;
using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Services;
using ECommerce.Shared.Common.Infrastructure.Services;

namespace ECommerce.Api.Products.Queries;

/// <summary>
/// inheritdoc
/// </summary>
public class DownloadTemplateQuery : IEndpointHandler
{
    /// <summary>
    /// inheritdoc
    /// </summary>
    public Delegate Handle
    => async (
        string type,
        Guid? merchantId,
        string? keyword,
        string[]? shopCollectionIds,
        string[]? notInShopCollectionIds,
        IIdentityService identityService,
        IProductService productService,
        IXlsxProcessing xlsxProcessing,
        HttpContext httpContext,
        CancellationToken cancellationToken
    ) =>
    {
        httpContext.SetContentDispositionResponseHeader();

        merchantId = merchantId != null ? merchantId : identityService.MerchantId;

        var parsedType = Enum.TryParse<ImportDocumentType>(type, true, out var importDocumentType) ? importDocumentType : ImportDocumentType.UNSPECIFIED;

        if (parsedType == ImportDocumentType.MASS_UPDATE_PRODUCT_BASE_INFO)
        {
            var dataTemplate = await productService.GetImportBaseInfoTemplateAsync(
                keyword
                , merchantId
                , shopCollectionIds.ToQueryGuidList()
                , notInShopCollectionIds.ToQueryGuidList()
                , cancellationToken);

            var fileStream = xlsxProcessing.ExportXlsxSteam(dataTemplate, "Template");

            return TypedResults.Stream(fileStream, "application/octet-stream", "ImportBaseInfoTemplate.xlsx");
        }

        if (parsedType == ImportDocumentType.MASS_UPDATE_PRODUCT_SALES_INFO)
        {
            var dataTemplate = await productService.GetImportSalesInfoTemplateAsync(
                keyword
                , merchantId
                , shopCollectionIds.ToQueryGuidList()
                , notInShopCollectionIds.ToQueryGuidList()
                , cancellationToken);

            var fileStream = xlsxProcessing.ExportXlsxSteam(dataTemplate, "Template");

            return TypedResults.Stream(fileStream, "application/octet-stream", "ImportSalesInfoTemplate.xlsx");
        }

        return Results.BadRequest(new { title = "Invalid Import Template Type" });
    };

}

