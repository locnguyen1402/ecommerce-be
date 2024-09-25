using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Inventory.Api.Importing.Queries;

/// <summary>
/// inheritdoc
/// </summary>
public class GetImportHistoryDetailQueryHandler : IEndpointHandler
{
    /// <summary>
    /// inheritdoc
    /// </summary>
    public Delegate Handle =>
    async (
        Guid id,
        IImportHistoryRepository importHistoryRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var importHistory = await importHistoryRepository.FindAsync(id, cancellationToken);

        if (importHistory == null)
            return Results.NotFound(new { title = "Import Histories Not Found" });

        return Results.Ok(importHistory);
    };
}

