using ECommerce.Inventory.Api.ObjectStorages.Requests;
using ECommerce.Inventory.Api.ObjectStorageStorages.Responses;

namespace ECommerce.Inventory.Api.Services;

public interface IObjectService
{
    Task<ObjectStorageResponse?> UploadFileAsync(
        UploadFileRequest request,
        CancellationToken cancellationToken = default);

    Task<PresignedUrlResponse?> GetPresignedUrlQueryAsync(
        Guid id,
        int expiration,
        CancellationToken cancellationToken = default);

    Task<IResult> GetObjectsQueryAsync(
        int page,
        int pageSize,
        string? keyword,
        CancellationToken cancellationToken = default);

    Task<IResult> FindObjectQueryAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Stream?> DownloadStreamQueryAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}