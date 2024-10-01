using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Shared.Common.Infrastructure.Services;
using Microsoft.AspNetCore.StaticFiles;

namespace ECommerce.Api.ObjectStorages.Queries;

/// <summary>
/// Get File Query Handler
/// </summary>
public class GetFileQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string bucket
        , string key
        , string ext
        , HttpRequest request
        , IObjectStorageService objectStorageService
        , HttpContext httpContext
        , CancellationToken cancellationToken = default
    ) =>
    {

        // Get prefix from query parameter
        string? prefix = request.Query["prefix"];

        if (string.IsNullOrEmpty(prefix))
        {
            return Results.BadRequest("Prefix is missing.");
        }

        var decodedPrefix = Uri.UnescapeDataString(prefix);

        var filePath = $"{decodedPrefix}/{key}.{ext}";

        var fileStream = await objectStorageService.DownloadAsync(
            bucket
            , filePath
            , cancellationToken
        );

        if (fileStream == null)
            return Results.NotFound();

        var fileProvider = new FileExtensionContentTypeProvider();

        if (!fileProvider.TryGetContentType($"{key}.{ext}", out string? contentType))
        {
            throw new ArgumentOutOfRangeException($"Unable to find Content Type for file name {key}.{ext}");
        }

        httpContext.SetContentDispositionResponseHeader();

        return TypedResults.Stream(fileStream, contentType);
    };
}
