using ECommerce.Inventory.Api.Services;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Inventory.Api.Importing.Queries;

public class DownloadStreamQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        IImportHistoryRepository importHistoryRepository,
        IObjectService objectService,
        ILogger<DownloadStreamQueryHandler> logger,
        HttpContext httpContext,
        CancellationToken cancellationToken
    ) =>
    {
        var importHistory = await importHistoryRepository.FindAsync(id, cancellationToken);

        if (importHistory == null)
        {
            logger.LogError("Import History with id {Id} not found.", id);
            throw new Exception($"Import History with id {id} not found.");
        }

        var filePath = importHistory.Document.GetFilePath();

        if (string.IsNullOrEmpty(filePath))
        {
            logger.LogError("File path not found for import history with id {Id}.", id);

            throw new Exception($"Import History with id {id} not found.");
        }

        var presignedResponse = await objectService.GetPresignedUrlQueryAsync(importHistory.Id, 5, cancellationToken);

        if (presignedResponse == null || string.IsNullOrEmpty(presignedResponse.Url))
        {
            logger.LogError("Presigned url not found for import history with id {Id}.", id);

            throw new Exception($"Import History with id {id} not found.");
        }

        var stream = await objectService.DownloadStreamQueryAsync(
            presignedResponse.Id,
            cancellationToken);

        if (stream == null)
        {
            logger.LogError("Stream not found for import history with id {Id}.", id);

            throw new Exception($"Import History with id {id} not found.");
        }

        logger.LogInformation("Download stream for import history with id {Id} successfully.", id);

        httpContext.SetContentDispositionResponseHeader();

        return TypedResults.Stream(stream,
            importHistory.Document.ContentType,
            $"{importHistory.Document.Name}{importHistory.Document.Extension}"
        );
    };
}

