using Microsoft.AspNetCore.Http;

namespace ECommerce.Shared.Common.Extensions;

public static class FormFileExtensions
{
    public static string GetFileName(this IFormFile formFile)
        => Path.GetFileNameWithoutExtension(formFile.FileName);

    public static string GetFileExtension(this IFormFile formFile)
        => Path.GetExtension(formFile.FileName).ToLowerInvariant();
}
