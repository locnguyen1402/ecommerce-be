using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Shared.Common.Infrastructure.Services;
using Microsoft.AspNetCore.StaticFiles;

namespace ECommerce.Inventory.Api.ObjectStorages.Queries;

/// <summary>
/// Get File Query Handler
/// </summary>
public class GetFileQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string bucket
        , string prefix
        , string year
        , string month
        , string day
        , string key
        , string ext
        , IObjectStorageService objectStorageService
        , CancellationToken cancellationToken = default
    ) =>
    {
        var fileStream = await objectStorageService.DownloadAsync(
       bucket, $"{prefix}/{year}/{month}/{day}/{key}.{ext}", cancellationToken);

        if (fileStream == null)
            return Results.NotFound();

        var fileProvider = new FileExtensionContentTypeProvider();

        if (!fileProvider.TryGetContentType($"{key}.{ext}", out string? contentType))
        {
            throw new ArgumentOutOfRangeException($"Unable to find Content Type for file name {key}.{ext}");
        }

        return Results.Stream(fileStream, contentType);
    };
}
