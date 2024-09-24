namespace ECommerce.Shared.Common.Infrastructure.Services;

/// <summary>
/// The interface for the object storage service.
/// </summary>
public interface IObjectStorageService
{
    /// <summary>
    /// Gets the presigned URL for the object from the specified bucket and key.
    /// </summary>
    /// <param name="bucketName">The name of the bucket containing the object to get the presigned URL for.</param>
    /// <param name="filePath">The key of the object to get the presigned URL for.</param>
    /// <param name="expiration">The expiration of the presigned URL.</param>
    /// <returns>The presigned URL for the object.</returns>
    string GetPresignedUrl(string bucketName, string filePath, TimeSpan? expiration);

    /// <summary>
    /// Downloads the object from the specified bucket and key.
    /// </summary>
    /// <param name="bucketName">The name of the bucket containing the object to download.</param>
    /// <param name="filePath">The key of the object to download.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The stream of the object.</returns>
    Task<Stream?> DownloadAsync(string bucketName, string filePath, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads the object to the specified bucket and key.
    /// </summary>
    /// <param name="bucketName">The name of the bucket to upload the object to.</param>
    /// <param name="path">The path of the object to upload.</param>
    /// <param name="fileName">The path of the object to upload, included extension.</param>
    /// <param name="stream">The stream of the object to upload.</param>
    /// <param name="contentType">The content type of the object to upload.</param>
    /// <param name="tags">The tags of the object to upload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the object was uploaded successfully, false otherwise.</returns>
    Task<UploadResponse> UploadAsync(string bucketName, string path, string fileName, Stream stream, string? contentType, IDictionary<string, string>? tags, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the object from the specified bucket and key.
    /// </summary>
    /// <param name="bucketName">The name of the bucket containing the object to delete.</param>
    /// <param name="filePath">The key of the object to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the object was deleted successfully, false otherwise.</returns>
    Task<bool> DeleteAsync(string bucketName, string filePath, CancellationToken cancellationToken = default);
}

public record UploadResponse(
    bool Success,
    string Bucket,
    string FilePath,
    string ContentType,
    IDictionary<string, string> Tags
)
{
    public string Bucket { get; init; } = Bucket;
    public string ContentType { get; init; } = ContentType;
    public IDictionary<string, string> Tags { get; init; } = Tags;
}
