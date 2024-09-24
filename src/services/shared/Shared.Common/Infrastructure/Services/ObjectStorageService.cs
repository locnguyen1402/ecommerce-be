using System.Net;

using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;

namespace ECommerce.Shared.Common.Infrastructure.Services;

/// <inheritdoc/>
public class ObjectStorageService : IObjectStorageService
{
    private readonly IAmazonS3 _s3Client; private readonly ILogger<ObjectStorageService> _logger;

    public ObjectStorageService(
        IAmazonS3 s3Client,
        ILogger<ObjectStorageService> logger)
    {
        _s3Client = s3Client;
        _logger = logger;
    }

    /// <inheritdoc/>
    public string GetPresignedUrl(string bucket, string filePath, TimeSpan? expiration)
    {
        _logger.LogInformation($"Getting Presigned URL for object from bucket {bucket} with file path {filePath}");

        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucket,
            Key = filePath,
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.Add(expiration ?? TimeSpan.FromMinutes(5))
        };

        string url = string.Empty;

        try
        {
            url = _s3Client.GetPreSignedURL(request);

            _logger.LogInformation($"Successfully got Presigned URL for object from bucket {bucket} with file path {filePath}");
        }
        catch (AmazonS3Exception ex)
        {
            _logger.LogError(ex, $"Failed to get Presigned URL for object from bucket {bucket} with file path {filePath}");
        }

        return url;
    }

    /// <inheritdoc/>
    public async Task<Stream?> DownloadAsync(string bucket, string filePath, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Downloading object from bucket {bucket} with file path {filePath}");

        var request = new GetObjectRequest
        {
            BucketName = bucket,
            Key = filePath
        };

        try
        {
            var response = await _s3Client.GetObjectAsync(request);
            if (response is not null && response.HttpStatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation($"Successfully downloaded object from bucket {bucket} with file path {filePath}");

                using var responseStream = new MemoryStream();
                await response.ResponseStream.CopyToAsync(responseStream, cancellationToken);
                responseStream.Seek(0, SeekOrigin.Begin);

                return responseStream;
            }

            _logger.LogError($"Failed to download object from bucket {bucket} with file path {filePath}");
        }
        catch (AmazonS3Exception ex)
        {
            _logger.LogError(ex, $"Failed to download object from bucket {bucket} with file path {filePath}");
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<UploadResponse> UploadAsync(
        string bucket, string path, string fileName,
        Stream stream, string? contentType, IDictionary<string, string>? tags,
        CancellationToken cancellationToken = default)
    {
        var currentDate = DateTime.UtcNow.Date;
        var filePath = $"{path}/{currentDate:yyyy}/{currentDate:MM}/{currentDate:dd}";
        var key = $"{filePath}/{fileName}";

        _logger.LogInformation($"Uploading object to bucket {bucket} with key {key}");

        var request = new PutObjectRequest
        {
            BucketName = bucket,
            Key = key,
            InputStream = stream,
            TagSet = new()
        };

        if (!string.IsNullOrEmpty(request.ContentType))
            request.ContentType = contentType;

        tags?.ToList().ForEach(x => request.TagSet.Add(new Tag { Key = x.Key, Value = x.Value }));

        try
        {
            var response = await _s3Client.PutObjectAsync(request);

            if (response is not null && response.HttpStatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation($"Successfully uploaded object to bucket {bucket} with key {key}");

                return new UploadResponse(true, bucket, filePath, contentType ?? string.Empty, tags ?? new Dictionary<string, string>());
            }

            _logger.LogError($"Failed to upload object to bucket {bucket} with key {key}");

            return new UploadResponse(false, bucket, filePath, contentType ?? string.Empty, tags ?? new Dictionary<string, string>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to upload object to bucket {bucket} with key {key}");

            return new UploadResponse(false, bucket, filePath, contentType ?? string.Empty, tags ?? new Dictionary<string, string>());
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(
        string bucket, string filePath,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Deleting object from bucket {bucket} with file path {filePath}");

        var response = await _s3Client.DeleteObjectAsync(bucket, filePath);

        if (response is not null && response.HttpStatusCode == HttpStatusCode.NoContent)
        {
            _logger.LogInformation($"Successfully deleted object from bucket {bucket} with file path {filePath}");

            return true;
        }

        _logger.LogError($"Failed to delete object from bucket {bucket} with file path {filePath}");

        return false;
    }
}
