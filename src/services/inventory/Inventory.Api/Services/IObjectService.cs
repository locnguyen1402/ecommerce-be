using ECommerce.Inventory.Api.ObjectStorages.Requests;

namespace ECommerce.Inventory.Api.Services;

public interface IObjectService
{
    Task<IResult> UploadFileAsync(
        UploadFileRequest request,
        CancellationToken cancellationToken = default);

    Task<IResult> GetPresignedUrlQueryAsync(
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

    Task<IResult> DownloadStreamQueryAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}