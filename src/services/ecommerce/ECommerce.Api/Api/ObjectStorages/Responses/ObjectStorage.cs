
using System.Linq.Expressions;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.ObjectStorageStorages.Responses;

public record ObjectStorageResponse(
    Guid Id,
    string Bucket,
    string Path,
    string Name,
    string Extension,
    string FilePath,
    string ContentType,
    long Size,
    string? Title,
    string? Description
);

public record PresignedUrlResponse(
    Guid Id,
    string Url
);

public static class ObjectStorageProjection
{
    public static ObjectStorageResponse ToObjectStorageResponse(this ObjectStorage collection)
    {
        return ToObjectStorageResponse().Compile().Invoke(collection);
    }

    public static List<ObjectStorageResponse>? ToListObjectStorageResponse(this IEnumerable<ObjectStorage> ObjectStorages)
    {
        return ObjectStorages.Any() ? ObjectStorages.Select(ToObjectStorageResponse().Compile()).ToList() : null;
    }

    public static Expression<Func<ObjectStorage, ObjectStorageResponse>> ToObjectStorageResponse()
        => x =>
        new ObjectStorageResponse(
            x.Id,
            x.Bucket,
            x.Path,
            x.Name,
            x.Extension ?? string.Empty,
            x.Path,
            x.ContentType ?? string.Empty,
            x.Size,
            x.Title,
            x.Description
        );

    public static PresignedUrlResponse ToPresignedUrlResponse(this ObjectStorage objectStorage, string url)
    {
        return new PresignedUrlResponse(objectStorage.Id, url);
    }
}