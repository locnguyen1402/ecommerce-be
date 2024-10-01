using Microsoft.EntityFrameworkCore;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Shared.Common.Extensions;

namespace ECommerce.Api.Importing.Queries;

/// <summary>
/// inheritdoc
/// </summary>
public class GetImportHistoryLogsByIdQueryHandler : IEndpointHandler
{
    /// <summary>
    /// inheritdoc
    /// </summary>
    public Delegate Handle =>
    async (
        Guid id,
        int page,
        int pageSize,
        IImportHistoryRepository importHistoryRepository,
        HttpContext httpContext,
        CancellationToken cancellationToken) =>
    {

        var importHistory = await importHistoryRepository.Query
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (importHistory == null)
            //return Results.NotFound(new { title = "Import Histories Not Found" });
            throw new Exception("Import Histories Not Found");

        var logs = importHistory.Logs
            .AsEnumerable()
            .ToPaginatedList(page, pageSize);

        return Results.Extensions.PaginatedListOk(logs);
    };
}
