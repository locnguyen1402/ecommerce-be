using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using ECommerce.Inventory.Api.ObjectStorages.Requests;
using ECommerce.Inventory.Api.ObjectStorageStorages.Responses;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Infrastructure.Settings;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Services;
using ECommerce.Shared.Libs.Extensions;

namespace ECommerce.Inventory.Api.Services;

public class ObjectService(
    IObjectStorageRepository repository,
    IObjectStorageService objectStorageService,
    IIdentityService identityService,
    IOptions<AppSettings> appSettings,
    ILogger<ObjectService> logger
) : IObjectService
{
    public async Task<Stream?> DownloadStreamQueryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var document = await repository.Query
            .Where(t => t.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        if (document == null)
            return null;

        var stream = await objectStorageService.DownloadAsync(
            document.Bucket,
            $"{document.Path}/{document.Id}{document.Extension}",
            cancellationToken
        );

        if (stream == null)
            return null;

        return stream;
    }

    public async Task<IResult> FindObjectQueryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var document = await repository.Query
            .Where(t => t.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        if (document == null)
            return Results.NotFound();

        return Results.Ok(document);
    }

    public async Task<IResult> GetObjectsQueryAsync(int page, int pageSize, string? keyword, CancellationToken cancellationToken = default)
    {
        var query = repository.Query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(keyword))
            query = query.Where(t =>
                EF.Functions.ILike($"{t.Title}", EF.Functions.Unaccent($"%{keyword}%")));

        var result = await query.ToPaginatedListAsync(page, pageSize, cancellationToken);

        return Results.Extensions.PaginatedListOk(result);
    }

    public async Task<PresignedUrlResponse?> GetPresignedUrlQueryAsync(Guid id, int expiration, CancellationToken cancellationToken = default)
    {
        var document = await repository.Query
           .Where(t => t.Id == id)
           .SingleOrDefaultAsync(cancellationToken);

        if (document == null)
            return null;

        var url = objectStorageService.GetPresignedUrl(
            document.Bucket,
            $"{document.Path}/{document.Id}{document.Extension}",
            TimeSpan.FromMinutes(expiration)
        );

        return document.ToPresignedUrlResponse(url);
    }

    public async Task<ObjectStorageResponse?> UploadFileAsync(UploadFileRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Uploading file to object storage");

        string name = request.File.GetFileName();
        string extension = request.File.GetFileExtension();
        string contentType = request.File.ContentType;
        long size = request.File.Length;
        Dictionary<string, string> tags = new()
        {
            { "created_by", $"{identityService.UserId}" },
            { "slug", name.ToSlug() }
        };

        ObjectStorage objectStorage = new(
            name,
            appSettings.Value.AwsSettings.DefaultBucket,
            request.Path,
            name,
            size,
            extension,
            contentType
        );

        await repository.AddAndSaveChangeAsync(objectStorage, cancellationToken);

        using var stream = request.File.OpenReadStream();
        using var fileStream = new MemoryStream();
        await stream.CopyToAsync(fileStream, cancellationToken);

        try
        {
            logger.LogInformation("Uploading file to object storage");

            var uploadResponse = await objectStorageService.UploadAsync(
               objectStorage.Bucket,
               objectStorage.Path,
               $"{objectStorage.Id}{extension}",
               fileStream,
               contentType,
               tags,
               cancellationToken
           );

            objectStorage.Uploaded(uploadResponse.FilePath);
            objectStorage.Update(objectStorage.Id.ToString(), uploadResponse.Key);

            await repository.UpdateAndSaveChangeAsync(objectStorage, cancellationToken);

            logger.LogInformation("File uploaded to object storage");

            return objectStorage.ToObjectStorageResponse();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error uploading file to object storage");

            objectStorage.FailedToUpload(ex.Message);

            await repository.UpdateAndSaveChangeAsync(objectStorage, cancellationToken);

            return null;
        }
    }
}