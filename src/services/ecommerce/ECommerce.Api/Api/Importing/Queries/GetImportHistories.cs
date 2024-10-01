using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Importing.Queries;

/// <summary>
/// inheritdoc
/// </summary>
public class GetImportHistoriesQueryHandler : IEndpointHandler
{
    /// <summary>
    /// inheritdoc
    /// </summary>
    public Delegate Handle =>
    async (
        int page,
        int pageSize,
        string? keyword,
        string? documentType,
        DateTimeOffset? from,
        DateTimeOffset? to,
        IImportHistoryRepository importHistoryRepository,
        HttpContext httpContext,
        CancellationToken cancellationToken) =>
    {
        var query = importHistoryRepository.Query.AsNoTracking();

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(x =>
            EF.Functions.ILike(EF.Functions.Unaccent(x.Document.Name), EF.Functions.Unaccent($"%{keyword}%")));

        if (from != null && from.HasValue)
            query = query.Where(x => x.CreatedAt >= from);

        if (to != null && to.HasValue)
        {
            var endOfDay = to.Value.AddDays(1).AddTicks(-1);
            query = query.Where(x => x.CreatedAt <= endOfDay);
        }

        if (documentType != null)
        {
            if (!Enum.TryParse<ImportDocumentType>(documentType, out var type))
                //return Results.BadRequest(new { Title = $"Type of import document by {documentType} not work." });
                query = FilterByImportDocumentType(query, type);
        }

        var result = await query.OrderByDescending(x => x.CreatedAt)
            .AsSplitQuery()
            .ToPaginatedListAsync(page, pageSize, cancellationToken);

        return Results.Extensions.PaginatedListOk(result);
    };

    private IQueryable<ImportHistory> FilterByImportDocumentType(IQueryable<ImportHistory> query, ImportDocumentType? type)
    {
        switch (type)
        {
            case ImportDocumentType.MASS_UPDATE_PRODUCT_BASE_INFO:
                return query.Where(t => t.DocumentType == ImportDocumentType.MASS_UPDATE_PRODUCT_BASE_INFO);
            case ImportDocumentType.MASS_UPDATE_PRODUCT_SALES_INFO:
                return query.Where(t => t.DocumentType == ImportDocumentType.MASS_UPDATE_PRODUCT_SALES_INFO);
            default:
                return query.Where(t => t.DocumentType != ImportDocumentType.UNSPECIFIED);
        }
    }
}

