namespace ECommerce.Api.ObjectStorages.Requests;

/// <summary>
/// The upload single file request.
/// </summary>
public record UploadFileRequest
{
    /// <summary>
    /// The path of the file.
    /// </summary>
    public string Path { get; init; } = null!;

    /// <summary>
    /// The file.
    /// </summary>
    public IFormFile File { get; init; } = null!;
}